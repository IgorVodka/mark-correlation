using MarkCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Helpers
{
    class CorrelationCalculator
    {
        List<Tuple<Student, Mark>> marks;

        public CorrelationCalculator()
        {
            marks = new List<Tuple<Student, Mark>>();
        }

        public void AddData(Student student, Mark mark)
        {
            this.marks.Add(new Tuple<Student, Mark>(student, mark));
        }

        public double ComputeCorrelation()
        {
            double[] values1;
            double[] values2;

            values1 = marks.Select(i => (double) i.Item1.Gender).ToArray();
            values2 = marks.Select(i => (double) i.Item2.Number).ToArray();

            if (values1.Length == 0 || values2.Length == 0)
                return 0;

            var avg1 = values1.Average();
            var avg2 = values2.Average();

            var sum1 = values1.Zip(values2, (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

            var sumSqr1 = values1.Sum(x => Math.Pow((x - avg1), 2.0));
            var sumSqr2 = values2.Sum(y => Math.Pow((y - avg2), 2.0));

            if (Math.Sqrt(sumSqr1 * sumSqr2) == 0)
                return 0;

            double result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);

            return result;
        }
    }
}
