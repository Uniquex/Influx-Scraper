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

// 

            WebScrapper scp = new WebScrapper();

            Sites sites = scp.readConfigFile();

            DBCon dbcon = new DBCon();
            dbcon.OpenConnection();

            var timer = new System.Threading.Timer((e) =>
            {
                int x = 1;
                scp.Scrap(sites);
                dbcon.WriteData(sites);
                Console.WriteLine("Count :" + x++);

            }, null, 0, (int)TimeSpan.FromMinutes(1).TotalMilliseconds);

            Console.Read();


        }
    }
}
