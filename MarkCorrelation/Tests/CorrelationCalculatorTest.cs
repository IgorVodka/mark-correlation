using MarkCorrelation.Helpers;
using MarkCorrelation.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Tests
{
    [TestFixture]
    class CorrelationCalculatorTest
    {
        private Student GetMaleStudent()
        {
            return new Student("Иванович");
        }

        private Student GetFemaleStudent()
        {
            return new Student("Ивановна");
        }

        private Mark GetExcellentMark()
        {
            return new Mark("Отл");
        }

        private Mark GetSatisfactoryMark()
        {
            return new Mark("Удов");
        }

        private Mark GetUnsatisfactoryMark()
        {
            return new Mark("Неуд");
        }

        private Mark GetUnknownMark()
        {
            return new Mark("НА");
        }

        [Test]
        public void TestNoCorrelation()
        {
            CorrelationCalculator calc = new CorrelationCalculator();
            calc.AddData(this.GetMaleStudent(), this.GetExcellentMark());
            calc.AddData(this.GetFemaleStudent(), this.GetExcellentMark());

            Assert.AreEqual(0.0d, (double) calc.ComputeCorrelation());
        }

        [Test]
        public void TestMinusOneCorrelation()
        {
            CorrelationCalculator calc = new CorrelationCalculator();
            calc.AddData(this.GetMaleStudent(), this.GetExcellentMark());
            calc.AddData(this.GetFemaleStudent(), this.GetUnsatisfactoryMark());

            Assert.AreEqual(-1.0d, (double)calc.ComputeCorrelation());
        }

        [Test]
        public void TestPlusOneCorrelation()
        {
            CorrelationCalculator calc = new CorrelationCalculator();
            calc.AddData(this.GetMaleStudent(), this.GetUnsatisfactoryMark());
            calc.AddData(this.GetFemaleStudent(), this.GetExcellentMark());

            Assert.AreEqual(+1.0d, (double)calc.ComputeCorrelation());
        }
    }
}
