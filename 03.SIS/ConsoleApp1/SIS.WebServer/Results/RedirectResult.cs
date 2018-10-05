

using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System.Net;
using System.Text;

namespace SIS.WebServer.Results
{
    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
            : base(HttpresponseStatsu.Redirect)
        {
            this.Headers.Add(new HttpHeader("Location", location));
        }
    }
}
