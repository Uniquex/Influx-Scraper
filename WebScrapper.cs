using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;
using System.Net;

namespace Scraper
{
    class WebScrapper
    {
        Boolean debug = false;

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Sites Scrap(Sites sites)
        {
            if(CheckForInternetConnection())
            {
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
                                    Console.WriteLine("0 - " + price);
                                }
                            }

                            if(price.Contains("Loading"))
                            {
                                price = "0";

                                if (debug)
                                {
                                    Console.WriteLine("1 - " + price);
                                }
                            }

                            if (price.Contains("."))
                            {
                                price = price.TrimEnd('.');

                                if(debug)
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

                            if(siteurl.Contains("gearbest") && !price.Contains("Loa"))
                            {
                                float price1 = float.Parse(price) / 1.10f;
                                price = price1.ToString();

                                price = price.Split(',')[0];

                                if (debug)
                                {
                                    Console.WriteLine("8 - " + price);
                                }

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
                    catch(HtmlWebException web)
                    {
                        Console.WriteLine(web.Message);
                        Console.WriteLine("Undefined exception");
                    }
                    catch(FormatException fex)
                    {
                        Console.WriteLine(fex.Message);
                        Console.WriteLine("Format exception");
                    }
                
                }
            }
            else
            {
                Console.WriteLine("no internet connection");
            }
            

            return sites; 
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

                String json = "[   {     \"id\": 1,     \"url\": \"https://tradingshenzhen.com/notebook/632-mi-air-133-zoll-8gb-ram-256gb-ssd.html?search_query=xiaomi+notebook+air&results=2#/firmware-win_10_pro_chinese_only_licence\",     \"node\": \"//div//p//span[@id='our_price_display']\"   },   {     \"id\": 2,     \"url\": \"http://www.gearbest.com/laptops/pp_421980.html?wid=4\",     \"node\": \"//*[@id='unit_price']\"   },   {     \"id\": 3,     \"url\": \"http://www.gearbest.com/laptops/pp_416105.html?wid=21\",     \"node\": \"//*[@id='unit_price']\"   }]";

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
