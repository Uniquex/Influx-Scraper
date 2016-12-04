using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;

namespace Scraper
{
    class WebScrapper
    {

        public void Scrap(Sites sites)
        {
            for (int x = 0; x < sites.siteCount(); x++)
            {
                var Webget = new HtmlWeb();
                var doc = Webget.Load(sites.sites.ElementAt(x).url);
                try
                {
                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes(sites.sites.ElementAt(x).node))
                    {
                        String price = node.ChildNodes[0].InnerHtml;

                        if (price.EndsWith("€"))
                        {
                            price = price.TrimEnd('€');
                        }
                        else if (price.StartsWith("EUR"))
                        {
                            price = price.Substring(4);
                        }

                        if(price.Length > 3)
                        {
                            price = price.Remove(4);
                        }

                        sites.sites.ElementAt(x).price = float.Parse(price);
                    }
                }
                catch(Exception)
                {

                }
            }
            
        }

        public Sites readConfigFile()
        {
            Sites sites = new Sites();

            try
            {
                // [
                // {
                // 'id': ''
                // 'url': ''
                // 'node': ''
                // }
                // ]



                String json = System.IO.File.ReadAllText(@"Sites.json");

                dynamic siteArray = JArray.Parse(json);

                for (int x = 0; x < siteArray.Count; x++)
                {
                    dynamic site = siteArray[x];

                    //Site siteobj = new Site((int)site.id, (String)site.url, (String)site.node);

                    sites.AddSites((int)site.id, (String)site.url, (String)site.node);
                }
            }
            catch (FileNotFoundException fnfex)
            {
                Console.WriteLine("File not found");
                Console.WriteLine(fnfex.Message);
            }
            catch (DirectoryNotFoundException dnfex)
            {
                Console.WriteLine("Directory not found!");
                Console.WriteLine(dnfex.Message);
            }

            return sites;


        }
    }
}
