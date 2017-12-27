using MarkCorrelation.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Tests
{
    class MarkTest
    {
        static object[] MarkSource()
        {
            return new object[] {
                new object[] { "Неуд", MarkResult.Unsatisfactory, 0 },
                new object[] { "Удов", MarkResult.Satisfactory, 3 },
                new object[] { "Хор", MarkResult.Good, 4 },
                new object[] { "Отл", MarkResult.Excellent, 5 },
                new object[] { "Зчт", MarkResult.Passed, 1 },
                new object[] { "НА", MarkResult.Unknown, 0 }
            };
        }

        [Test, TestCaseSource("MarkSource")]
        public void TestMark(
            string originalText,
            MarkResult expectedValue,
            int expectedNumber
            )
        {
            Mark mark = new Mark(originalText);

            Assert.AreEqual(expectedValue, mark.Value);
            Assert.AreEqual(expectedNumber, mark.Number);
        }
    }
}
