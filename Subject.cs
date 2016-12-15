using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    //https://msdn.microsoft.com/en-us/library/dd990377(v=vs.110).aspx#Version%20Information

    class SiteTracker : IObservable<Observer>
    {
        List<IObserver<Observer>> obslist;


        public SiteTracker()
        {
            obslist = new List<IObserver<Observer>>();
        }

        public IDisposable Subscribe(IObserver<Observer> observer)
        {
            if (!obslist.Contains(observer))
                obslist.Add(observer);
            return new Unsubscriber(obslist, observer);
        }

        public void CheckforUpdate()
        {
            DBCon dbcon = new DBCon();
            WebScrapper scp = new WebScrapper();

            Sites sites = scp.readConfigFile();
            scp.Scrap(sites);

            dbcon.checkData(sites);


        }
    }

    class Unsubscriber : IDisposable
    {
        private List<IObserver<Observer>> _observers;
        private IObserver<Observer> _observer;

        public Unsubscriber(List<IObserver<Observer>> observers, IObserver<Observer> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
