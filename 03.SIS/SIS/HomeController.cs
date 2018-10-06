using SIS.HTTP.Enums;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS
{
    public class HomeController
    {
        public IHttpResponse Index()
        {
            string content = "<h1>Hello, World!</h1><h2>How u doin?</h2>";

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}
