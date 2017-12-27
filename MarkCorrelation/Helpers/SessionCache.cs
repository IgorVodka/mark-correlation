using MarkCorrelation.Models;
using MarkCorrelation.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Helpers
{
    public class SessionCache
    {
        protected Dictionary<int, Session> sessions;
        protected WebClientEx client;
        protected string sessionLink;

        public SessionCache(WebClientEx client, string sessionLink)
        {
            this.sessions = new Dictionary<int, Session>();
            this.client = client;
            this.sessionLink = sessionLink;
        }

        public Session this[int key]
        {
            get
            {
                if(!this.sessions.ContainsKey(key))
                {
                    SessionPageRequest spr = 
                        new SessionPageRequest(client, sessionLink, key);
                    spr.Perform();
                    this.sessions[key] = spr.Session;
                }

                return this.sessions[key];
            }
            set
            {
                this.sessions[key] = value;
            }
        }
    }
}
