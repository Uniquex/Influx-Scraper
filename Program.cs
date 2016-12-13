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
            WebScrapper scp = new WebScrapper();

            Sites sites = scp.readConfigFile();

            DBCon dbcon = new DBCon();
            dbcon.OpenConnection();

            //var timer = new System.Threading.Timer((e) =>
            //{
            scp.Scrap(sites);
            dbcon.WriteData(sites);
            //Console.WriteLine("Count: " + scp.counter);
            //Console.WriteLine(sites.getSite(1).url);
            //Console.WriteLine(sites.getSite(1).price);

            //}, null, 0, (int)TimeSpan.FromMinutes(60).TotalMilliseconds);

            //Console.Read();

            //Environment.Exit(0);

        }
    }
}
