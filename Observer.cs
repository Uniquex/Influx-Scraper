using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class InfluxDB : IObserver<Sites>
    {
        private String observername;
        private IDisposable unsubscriber;

        public InfluxDB(String name)
        {
            this.observername = name;
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public virtual void Subscribe(IObservable<Sites> obs)
        {
            if (obs != null)
                unsubscriber = obs.Subscribe(this);
        }

        public void WriteToDB(Sites sites)
        {
            DBCon dbcon = new DBCon();

            dbcon.OpenConnection();

            dbcon.WriteData(sites);
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Sites sites)
        {
            this.WriteToDB(sites);
        }
    }
}
