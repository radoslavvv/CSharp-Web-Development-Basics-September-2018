
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Responses.Contracts;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse(HttpResponseStatusCode statusCode)
        {
            this.Headers = new HttpHeadersCollection();
            this.Content = new byte[0];
            this.StatusCode = statusCode;

            this.Cookies = new HttpCookieCollection();
        }

        public IHttpCookieCollection Cookies { get; set; }

        public HttpResponseStatusCode StatusCode { get; }

        public IHttpHeadersCollection Headers { get; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            this.Headers.Add(header);
        }

        public void AddCookie(HttpCookie cookie)
        {
            if (cookie != null)
            {
                this.Cookies.Add(cookie);
            }
        }
        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString()).Concat(this.Content).ToArray();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetResponseLine()}");

            result.AppendLine($"{this.Headers}");

            if (this.Cookies.HasCookies())
            {
                result.AppendLine($"Set-Cookie: {this.Cookies}");
                result.AppendLine();
            }

            result.AppendLine();

            return result.ToString();
        }
    }
}
