using Newtonsoft.Json;
using System;

namespace MarkCorrelation.Models
{
    public class Tutor
    {
        private Tutor()
        {
        }

        [JsonProperty("value")]
        public string Name { get; set; }
    }
}

