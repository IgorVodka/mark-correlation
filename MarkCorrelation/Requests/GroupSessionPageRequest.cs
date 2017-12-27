using MarkCorrelation.Helpers;
using MarkCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Requests
{
    class GroupSessionPageRequest : BaseHtmlRequest
    {
        public GroupSessionPageRequest(WebClientEx client, string url, string subjectName) : 
            base(url, client)
        {
            this.SubjectName = subjectName;
            this.Marks = new Dictionary<Student, Mark>();
        }

        public string SubjectName
        {
            get; set;
        }

        public Dictionary<Student, Mark> Marks
        {
            get; set;
        }

        public override void Perform()
        {
            this.PerformGet();
            var column = this.DocumentNode.SelectSingleNode("//th[contains(@id, '" + this.SubjectName + "')]");
            if (column == null)
                return;

            int index = this.DocumentNode.SelectNodes("//th").GetNodeIndex(column);

            var rows = this.DocumentNode.SelectNodes("//table[contains(@class, 'eu-table')]/tbody/tr");
            foreach (var tr in rows)
            {
                var fio = tr.SelectSingleNode(".//div[contains(@class, 'student-fio')]/span[1]");
                if (fio == null)
                    continue;
                var mark = tr.SelectSingleNode(".//td[" + (index + 1) + "]/span");

                this.Marks.Add(new Student(fio.InnerText), new Mark(mark.InnerText.Trim()));
            }
        }
    }
}
