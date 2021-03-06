﻿using Parser.Models;
using Parser.WikiParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace Parser.Controllers
{
    public class ContinentController : ApiController
    {
        private ICollection<Country> countries;
        private HtmlParser parser;
        private Continent euorpe;
        private Continent nAmerica;
        private Continent sAmerica;
        private Continent australia;
        private Continent asia;
        private Continent africa;
        private Continent other;

        public ContinentController()
        {
            this.euorpe = new Continent();
            this.nAmerica = new Continent();
            this.sAmerica = new Continent();
            this.australia = new Continent();
            this.asia = new Continent();
            this.africa = new Continent();
            this.other = new Continent();
            this.parser = new HtmlParser();

            this.euorpe.Name = "Europe";
            this.nAmerica.Name = "North America";
            this.asia.Name = "Asia";
            this.africa.Name = "Africa";
            this.australia.Name = "Australia";
            this.sAmerica.Name = "South America";
            this.other.Name = "I will add this country to the right continent soon";
        }

        private ICollection<Continent> InitContinentsContent()
        {
            var html = this.parser.extractAllHtmlINeed();
            var countrStr = this.parser.extractCountries(html);
            this.countries = this.parser.extractCountriesObj(countrStr);

            foreach (var coutr in this.countries)
            {
                if (
                  (coutr.Name == "SWE") ||
                  (coutr.Name == "ESP") ||
                  (coutr.Name == "SUI") ||
                  (coutr.Name == "CZE") ||
                  (coutr.Name == "GER") ||
                  (coutr.Name == "SRB") ||
                  (coutr.Name == "RUS") ||
                  (coutr.Name == "UK") ||
                  (coutr.Name == "ROU") ||
                  (coutr.Name == "AUT") ||
                  (coutr.Name == "NLD") ||
                  (coutr.Name == "ITL") ||
                  (coutr.Name == "CRO") ||
                  (coutr.Name == "FRA") ||
                  (coutr.Name == "BUL") ||
                  (coutr.Name == "BEL"))
                {
                   
                    this.euorpe.Countries.Add(coutr);
                }
                else if (coutr.Name == "AUS")
                {
                   
                    this.australia.Countries.Add(coutr);
                }
                else if ((coutr.Name == "USA") ||
                   (coutr.Name == "MEX"))
                {
                    
                    this.nAmerica.Countries.Add(coutr);
                }
                else if ((coutr.Name == "JPN") ||
                    (coutr.Name == "CHN"))
                {
                    
                    this.asia.Countries.Add(coutr);
                }
                else if ((coutr.Name == "SAF") ||
                    (coutr.Name == "TUN"))
                {
                    
                    this.africa.Countries.Add(coutr);
                }
                else if ((coutr.Name == "ARG") ||
                    (coutr.Name == "BRA") ||
                    (coutr.Name == "ECU"))
                {
                   
                    this.sAmerica.Countries.Add(coutr);
                }
                else
                {
                    this.other.Countries.Add(coutr);
                }
            }

            ICollection<Continent> continents = new HashSet<Continent>() 
            { 
                this.africa,
                this.asia,
                this.australia,
                this.euorpe,
                this.nAmerica,
                this.sAmerica,
                this.other
            };

            return continents;
        }

        public HttpResponseMessage GetContinents()
        {
            var continents = this.InitContinentsContent();

            return this.Request.CreateResponse(HttpStatusCode.OK,continents);
        }

        public HttpResponseMessage GetContinentsById(string name)
        {
            var continents = this.InitContinentsContent();
            var theContinent=continents.FirstOrDefault(c => c.Name == name);
            if (theContinent == null)
            {
                throw new ArgumentNullException("No such Continent");
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, theContinent);
        }
    }
}