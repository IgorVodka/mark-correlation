using System;

namespace MarkCorrelation.Models
{
    public class Tutor
    {
        string name;

        private Tutor()
        {
        }

        public void Lookup(string name)
        {
            
        }

        public string Name
        {
            get 
            {
                if (name == null)
                    throw new NullReferenceException();
                
                return this.name;
            }
        }
    }
}

