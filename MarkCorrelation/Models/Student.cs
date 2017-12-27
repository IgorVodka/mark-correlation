using System;

namespace MarkCorrelation.Models
{
    public enum Gender
    {
        Unknown = 0,
        Male = -1,
        Female = 1
    }

    public class Student
    {
        public Student(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
        
        public Gender Gender
        {
            get
            {
                if (this.Name.Contains("ович") || this.Name.Contains("евич")
                    || this.Name.Contains("льич"))
                    return Gender.Male;
                if (this.Name.Contains("овна") || this.Name.Contains("евна")
                    || this.Name.Contains("ична"))
                    return Gender.Female;
                else
                    return Gender.Unknown;
            }
        }
    }
}

