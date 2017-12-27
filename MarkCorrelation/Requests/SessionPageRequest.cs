using MarkCorrelation.Helpers;
using MarkCorrelation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Requests
{
    class SessionPageRequest : BaseHtmlRequest
    {
        private int sessionID;
        private Session session;

        public SessionPageRequest(WebClientEx client, string url, int sessionID) :
            base(url, client)
        {
            this.SessionID = sessionID;
            this.Session = new Session();
        }

        public int SessionID
        {
            get
            {
                return sessionID;
            }

            set
            {
                sessionID = value;
            }
        }

        internal Session Session
        {
            get
            {
                return session;
            }

            set
            {
                session = value;
            }
        }

        public override void Perform()
        {
            this.PerformGet("session_id=" + this.SessionID);

            var nodes = this.DocumentNode.SelectNodes("//a[@name='sdlk']");
            foreach(var node in nodes)
            {
                this.Session.RegisterGroup(
                    LinkHelper.CleanGroupName(node.InnerText),
                    LinkHelper.ToAbsolute(node.Attributes["href"].Value)
                );
            }
        }
    }
}
