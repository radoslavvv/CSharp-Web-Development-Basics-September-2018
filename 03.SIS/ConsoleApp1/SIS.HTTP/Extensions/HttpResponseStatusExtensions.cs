using SIS.HTTP.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SIS.HTTP.Extensions
{
    public static class HttpResponseStatusExtensions
    {
        public static string GetResponseLine(this HttpStatusCode statusCode) => $"{(int)statusCode} {statusCode}";
    }
}
