using MarkCorrelation.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Tests
{
    class StudentTest
    {
        static object[] StudentSource()
        {
            return new object[] {
                new object[] { "Водка Игорь Эдуардович", Gender.Male },
                new object[] { "Ленин Владимир Ильич", Gender.Male },
                new object[] { "Хапов Андрей Владимирович", Gender.Male },
                new object[] { "Москатова Екатерина Сергеевна", Gender.Female },
                new object[] { "Кареникс Артёмс", Gender.Unknown },
                new object[] { "Со Гёну", Gender.Unknown },
            };
        }

        [Test, TestCaseSource("StudentSource")]
        public void TestStudent(
            string studentName,
            Gender expectedGender
            )
        {
            Student student = new Student(studentName);

            Assert.AreEqual(expectedGender, student.Gender);
        }
    }
}
