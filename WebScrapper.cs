using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Scraper
{
    class WebScrapper
    {

        public void Scrap()
        {

            
        }

        public Sites readConfigFile()
        {
            Sites sites = new Sites();

            try
            {
                //TODO Parsing JSON config file and create Site objects
                // [
                // {
                // 'id': ''
                // 'url': ''
                // 'node': ''
                // }
                // ]

                String json = System.IO.File.ReadAllText(@"C:\Users\wvitz\GIT\Influx Scraper\config\Sites.json");

                dynamic siteArray = JArray.Parse(json);

                for (int x = 0; x<siteArray.Count-1; x++)
                {
                    dynamic site = siteArray[x];

                    Site siteobj = new Site((int)site.id, (String)site.url, (String)site.node);

                    sites.AddSites(siteobj);
                }
            }
            catch (FileNotFoundException fnfex)
            {
                Console.WriteLine("File not found");
            }
            catch(DirectoryNotFoundException dnfex)
            {
                Console.WriteLine("Directory not found!");
            }

            return sites;

        }
    }
}
