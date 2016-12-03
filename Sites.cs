using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class Sites
    {
        List<Site> sites { set; get; }

        public void AddSites(Site site)
        {
            if (site != null)
                sites.Add(site);
            else
                Console.Out.WriteLine("Site == null");
        }
    }
}
