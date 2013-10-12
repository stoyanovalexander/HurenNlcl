using Parser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace Parser.WikiParser
{
    public class HtmlParser
    {
        public string extractAllHtmlINeed()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            WebRequest req = 
                HttpWebRequest.Create("http://en.wikipedia.org/w/index.php?title=List_of_Grand_Slam_men%27s_singles_champions&action=edit");
            req.Method = "GET";
            string source;
            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                source = reader.ReadToEnd();
            }
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(source);
            var textarea = doc.DocumentNode.Descendants("textarea").FirstOrDefault(x => x.Attributes["id"].Value == "wpTextbox1");

            string allTexareaHtml = textarea.InnerHtml;

            int startIndex = allTexareaHtml.IndexOf("1968''");
            int endIndex = allTexareaHtml.LastIndexOf("! Legend");
            int theLength = endIndex - startIndex;
            string allHtmlINeed = allTexareaHtml.Substring(startIndex, theLength);
            return allHtmlINeed;
        }

        public List<string> extractCountries(string allHtmlINeed)
        {
            string startIndexStr = "flagicon";
            string endIndexStr = "}}";
            int startIndex = -5;
            int endIndex;
            int lenght;
            string theCountry;
            List<string> countries = new List<string>();

            int theCountrySlashIndex = -5;

            // Practicaly unreachable value            
            for (int i = 0; i < 10000; i++)
            {
                startIndex = allHtmlINeed.IndexOf(startIndexStr);
                if (startIndex != -1)
                {
                    endIndex = allHtmlINeed.IndexOf(endIndexStr, startIndex);
                    lenght = endIndex - startIndex;
                    theCountry = allHtmlINeed.Substring(startIndex, lenght);
                    theCountry = theCountry.Replace("flagicon|", string.Empty);

                    theCountrySlashIndex = theCountry.IndexOf("|");
                  
                    if (theCountrySlashIndex != -1)
                    {
                        theCountry = theCountry.Substring(0, theCountrySlashIndex);
                    }

                    countries.Add(theCountry);
                    allHtmlINeed = allHtmlINeed.Substring(endIndex + 1);
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < countries.Count; i++)
            {
                if (countries[i] == "Argentina")
                {
                    countries[i] = "ARG";
                }

                else if (countries[i] == "Romania")
                {
                    countries[i] = "ROU";
                }

                else if (countries[i] == "South Africa")
                {
                    countries[i] = "SAF";
                }

                else if ((countries[i] == "FRG") || (countries[i] == "Germany") || (countries[i] == "West Germany"))
                {
                    countries[i] = "GER";
                }

                else if (countries[i] == "Spain")
                {
                    countries[i] = "ESP";
                }

                else if (countries[i] == "Italy")
                {
                    countries[i] = "ITL";
                }

                else if (countries[i] == "Brazil")
                {
                    countries[i] = "BRA";
                }

                else if (countries[i] == "Russia")
                {
                    countries[i] = "RUS";
                }

                else if ((countries[i] == "Czechoslovakia") || (countries[i] == "TCH"))
                {
                    countries[i] = "CZE";
                }
            }
            return countries;
        }
    
        public List<Country> extractCountriesObj(List<string> countries)
        {
            List<string> titlesCountAndCountryStr = new List<string>();

            Dictionary<string, Point> locationsForCountry = new Dictionary<string, Point>();
            // key value pairs
            locationsForCountry.Add("FRA", new Point() { LatT = 48.74, LongT = 2.46 });
            locationsForCountry.Add("CRO", new Point() { LatT = 45.48, LongT = 16 });
            locationsForCountry.Add("NLD", new Point() { LatT = 52.21, LongT = 4.52 });
            locationsForCountry.Add("AUT", new Point() { LatT = 48.16, LongT = 16.21 });
            locationsForCountry.Add("ROU", new Point() { LatT = 44.25, LongT = 26.06 });
            locationsForCountry.Add("ECU", new Point() { LatT = -0.26, LongT = -78.57 });
            locationsForCountry.Add("ITL", new Point() { LatT = 41.53, LongT = 12.29 });
            locationsForCountry.Add("SAF", new Point() { LatT = -34.01, LongT = 18.54 });
            locationsForCountry.Add("UK", new Point() { LatT = 51.30, LongT = 0.07 });
            locationsForCountry.Add("BRA", new Point() { LatT = -15.45, LongT = -47.57 });
            locationsForCountry.Add("RUS", new Point() { LatT = 55.25, LongT = 37.37 });
            locationsForCountry.Add("SRB", new Point() { LatT = 44.48, LongT = 20.28 });
            locationsForCountry.Add("ARG", new Point() { LatT = -34.66, LongT = -58.18 });
            locationsForCountry.Add("GER", new Point() { LatT = 52.31, LongT = 13.24 });
            locationsForCountry.Add("CZE", new Point() { LatT = 50.01, LongT = 14.50 });
            locationsForCountry.Add("SUI", new Point() { LatT = 46.94, LongT = 7.44 });
            locationsForCountry.Add("ESP", new Point() { LatT = 40.26, LongT = -3.41 });
            locationsForCountry.Add("AUS", new Point() { LatT = -35.18, LongT = 149.08 });
            locationsForCountry.Add("SWE", new Point() { LatT = 59.28, LongT = 18.14 });
            locationsForCountry.Add("USA", new Point() { LatT = 38.53, LongT = -77.02 });

            List<Country> countryList = new List<Country>();

            Dictionary<string, int> countriesDictionary = new Dictionary<string, int>();
            foreach (var country in countries)
            {
                int count = 0;
                if (countriesDictionary.ContainsKey(country))
                {
                    count = countriesDictionary[country];
                }
                countriesDictionary[country] = count + 1;
            }

            foreach (KeyValuePair<string, int> item in countriesDictionary)
            {
                var currCounty = new Country();
                currCounty.Name = item.Key;
                currCounty.Wins = item.Value;
                if (locationsForCountry.ContainsKey(currCounty.Name))
                {
                    var point = locationsForCountry[currCounty.Name];
                    currCounty.LongT = point.LongT;
                    currCounty.LotT = point.LatT;
                }
                countryList.Add(currCounty);
            }

            return countryList;
        }
    }
}