

using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System.Net;
using System.Text;

namespace SIS.WebServer.Results
{
    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
            : base(HTTP.Enums.HttpResponseStatusCode.NotFound)
        {
            this.Headers.Add(new HttpHeader("Location", location));
        }
    }
}
