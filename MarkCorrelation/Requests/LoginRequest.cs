using System;
using MarkCorrelation.Requests;
using System.Collections.Specialized;

namespace MarkCorrelation
{
    public class LoginRequest : EURequest
    {
        string login;
        string password;

        public LoginRequest(string login, string password) : 
            base("https://webvpn.bmstu.ru/+webvpn+/index.html")
        {
            this.login = login;
            this.password = password;
        }

        public override void Perform()
        {
            this.PerformGet();
            this.PerformPost(new NameValueCollection() {
                {"username", this.login},
                {"password", this.password},
                {"login", "Login"}
            });
        }
    }
}

