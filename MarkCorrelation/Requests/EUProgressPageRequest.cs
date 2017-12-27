using MarkCorrelation.Helpers;
using MarkCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Requests
{
    class EUProgressPageRequest : BaseHtmlRequest
    {
        public EUProgressPageRequest(WebClientEx client, string url) :
            base(url, client)
        {
            this.SemesterLinks = new List<SemesterLink>();
        }

        public override void Perform()
        {
            this.PerformGet();
            this.AddOtherSemesters();
            this.AddCurrentSemester();
            this.SortSemesterLinks();
        }

        private void AddOtherSemesters()
        {
            var termSelectLinks = this.DocumentNode.SelectNodes("//ul[@id='term-select']/li/a");

            var links = termSelectLinks.Select(element => new SemesterLink()
            {
                Link = LinkHelper.ToAbsolute(element.Attributes["href"].Value),
                Name = element.InnerText
            });

            this.SemesterLinks.AddRange(links);
        }

        private void AddCurrentSemester()
        {
            var currentSemesterName =
                this.DocumentNode.SelectNodes("//*[@id='content']/ul/li[2]/span")[0];

            var semesterLink = new SemesterLink()
            {
                Link = this.url,
                Name = currentSemesterName.InnerText,
            };

            this.SemesterLinks.Add(semesterLink);
        }

        private void SortSemesterLinks()
        {
            this.SemesterLinks.Sort(new Comparison<SemesterLink>(
                (SemesterLink a, SemesterLink b) =>
            {
                return a.GetSortingNumber().CompareTo(b.GetSortingNumber());
            }));
        }

        public List<SemesterLink> SemesterLinks
        {
            get; private set;
        }
    }
}
