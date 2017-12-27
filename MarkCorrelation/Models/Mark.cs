using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Models
{
    public enum MarkResult
    {
        Unknown,
        Passed,
        Unsatisfactory,
        Satisfactory,
        Good,
        Excellent,
    }

    public class Mark
    {
        private string original;

        public Mark(string original)
        {
            this.original = original;
        }

        public MarkResult Value
        {
            get
            {
                switch(this.original)
                {
                    case "Зчт":
                        return MarkResult.Passed;
                    case "Неуд":
                        return MarkResult.Unsatisfactory;
                    case "Удов":
                        return MarkResult.Satisfactory;
                    case "Хор":
                        return MarkResult.Good;
                    case "Отл":
                        return MarkResult.Excellent;
                    default: 
                        return MarkResult.Unknown;
                }
            }
        }

        public int Number
        {
            get { 
                switch(this.Value) {
                    case MarkResult.Satisfactory:
                        return 3;
                    case MarkResult.Good:
                        return 4;
                    case MarkResult.Excellent:
                        return 5;
                    case MarkResult.Passed:
                        return 1;
                    default:
                        return 0;
                }
            }
        }

        public override string ToString()
        {
            return original;
        }
    }
}
