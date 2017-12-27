using System;
using MarkCorrelation.Helpers;

namespace MarkCorrelation.Requests
{
    public class MainPageRequest : BaseHtmlRequest
    {
        public MainPageRequest(WebClientEx client) : 
            base("https://webvpn.bmstu.ru/+CSCOE+/home/index.html", client) { }

        public override void Perform()
        {
            this.PerformGet();
            var EULink = this.DocumentNode.SelectNodes("//a[text()='Электронный Университет']")[0];

            this.EULink = LinkHelper.GetLinkByOnClick(EULink.Attributes["href"].Value);
        }

        public string EULink
        {
            get; private set;
        }
    }
}

