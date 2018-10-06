﻿

using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Responses;
using System.Net;
using System.Text;

namespace SIS.WebServer.Results
{
    public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseStatusCode statusCode) 
            : base(statusCode)
        {
            this.Headers.Add(new HttpHeader("Content-Type", "text/html"));

            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
