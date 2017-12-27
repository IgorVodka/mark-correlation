using MarkCorrelation.Helpers;
using MarkCorrelation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarkCorrelation.Requests
{
    class ModulesPageRequest : BaseHtmlRequest
    {
        public ModulesPageRequest(WebClientEx client, string url) :
            base(url, client)
        {

        }

        public override void Perform()
        {
            this.PerformGet();

            var match = Regex.Match(this.DocumentNode.InnerHtml, "var PREPS = ([^;]+)");
            string jsonPreps = match.Groups[1].Captures[0].Value;

            this.Tutors = JsonConvert.DeserializeObject<List<Tutor>>(jsonPreps);
        }

        public List<Tutor> Tutors { get; set; }
    }
}
