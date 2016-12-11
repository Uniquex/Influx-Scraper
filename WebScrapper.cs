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
        public int counter = 0;
        Boolean debug = false;
        public void Scrap(Sites sites)
        {
            counter++;
            for (int x = 0; x < sites.siteCount(); x++)
            {
                String siteurl = sites.sites.ElementAt(x).url;

                

                var Webget = new HtmlWeb();
                var doc = Webget.Load(sites.sites.ElementAt(x).url);
                try
                {
                    foreach (HtmlNode node in doc.DocumentNode.SelectNodes(sites.sites.ElementAt(x).node))
                    {
                        String price = node.ChildNodes[0].InnerHtml;

                        if (debug)
                        { Console.WriteLine(price); }

                        if (price.Contains(","))
                        {
                            price = price.TrimEnd(',');

                            if (debug)
                            {
                                Console.WriteLine("1 - " + price);
                            }
                        }
                        if (price.Contains("."))
                        {
                            price = price.TrimEnd('.');

                            if (debug)
                            {
                                Console.WriteLine("2 - " + price);
                            }
                        }
                        if (price.EndsWith("€"))
                        {
                            price = price.TrimEnd('€');

                            if (debug)
                            {
                                Console.WriteLine("3 - " + price);
                            }
                        }

                        if (price.StartsWith("€"))
                        {
                            price = price.Remove(1, 1);

                            if (debug)
                            {
                                Console.WriteLine("4 - " + price);
                            }
                        }

                        if (price.StartsWith("EUR"))
                        {
                            price = price.Substring(4);

                            if (debug)
                            {
                                Console.WriteLine("5 - " + price);
                            }
                        }

                        if(price.Length > 3)
                        {
                            price = price.Remove(3);

                            if (debug)
                            {
                                Console.WriteLine("6 - " + price);
                            }
                        }

                        if(price.EndsWith("."))
                        {
                            price = price.Remove(4);

                            if (debug)
                            {
                                Console.WriteLine("7 - " + price);
                            }
                            
                        }

                        if(siteurl.Contains("gearbest"))
                        {
                            float price1 = float.Parse(price) / 1.10f;
                            price = price1.ToString();

                            price = price.Split(',')[0];

                            if (debug)
                            {
                                Console.WriteLine("8 - " + price);
                            }

                        }

                        if (float.Parse(price) == 0)
                        {
                            price = null;
                        }

                        try
                        {
                            sites.sites.ElementAt(x).price = float.Parse(price);
                        }
                        catch (FormatException fex)
                        {
                            Console.WriteLine(fex.Message);
                        }
                        catch (ArgumentNullException anex)
                        {
                            Console.WriteLine(anex.Message);
                        }

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

                String json = "[   {     \"id\": 1,     \"url\": \"https://tradingshenzhen.com/notebook/632-mi-air-133-zoll-8gb-ram-256gb-ssd.html?search_query=xiaomi+notebook+air&results=2#/firmware-win_10_pro_chinese_only_licence\",     \"node\": \"//div//p//span[@id='our_price_display']\"   },   {     \"id\": 2,     \"url\": \"http://www.gearbest.com/laptops/pp_421980.html?wid=21\",     \"node\": \"//*[@id='unit_price']\"   },   {     \"id\": 3,     \"url\": \"http://www.gearbest.com/laptops/pp_416105.html?wid=21\",     \"node\": \"//*[@id='unit_price']\"   }]";

                //Linux for Cronjob
                //String json = System.IO.File.ReadAllText(@"~/mono/Influx-Scraper/bin/Debug/Sites.json");
                //Windows
                //String json = System.IO.File.ReadAllText(@"Sites.json");
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
