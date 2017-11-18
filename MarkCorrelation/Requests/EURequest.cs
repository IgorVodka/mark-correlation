using System;
using MarkCorrelation.Helpers;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace MarkCorrelation.Requests
{
    public abstract class EURequest
    {
        WebClientEx client;
        string url;
        string response;

        public EURequest(string url, WebClientEx client)
        {
            this.url = url;
            this.response = null;

            if (client == null)
            {
                var cookies = new CookieContainer();
                this.client = new WebClientEx(cookies);
            }
            else
            {
                this.client = client;
            }

            System.Net.ServicePointManager.ServerCertificateValidationCallback += 
                (send, certificate, chain, sslPolicyErrors) => { return true; };
        }

        public EURequest(string url) : this(url, null) { }

        public void FillPlaceholder(string key, string value)
        {
            this.url = this.url.Replace("{" + key + "}", value);
        }

        private void CheckPlaceholders() 
        {
            if(this.url.Contains("{") || this.url.Contains("}"))
                throw new Exception("Fill the placeholders first.");
        }

        protected void PerformGet()
        {
            CheckPlaceholders();
            this.response = client.DownloadString(this.url);
        }

        protected void PerformPost(string data)
        {
            CheckPlaceholders();
            this.response = client.UploadString(this.url, data);
        }

        protected void PerformPost(NameValueCollection data)
        {
            CheckPlaceholders();
            this.response = Encoding.UTF8.GetString(client.UploadValues(this.url, data));
        }

        public abstract void Perform();

        public string GetResponse()
        {
            if (this.response == null)
                throw new Exception("Make a request first.");

            return this.response;
        }

        public WebClientEx Client
        {
            get { return this.client; }
        }
    }
}

