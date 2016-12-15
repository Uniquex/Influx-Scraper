using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class Observer : IObserver<Sites>
    {
        String observername;

        public Observer(String name)
        {
            this.observername = name;
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
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
