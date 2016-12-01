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
        String dbName = "newDbName";

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

        public async void ReadData()
        {

        }

        public async void WriteData(double price)
        {
            if(this.influxDbClient != null)
            {
                var pointToWrite = new Point()
                {
                    Name = "reading", // serie/measurement/table to write into
                    Tags = new Dictionary<string, object>()
                        {
                            { "SensorId", 8 },
                            { "SerialNumber", "00AF123B" }
                        },
                    Fields = new Dictionary<string, object>()
                        {
                            { "SensorState", "act" },
                            { "Humidity", 431 },
                            { "Temperature", 22.1 },
                            { "Resistance", 34957 }
                        },
                    Timestamp = DateTime.UtcNow // optional (can be set to any DateTime moment)
                };

                var response = await influxDbClient.Client.WriteAsync(dbName, pointToWrite);

            }

        }
    }


}
