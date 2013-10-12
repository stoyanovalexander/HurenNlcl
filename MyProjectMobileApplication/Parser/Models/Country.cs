using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parser.Models
{
    public class Country
    {
        public string Name { get; set; }
        public int Wins { get; set; }
        public double LongT { get; set; }
        public double LotT { get; set; }
        public double Distance { get; set; }

        public int CompareTo(Country other)
        {
            if (this.Wins > other.Wins)
                return 1;
            if (this.Wins == other.Wins)
                return 0;
            return -1;
        }
    }
}