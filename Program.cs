﻿using System;
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


            InfluxDB influxDB = new InfluxDB("InfluxDB");
            PushBulletObs pbobs = new PushBulletObs("PushbulletObserver");

            SiteTracker tracker = new SiteTracker();

            

            tracker.Subscribe(influxDB);
            tracker.Subscribe(pbobs);

            tracker.CheckforUpdate();



            //var timer = new System.Threading.Timer((e) =>
            //{



            //}, null, 0, (int)TimeSpan.FromMinutes(60).TotalMilliseconds);

            //Console.Read();


        }
    }
}
