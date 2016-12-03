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

            

            Console.Read();


        }
    }
}
