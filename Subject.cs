using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    //https://msdn.microsoft.com/en-us/library/dd990377(v=vs.110).aspx#Version%20Information

    class SiteTracker : IObservable<Sites>
    {
        List<IObserver<Sites>> obslist;


        public SiteTracker()
        {
            obslist = new List<IObserver<Sites>>();
        }

        public IDisposable Subscribe(IObserver<Sites> observer)
        {
            if (!obslist.Contains(observer))
                obslist.Add(observer);
            return new Unsubscriber(obslist, observer);
        }

        public void CheckforUpdate()
        {
            WebScrapper scp = new WebScrapper();
            Sites sites = scp.readConfigFile();
            scp.Scrap(sites);
            
            foreach(Observer obs in obslist)
            {
                obs.OnNext(sites);
            }

            //dbcon.checkData(sites);
        }
    }

    class Unsubscriber : IDisposable
    {
        private List<IObserver<Sites>> _observers;
        private IObserver<Sites> _observer;

        public Unsubscriber(List<IObserver<Sites>> observers, IObserver<Sites> observer)
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
