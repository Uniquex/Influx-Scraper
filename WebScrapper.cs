using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class WebScrapper
    {
        Dictionary<String, String> sites;

        public void Scrap()
        {


        }

        public void readConfigFile()
        {
            try
            {
                //TODO Parsing JSON config file and create Site objects

                

            }
            catch (FileNotFoundException fnfex)
            {
                Console.WriteLine("File not found");
            }
        }
    }
}
