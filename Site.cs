using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class Site
    {
        private int id;
        private String url;
        private String node;

        public Site(int id, string url, string node)
        {
            this.id = id;
            this.url = url;
            this.node = node;
        }
    }
}
