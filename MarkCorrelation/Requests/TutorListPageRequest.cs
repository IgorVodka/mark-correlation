using MarkCorrelation.Helpers;
using MarkCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MarkCorrelation.Requests
{
    class TutorListPageRequest : BaseHtmlRequest
    {
        private string tutorName;
        List<SubjectLink> subjects;

        public TutorListPageRequest(WebClientEx client, string url, string tutorName) :
            base(url, client)
        {
            this.TutorName = tutorName;
            this.subjects = new List<SubjectLink>();
        }

        public string TutorName
        {
            get
            {
                return tutorName;
            }
            set
            {
                tutorName = value;
            }
        }

        public List<SubjectLink> Subjects
        {
            get
            {
                return subjects;
            }
        }

        public override void Perform()
        {
            this.PerformGet("search[fio]=" + Regex.Replace(HttpUtility.UrlEncode(this.TutorName), "%2b", "+"));
            var nodes = this.DocumentNode.SelectNodes("//h3[@class='search-teacher-title']");

            if (nodes == null)
                return;

            foreach(var tutorNode in nodes)
            {
                var nextSibling = tutorNode.NextSibling;
                if (nextSibling.ChildNodes.Count == 0)
                    nextSibling = nextSibling.NextSibling;

                var rows = nextSibling.SelectNodes(".//tr");

                foreach (var row in rows)
                {
                    var moduleLinkCollection = row.SelectNodes(".//a[text()='М']");

                    if (moduleLinkCollection == null)
                        continue;

                    var moduleLink = moduleLinkCollection[0];
                    var subjectAndGroup = row.SelectNodes("./td[7]/span[@class='false-link']")[0];

                    string moduleLinkHref = LinkHelper.ToAbsolute(moduleLink.Attributes["href"].Value);
                    string subjectAndGroupString = LinkHelper.CleanSpaces(subjectAndGroup.InnerText);

                    SubjectLink link = LinkHelper.ParseSubjectLink(
                        subjectAndGroupString, 
                        moduleLinkHref
                    );

                    this.Subjects.Add(link);
                }
            }
        }
    }
}
