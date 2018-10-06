using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Sessions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Requests.Contracts
{
    public interface IHttpRequest
    {
        string Path { get; }

        string Url { get; }

        Dictionary<string, object> FormData { get; }

        Dictionary<string, object> QueryData { get; }

        IHttpHeadersCollection Headers { get; }

        HttpRequestMethod RequestMethod { get; }

        HTTP.Sessions.IHttpSession Session { get; set; }

        IHttpCookieCollection Cookies { get; }
    }
}
