using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using PushbulletSharp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    class PushBulletObs : IObserver<Sites>
    {
        private String observername;
        private IDisposable unsubscriber;

        public PushBulletObs(String name)
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

        public void SendNotification(Sites sites)
        {
            PushbulletClient client = new PushbulletClient("o.gPbFYLv0VHGepKmD77nJElg9FWSbGQca");

            //If you don't know your device_iden, you can always query your devices
            var devices = client.CurrentUsersDevices();

            var device = devices.Devices.Where(o => o.Iden == "ujuXuA1sqOqsjAnjr1gpPw");

            String message = "";

            for(int x = 0; x < sites.siteCount(); x++)
            {
                message = message + "Site " + sites.getSite(x).id + " has a value of: " + sites.getSite(x).price + "\n";
            }

            if (device != null)
            {
                PushNoteRequest request = new PushNoteRequest()
                {
                    DeviceIden = "ujuXuA1sqOqsjAnjr1gpPw",
                    Title = "Value has changed",
                    Body = message
                };
                PushResponse response = client.PushNote(request);
            }
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Sites sites)
        {
            this.SendNotification(sites);
        }
    }
}
