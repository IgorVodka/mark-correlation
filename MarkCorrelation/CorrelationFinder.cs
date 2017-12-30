using System;
using MarkCorrelation.Requests;
using MarkCorrelation.Helpers;
using MarkCorrelation.Models;
using System.Linq;
using System.Collections.Generic;

namespace MarkCorrelation
{
    public class CorrelationFinder
    {
        protected string TUTOR_NAME;
        protected WebClientEx client;
        protected SessionCache sessionCache;
        protected CorrelationCalculator calculator;
        protected ILogger logger;

        public CorrelationFinder(string tutorName, ILogger logger)
        {
            this.TUTOR_NAME = tutorName;
            this.logger = logger;
        }

        protected void HandleSemester(SemesterLink semester)
        {
            TutorListPageRequest tlpr = new TutorListPageRequest(client, semester.Link, TUTOR_NAME);
            tlpr.Perform();

            foreach(var subject in tlpr.Subjects)
            {
                logger.Log(semester.Name + " =>\n" + "Группа: " 
                    + subject.Group + ", предмет " + subject.SubjectName);

                var mpr = new ModulesPageRequest(client, subject.Url);
                mpr.Perform();
                var tutors = mpr.Tutors;

                if (tutors.Count > 1 && !tutors[0].Name.Contains(TUTOR_NAME))
                {
                    logger.Log(TUTOR_NAME + " не основной преподаватель, пропускаем.");
                    continue;
                }

                Session session = this.sessionCache[semester.GetSessionID()];
                string groupSessionLink = session.GetGroupLink(subject.Group);

                GroupSessionPageRequest gspr = new GroupSessionPageRequest(
                    client, 
                    groupSessionLink, 
                    subject.SubjectName
                );
                gspr.Perform();

                logger.Log("Найдено оценок: " + gspr.Marks.Count);

                foreach(var mark in gspr.Marks)
                {
                    logger.Log(String.Format("{0} ({1}): {2}", mark.Key.Name, mark.Key.Gender, mark.Value.Value));
                    this.calculator.AddData(mark.Key, mark.Value);
                }
            }
        }

        public void Handle()
        {
            this.calculator = new CorrelationCalculator();

            LoginRequest lr = new LoginRequest("viea16u008", "[password here]");
            lr.Perform();

            this.client = lr.Client;

            MainPageRequest mpr = new MainPageRequest(client);
            mpr.Perform();

            EUMainPageRequest eumpr = new EUMainPageRequest(client, mpr.EULink);
            eumpr.Perform();

            this.sessionCache = new SessionCache(this.client, eumpr.SessionLink);

            EUProgressPageRequest euppr = new EUProgressPageRequest(client, eumpr.ProgressLink);
            euppr.Perform();

            logger.Log("Начинаем...");

            var semesters = euppr.SemesterLinks;
            logger.SetStepsCount(semesters.Count - 1);
            
            foreach (var semester in semesters)
            {
                if (semester.Year > 2016)
                    break;

                HandleSemester(semester);
                logger.Step();
            }

            double result = calculator.ComputeCorrelation();
            // " (+1 это перекос в сторону отличных оценок у девушек, -1 - у парней)
            logger.LogCorrelation(result);
        }
    }
}
