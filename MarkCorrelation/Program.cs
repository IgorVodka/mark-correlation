using System;
using MarkCorrelation.Requests;
using MarkCorrelation.Helpers;
using MarkCorrelation.Models;
using System.Linq;
using System.Collections.Generic;

namespace MarkCorrelation
{
    class MainClass
    {
        const string TUTOR_NAME = "Бескровный";
        protected WebClientEx client;
        protected SessionCache sessionCache;
        protected CorrelationCalculator calculator;

        protected void HandleSemester(SemesterLink semester)
        {
            TutorListPageRequest tlpr = new TutorListPageRequest(client, semester.Link, TUTOR_NAME);
            tlpr.Perform();

            foreach(var subject in tlpr.Subjects)
            {
                Console.WriteLine(semester.Name + " =>\n" + "Группа: " 
                    + subject.Group + ", предмет " + subject.SubjectName);

                var mpr = new ModulesPageRequest(client, subject.Url);
                mpr.Perform();
                var tutors = mpr.Tutors;

                if (tutors.Count > 1 && !tutors[0].Name.Contains(TUTOR_NAME))
                {
                    Console.WriteLine(TUTOR_NAME + " не основной преподаватель, пропускаем.");
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

                foreach(var mark in gspr.Marks)
                {
                    Console.WriteLine("{0} ({1}): {2}", mark.Key.Name, mark.Key.Gender, mark.Value.Value);
                    this.calculator.AddData(mark.Key, mark.Value);
                }
            }
        }

        protected void Handle()
        {
            this.calculator = new CorrelationCalculator();

            LoginRequest lr = new LoginRequest("viea16u008", "dkcrwhat");
            lr.Perform();

            this.client = lr.Client;

            MainPageRequest mpr = new MainPageRequest(client);
            mpr.Perform();

            EUMainPageRequest eumpr = new EUMainPageRequest(client, mpr.EULink);
            eumpr.Perform();

            this.sessionCache = new SessionCache(this.client, eumpr.SessionLink);

            EUProgressPageRequest euppr = new EUProgressPageRequest(client, eumpr.ProgressLink);
            euppr.Perform();

            Console.WriteLine("Начинаем...");

            var semesters = euppr.SemesterLinks;
            foreach (var semester in semesters)
            {
                if (semester.Year > 2016)
                    break;

                HandleSemester(semester);

                // TODO: skip all semesters after previous
            }

            double result = calculator.ComputeCorrelation();
            Console.WriteLine("Корреляция: " + result.ToString("F6") + " (+1 это перекос в сторону отличных оценок у девушек, -1 - у парней)");

            Console.ReadKey(true);
        }

        public static void Main(string[] args)
        {
            new MainClass().Handle();
        }
    }
}
