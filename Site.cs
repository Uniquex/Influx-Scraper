using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class Site
    {
        public int id;
        public String url { get; }
        public String node { get; }
        public float price { get; set; }

        public Site(int id, string url, string node)
        {
            this.id = id;
            this.url = url;
            this.node = node;
        }

        public String toString()
        {
            return this.id + " Price: " + this.price;
        }
    }
}
