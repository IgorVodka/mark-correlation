using System;
using MarkCorrelation.Helpers;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace MarkCorrelation.Requests
{
    abstract public class BaseRequest
    {
        protected string url;
        protected string response;

        public delegate void RequestHandler(object sender, Event.RequestEventArgs e);
        public event RequestHandler RequestEvent;

        public BaseRequest(string url, WebClientEx client)
        {
            this.url = url;
            this.response = null;

            if (client == null)
            {
                var cookies = new CookieContainer();
                this.Client = new WebClientEx(cookies);
                this.Client.Encoding = Encoding.UTF8;
            }
            else
            {
                this.Client = client;
            }

            System.Net.ServicePointManager.ServerCertificateValidationCallback += 
                (send, certificate, chain, sslPolicyErrors) => { return true; };
        }

        public BaseRequest(string url) : this(url, null) { }

        public void FillPlaceholder(string key, string value)
        {
            this.url = this.url.Replace("{" + key + "}", value);
        }

        private void CheckPlaceholders() 
        {
            if(this.url.Contains("{") || this.url.Contains("}"))
                throw new Exception("Fill the placeholders first.");
        }

        private void RaiseRequestEvent()
        {
            if(RequestEvent != null)
                RequestEvent(this, new Event.RequestEventArgs(this.response));
        }

        protected void PerformGet()
        {
            CheckPlaceholders();
            this.response = Client.DownloadString(this.url);

            RaiseRequestEvent();
        }

        protected void PerformGet(string query)
        {
            CheckPlaceholders();
            this.response = Client.DownloadString(this.url + "?" + query);

            RaiseRequestEvent();
        }

        protected void PerformPost(string data)
        {
            CheckPlaceholders();
            this.response = Client.UploadString(this.url, data);

            RaiseRequestEvent();
        }

        protected void PerformPost(NameValueCollection data)
        {
            CheckPlaceholders();
            this.response = Encoding.UTF8.GetString(Client.UploadValues(this.url, data));

            RaiseRequestEvent();
        }

        public abstract void Perform();

        public string Response
        {
            get
            {
                if (this.response == null)
                    throw new Exception("Make a request first.");

                return this.response;
            }
        }

        public WebClientEx Client
        {
            get; private set;
        }
    }
}

