using Parser.Models;
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
    public class CountryController : ApiController
    {
        HtmlParser parser;
        List<Country> countries;

        public CountryController()
        {
            this.parser = new HtmlParser();
        }

        [ActionName("all")]
        public HttpResponseMessage GetAllCountries()
        {
            var htmlString = this.parser.extractAllHtmlINeed();
            List<string> coutryStrings = this.parser.extractCountries(htmlString);
            this.countries = this.parser.extractCountriesObj(coutryStrings);

            return this.Request.CreateResponse(HttpStatusCode.OK, this.countries);
        }

        [ActionName("loc")]
        public HttpResponseMessage GetSortedByPosition(double longt, double lat)
        {
            var htmlString = this.parser.extractAllHtmlINeed();
            List<string> coutryStrings = this.parser.extractCountries(htmlString);
            this.countries = this.parser.extractCountriesObj(coutryStrings);

            foreach (var item in this.countries)
            {
                item.Distance = 111.319 * (Math.Sqrt((item.Latitude - lat) * (item.Latitude - lat) +
                                       (item.Longitude - longt) * (item.Longitude - longt)));
            }

            var sorted = this.countries
                                   .OrderBy(p => Math.Sqrt((p.Latitude - lat) * (p.Latitude - lat) +
                                       (p.Longitude - longt) * (p.Longitude - longt))).Select(p => p);

            return this.Request.CreateResponse(HttpStatusCode.OK, sorted);
        }

        [ActionName("win")]
        public HttpResponseMessage GetSortedByWin()
        {
            var htmlString = this.parser.extractAllHtmlINeed();
            List<string> coutryStrings = this.parser.extractCountries(htmlString);
            this.countries = this.parser.extractCountriesObj(coutryStrings);

            var sorted = from currC in this.countries
                         orderby currC.Wins descending
                         select currC;

            return this.Request.CreateResponse(HttpStatusCode.OK, sorted);
        }
    }
}