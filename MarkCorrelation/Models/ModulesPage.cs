using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Models
{
    class ModulesPage
    {
        public ModulesPage()
        {
            this.Subjects = new List<Subject>();
        }

        public List<Subject> Subjects { get; set; }
    }
}
