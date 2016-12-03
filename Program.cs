using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
// DBCon dbcon = new DBCon();

// dbcon.OpenConnection();

// dbcon.WriteData(100);

            WebScrapper scp = new WebScrapper();

            Sites sites = scp.readConfigFile();

            Console.WriteLine();

            scp.Scrap(sites);

            for(int x = 0; x<sites.siteCount(); x++)
            {
                String tost = sites.getSite(x).ToString();
                Console.WriteLine(tost);
            }

            Console.Read();


        }
    }
}
