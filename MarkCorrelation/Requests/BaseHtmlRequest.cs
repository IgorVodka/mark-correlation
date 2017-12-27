using HtmlAgilityPack;
using MarkCorrelation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Requests
{
    abstract public class BaseHtmlRequest : BaseRequest
    {
        public BaseHtmlRequest(string url) : base(url)
        {
            InitializeEvent();
        }

        public BaseHtmlRequest(string url, WebClientEx Client) : base(url, Client)
        {
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            this.RequestEvent += OnRequest;
        }

        private void OnRequest(object sender, Event.RequestEventArgs e)
        {
            if (Document == null)
                Document = new HtmlDocument();

            Document.LoadHtml(e.Response);
        }

        protected HtmlDocument Document {
            get; private set;
        }

        public HtmlNode DocumentNode
        {
            get
            {
                if (Document == null)
                    return null;

                return Document.DocumentNode;
            }
        }
    }
}
