using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkCorrelation.Requests.Event
{
    public class RequestEventArgs
    {
        public RequestEventArgs(string response) {
            Response = response;
        }

        public string Response { get; private set; }
    }
}
