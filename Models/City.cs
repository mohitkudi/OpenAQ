using System.Collections.Generic;

namespace OpenAQ.Models
{
    public class City
    {
        public class Result
        {
            public string country { get; set; }
            public string name { get; set; }
            public string city { get; set; }
            public int count { get; set; }
            public int locations { get; set; }
            public List<string> parameters { get; set; }
        }
        public List<Result> results { get; set; }
    }
}
