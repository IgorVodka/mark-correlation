using System;
using MarkCorrelation.Helpers;

namespace MarkCorrelation.Requests
{
    public class EUMainPageRequest : EURequest
    {
        public EUMainPageRequest(WebClientEx client) : 
            base("https://webvpn.bmstu.ru/+CSCOE+/portal.html", client)
        {
            
        }

        public override void Perform()
        {
            this.PerformGet();
            Console.WriteLine(this.GetResponse());
        }
    }
}

