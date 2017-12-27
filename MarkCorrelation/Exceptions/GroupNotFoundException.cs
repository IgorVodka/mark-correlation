using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Exceptions
{
    class GroupNotFoundException : Exception
    {
        string group;

        public GroupNotFoundException(string group)
        {
            this.group = group;
        }
    }
}
