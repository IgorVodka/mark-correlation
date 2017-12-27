using MarkCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarkCorrelation.Helpers
{
    public class LinkHelper
    {
        public static string GetLinkByOnClick(string onClick)
        {
            var results = Regex.Match(onClick, "doURL\\('([0-9A-F]+)'");
            if (results.Groups.Count < 2)
                return null;

            string value = results.Groups[1].Captures[0].Value;
            return "https://webvpn.bmstu.ru/+CSCO+00" + value + "++/";
        }
        
        public static string ToAbsolute(string relative)
        {
            Uri original = new Uri(new Uri("https://webvpn.bmstu.ru/"), relative);

            return original.ToString();
        }

        public static string CleanSpaces(string text)
        {
            return Regex.Replace(text, "\\s+", " ").Trim();
        }

        public static SubjectLink ParseSubjectLink(string subjectAndGroup, string url)
        {
            SubjectLink link = new SubjectLink();
            link.Url = url;

            var match = Regex.Match(subjectAndGroup, "([^\\(]+) \\(([А-Я0-9-]+)\\)");
            link.SubjectName = match.Groups[1].Captures[0].Value;
            link.Group = match.Groups[2].Captures[0].Value;
            
            return link;
        }

        public static string CleanGroupName(string text)
        {
            return text.Replace("(М)", "").Replace("(Б)", "").Trim();
        }
    }
}
