using MarkCorrelation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Requests
{
    class EUMainPageRequest : BaseHtmlRequest
    {
        public EUMainPageRequest(WebClientEx client, string url) : 
            base(url, client) { }

        public override void Perform()
        {
            this.PerformGet();

            var progressLink = this.DocumentNode.SelectNodes("//a[contains(text(),'Текущая успеваемость')]")[0];
            this.ProgressLink = LinkHelper.ToAbsolute(progressLink.Attributes["href"].Value);

            var sessionLink = this.DocumentNode.SelectNodes("//a[contains(text(),'Сессия')]")[0];
            this.SessionLink = LinkHelper.ToAbsolute(sessionLink.Attributes["href"].Value);
        }

        public string ProgressLink
        {
            get; private set;
        }

        public string SessionLink
        {
            get; private set;
        }
    }
}
