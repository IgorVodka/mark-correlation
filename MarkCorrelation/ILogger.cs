using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation
{
    public interface ILogger
    {
        void Log(string text);
        void LogCorrelation(double correlation);

        void SetStepsCount(int count);
        void Step();
    }
}
