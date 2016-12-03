using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class Sites
    {
        public List<Site> sites = new List<Site>();

        public void AddSites(int id, String url, String node)
        {
            Site site = new Site(id, url, node);
            sites.Add(site);
        }

        public Site getSite(int pos)
        {
            return sites.ElementAt(pos);
        }

        public int siteCount()
        {
            return sites.Count;
        }
    }
}
