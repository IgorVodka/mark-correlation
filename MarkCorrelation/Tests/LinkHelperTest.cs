using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MarkCorrelation.Models;
using MarkCorrelation.Helpers;

namespace MarkCorrelation.Tests
{
    [TestFixture]
    class LinkHelperTest
    {
        [Test]
        public void TestCleanSpaces()
        {
            string result = LinkHelper.CleanSpaces(
                "  oh wow       \n\n\t    (ИУ5-31)"
            );
            Assert.AreEqual("oh wow (ИУ5-31)", result);
        }

        static object[] CleanGroupNameSource()
        {
            return new object[] {
                new object[] { "ИУ5-31Б (Б)", "ИУ5-31Б" },
                new object[] { "Э1-11М (М)", "Э1-11М" },
                new object[] { "БМТ3-44", "БМТ3-44" }
            };
        }

        [Test, TestCaseSource("CleanGroupNameSource")]
        public void TestCleanGroupName(string groupName, string expected)
        {
            string result = LinkHelper.CleanGroupName(groupName);
            Assert.AreEqual(expected, result);
        }

        static object[] SubjectLinkSource()
        {
            return new object[] {
                new object[] { "Электротехника (ИУ5-31)", "Электротехника", "ИУ5-31" },
                new object[] { "Болтоведение (СМ9-51)", "Болтоведение", "СМ9-51" },
                new object[] { "Нетрадиционные системы (Э2-13)", "Нетрадиционные системы", "Э2-13" }
            };
        }

        [Test, TestCaseSource("SubjectLinkSource")]
        public void TestParseSubjectLink(
            string subjectAndGroup, 
            string expectedSubject, 
            string expectedGroup
            )
        {
            SubjectLink link = LinkHelper.ParseSubjectLink(subjectAndGroup, "");
            Assert.AreEqual(expectedSubject, link.SubjectName);
            Assert.AreEqual(expectedGroup, link.Group);
        }
    }
}
