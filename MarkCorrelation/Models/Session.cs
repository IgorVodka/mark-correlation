using MarkCorrelation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Models
{
    public class Session
    {
        protected Dictionary<string, string> groups;

        public Session()
        {
            groups = new Dictionary<string, string>();
        }

        public void RegisterGroup(string group, string link)
        {
            if(!groups.ContainsKey(group))
                groups.Add(group, link);
        }

        public string GetGroupLink(string group)
        {
            if (!groups.ContainsKey(group))
                throw new GroupNotFoundException(group);

            return groups[group];
        }
    }
}
