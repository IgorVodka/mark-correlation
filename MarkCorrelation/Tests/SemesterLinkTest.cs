using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MarkCorrelation.Models;

namespace MarkCorrelation.Tests
{
    [TestFixture]
    class SemesterLinkTest
    {
        static object[] SemesterNameSource()
        {
            return new object[] {
                new object[] { "Весенний семестр 2006-2007(02)", 2006, SemesterLink.SemesterType.Spring, 20062, 3 },
                new object[] { "Осенний семестр 2014-2015(01)", 2014, SemesterLink.SemesterType.Autumn, 20141, 18 },
                new object[] { "Весенний семестр 2016-2017(02)", 2016, SemesterLink.SemesterType.Spring, 20162, 23 },
                new object[] { "Нечто 1", 0, SemesterLink.SemesterType.Unknown, 0, 0 },
            };
        }

        [Test, TestCaseSource("SemesterNameSource")]
        public void TestNameParsed(
            string name, 
            int expectedYear, 
            SemesterLink.SemesterType expectedType,
            int expectedSortingNumber,
            int expectedSessionID)
        {
            SemesterLink slt = new SemesterLink()
            {
                Link = "",
                Name = name,
            };

            Assert.AreEqual(expectedYear, slt.Year);
            Assert.AreEqual(expectedType, slt.Type);
            Assert.AreEqual(expectedSortingNumber, slt.GetSortingNumber());
            Assert.AreEqual(expectedSessionID, slt.GetSessionID());
        }
    }
}
