using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarkCorrelation.Models
{
    class SemesterLink
    {
        private string link;
        private string name;

        public enum SemesterType
        {
            Unknown,
            Autumn,
            Spring
        }

        public string Link
        {
            get
            {
                return this.link;
            }
            set
            {
                this.link = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.ParseName();
            }
        }

        public SemesterType Type
        {
            get; private set;
        }

        public int Year
        {
            get; private set;
        }

        protected void ParseName()
        {
            Match m = Regex.Match(this.Name, "([0-9]+)-([0-9]+)");

            int year = 0;
            bool parsed = int.TryParse(m.Groups[1].Value, out year);

            if (!m.Success || !parsed)
            {
                this.Type = SemesterType.Unknown;
                this.Year = 0;
                return;
            }

            this.Year = year;
            this.Type =
                (Name.Contains("Осенний") ? 
                    SemesterType.Autumn :
                    (
                        Name.Contains("Весенний") ? 
                        SemesterType.Spring :
                        SemesterType.Unknown
                    )
                );
        }

        public int GetSortingNumber()
        {
            return this.Year * 10 + (int) this.Type;
        }

        public int GetSessionID()
        {
            if (this.Year == 0)
                return 0;

            return (this.Year - 2005) * 2 + (this.Type == SemesterType.Autumn ? 0 : 1);
        }
    }
}
