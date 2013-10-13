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
            Dictionary<string, Point> locationsForCountry = new Dictionary<string, Point>();
            // key value pairs
            locationsForCountry.Add("FRA", new Point() { Latitude = 48.74, Longitude = 2.46 });
            locationsForCountry.Add("CRO", new Point() { Latitude = 45.48, Longitude = 16 });
            locationsForCountry.Add("NLD", new Point() { Latitude = 52.21, Longitude = 4.52 });
            locationsForCountry.Add("AUT", new Point() { Latitude = 48.16, Longitude = 16.21 });
            locationsForCountry.Add("ROU", new Point() { Latitude = 44.25, Longitude = 26.06 });
            locationsForCountry.Add("ECU", new Point() { Latitude = -0.26, Longitude = -78.57 });
            locationsForCountry.Add("ITL", new Point() { Latitude = 41.53, Longitude = 12.29 });
            locationsForCountry.Add("SAF", new Point() { Latitude = -34.01, Longitude = 18.54 });
            locationsForCountry.Add("UK", new Point() { Latitude = 51.30, Longitude = 0.07 });
            locationsForCountry.Add("BRA", new Point() { Latitude = -15.45, Longitude = -47.57 });
            locationsForCountry.Add("RUS", new Point() { Latitude = 55.25, Longitude = 37.37 });
            locationsForCountry.Add("SRB", new Point() { Latitude = 44.48, Longitude = 20.28 });
            locationsForCountry.Add("ARG", new Point() { Latitude = -34.66, Longitude = -58.18 });
            locationsForCountry.Add("GER", new Point() { Latitude = 52.31, Longitude = 13.24 });
            locationsForCountry.Add("CZE", new Point() { Latitude = 50.01, Longitude = 14.50 });
            locationsForCountry.Add("SUI", new Point() { Latitude = 46.94, Longitude = 7.44 });
            locationsForCountry.Add("ESP", new Point() { Latitude = 40.26, Longitude = -3.41 });
            locationsForCountry.Add("AUS", new Point() { Latitude = -35.18, Longitude = 149.08 });
            locationsForCountry.Add("SWE", new Point() { Latitude = 59.28, Longitude = 18.14 });
            locationsForCountry.Add("USA", new Point() { Latitude = 38.53, Longitude = -77.02 });

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
                    currCounty.Longitude = point.Longitude;
                    currCounty.Latitude = point.Latitude;
                }
                countryList.Add(currCounty);
            }

            return countryList;
        }
    }
}