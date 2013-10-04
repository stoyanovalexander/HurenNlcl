using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Globalization;
using HtmlAgilityPack;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            WebRequest req = HttpWebRequest.Create("http://en.wikipedia.org/w/index.php?title=List_of_Grand_Slam_men%27s_singles_champions&action=edit");
            req.Method = "GET";

            string source;
            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                source = reader.ReadToEnd();
            }

            //Console.WriteLine(source);

             HtmlDocument doc = new HtmlDocument();
             doc.LoadHtml(source);
             var textarea = doc.DocumentNode.Descendants("textarea").FirstOrDefault(x => x.Attributes["id"].Value == "wpTextbox1");
             if (textarea != null)
             {
                 //Console.WriteLine(textarea.InnerHtml);
             }
             string please = textarea.InnerHtml;

             Console.WriteLine(please);
 //foreach(HtmlNode link in doc.DocumentElement.SelectNodes("//a[@href"])
 //{
 //   HtmlAttribute att = link["href"];
 //   att.Value = FixLink(att);
 //}

 //doc.Save("file.htm");
        }

        
    }
}
