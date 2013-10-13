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
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Distance { get; set; }
    }
}