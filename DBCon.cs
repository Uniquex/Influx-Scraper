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
        String tblName = "Sites";

        public void OpenConnection()
        {
            try
            {
                influxDbClient = new InfluxDbClient("http://famiel.ddns.net:8086/", "root", "root", InfluxDbVersion.v_1_0_0);
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
                        Name = tblName, // serie/measurement/table to write into
                        Tags = new Dictionary<string, object>()
                            {
                                { "Id", site.id },
                            },
                        Fields = new Dictionary<string, object>()
                            {
                                { "Price", site.price }
                            },
                        Timestamp = DateTime.Now // optional (can be set to any DateTime moment)
                    };

                    points.Add(pointToWrite);

                }

                await influxDbClient.Client.WriteAsync(dbName, points);
            }

        }

        public async void checkData(Sites sites)
        {
            
            var queries = new string[sites.siteCount()];
            for(int x = 0; x < sites.siteCount(); x++)
            {
                queries[x] = "SELECT last(price) FROM " + tblName + " WHERE id == " + x;
            }

            var response = await influxDbClient.Client.QueryAsync(dbName, queries);
        }
    }


}
