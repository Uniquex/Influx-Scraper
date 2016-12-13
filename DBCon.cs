using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.Common.Infrastructure;
using System.IO;
using InfluxData.Net.InfluxDb.Models;
using InfluxData.Net.InfluxDb.Enums;

namespace Scraper
{
    class DBCon
    {
        InfluxDbClient influxDbClient;
        String dbName = "db_1";

        public void OpenConnection()
        {
            try
            {
                influxDbClient = new InfluxDbClient("http://192.168.1.100:8086/", "root", "root", InfluxDbVersion.v_1_0_0);
            }
            catch(InfluxDataWarningException ex)
            {
                Console.WriteLine(ex.WarningMessage);   
            }
            catch(FileLoadException ex2)
            {
                Console.WriteLine(ex2.Message);
            }
        }

        public async void WriteData(Sites sites)
        {
           if(this.influxDbClient != null)
            {
                List<Point> points = new List<Point>();
                for(int x = 0; x < sites.siteCount(); x++)
                {
                    Site site = sites.getSite(x);

                    var pointToWrite = new Point()
                    {
                        Name = "Sites", // serie/measurement/table to write into
                        Tags = new Dictionary<string, object>()
                            {
                                { "Id", site.id },
                            },
                        Fields = new Dictionary<string, object>()
                            {
                                { "Price", site.price }
                            },
                        Timestamp = DateTime.UtcNow // optional (can be set to any DateTime moment)
                    };

                    points.Add(pointToWrite);

                }

                var response = await influxDbClient.Client.WriteAsync(dbName, points);


                System.Threading.Thread.Sleep(1000);

                if (!response.Success)
                {
                    Console.WriteLine("Could not write into DB");
                }
            }

        }
    }


}
