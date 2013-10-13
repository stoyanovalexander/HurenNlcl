using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parser.Models
{
    public class Continent
    {
        public string Name { get; set; }
        public ICollection<Country> Countries { get; set; }
        public Continent()
        {
            this.Countries = new HashSet<Country>();
        }
    }
}