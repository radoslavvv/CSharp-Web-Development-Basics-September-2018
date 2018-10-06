using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeadersCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString.Split(Environment.NewLine, StringSplitOptions.None);

            string[] requestLine = splitRequestContent[0].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.ValidateRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLine);

            this.ParseRequestUrl(requestLine);

            this.ParseRequestPath(requestLine);

            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());

            this.ParseCookies();

            bool requestHasBody = splitRequestContent.Length > 1;

            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1], requestHasBody);
        }

        private void ParseCookies()
        {
            if (this.Headers.ContainsHeader("Cookie"))
            {
                HttpHeader cookieHeader = Headers.GetHeader("Cookie");

                string[] cookieKeyValuePair = cookieHeader.Value
                    .Split("=", StringSplitOptions.RemoveEmptyEntries);

                string cookieKey = cookieKeyValuePair[0];
                string cookieValue = cookieKeyValuePair[1];

                HttpCookie cookie = new HttpCookie(cookieKey, cookieValue, 3);

                this.Cookies.Add(cookie);
            }
        }

        private void ParseRequestParameters(string bodyParameters, bool requestHasBody)
        {
            //this.ParseQueryParameters(this.Url);
            if (requestHasBody)
            {
                this.ParseFormDataPrameters(bodyParameters);
            }
        }

        private void ParseFormDataPrameters(string bodyParameters)
        {

            string[] formDataKeyValuePairs = bodyParameters.Split('&', StringSplitOptions.RemoveEmptyEntries);

            ExtractRequestParameters(formDataKeyValuePairs, this.FormData);
        }

        private void ExtractRequestParameters(string[] parameterKeyValuePairs, Dictionary<string, object> parametersCollection)
        {
            foreach (var parameterKeyValuePair in parameterKeyValuePairs)
            {
                string[] keyValuePair = parameterKeyValuePair.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (keyValuePair.Length != 2)
                {
                    throw new BadRequestException();
                }

                string parameterDataKey = keyValuePair[0];
                string parameterDataValue = keyValuePair[1];

                parametersCollection[parameterDataKey] = parameterDataValue;
                //this.QueryData.Add(queryKey, queryValue);
            }
        }

        private void ParseQueryParameters(string url)
        {
            string queryPattern = @"\?[^#]*(?=$|#)";
            Match queryMatch = Regex.Match(this.Url, queryPattern);

            if (queryMatch.Success == false)
            {
                return;
            }

            string query = queryMatch.Groups[0].Value;
            string[] subQueries = query.Split("&", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < subQueries.Length; i++)
            {
                string[] subQueryKeyValuePair = subQueries[i]
                    .Split("=", StringSplitOptions.RemoveEmptyEntries);

                if (subQueryKeyValuePair.Length != 2)
                {
                    continue;
                }

                string queryKey = subQueryKeyValuePair[0];
                string queryValue = subQueryKeyValuePair[1];

                this.QueryData.Add(queryKey, queryKey);
            }

            if (!this.IsValidRequestQueryString(query, subQueries))
            {
                throw new BadRequestException();
            }

            //string[] queryParameters = this.Url?
            //    .Split(new char[] { '?', '#' });

            //if (queryParameters.Length == 0)
            //{
            //    throw new BadRequestException();
            //}

            //string[] queryKeyValuePairs = queryParameters.Split('&', StringSplitOptions.RemoveEmptyEntries);

            //ExtractRequestParameters(queryKeyValuePairs, this.QueryData);
        }

        private bool IsValidRequestQueryString(string queryString, string[] subQueries)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return false;
            }

            if (subQueries.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void ParseHeaders(string[] requestHeaders)
        {
            if (!requestHeaders.Any())
            {
                throw new BadRequestException();
            }

            foreach (var requestHeader in requestHeaders)
            {
                if (string.IsNullOrEmpty(requestHeader))
                {
                    return;
                }

                string[] splitRequestHeader = requestHeader.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                string requestHeaderKey = splitRequestHeader[0];
                string requestHeaderValue = splitRequestHeader[1];

                this.Headers.Add(new HttpHeader(requestHeaderKey, requestHeaderValue));
            }
        }

        private void ParseRequestPath(string[] requestLine)
        {
            string path = this.Url?.Split("?").FirstOrDefault();

            if (string.IsNullOrEmpty(path))
            {
                throw new BadRequestException();
            }

            this.Path = path;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            if (string.IsNullOrEmpty(requestLine[1]))
            {
                throw new BadRequestException();
            }

            this.Url = requestLine[1];
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            if (!requestLine.Any())
            {
                throw new BadRequestException();
            }

            HttpRequestMethod requestMethod;

            bool parseResult = Enum.TryParse<HttpRequestMethod>(requestLine[0], out requestMethod);

            if (!parseResult)
            {
                throw new BadRequestException();
            }

            this.RequestMethod = requestMethod;
        }

        private bool ValidateRequestLine(string[] requestLine)
        {
            if (!requestLine.Any())
            {
                throw new BadRequestException();
            }

            if (requestLine.Length == 3
                && requestLine[2] == GlobalConstants.HttpOneProtocolFragment)
            {
                return true;
            }

            return false;
        }

        public HTTP.Sessions.IHttpSession Session { get; set; }

        public IHttpCookieCollection Cookies { get; }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeadersCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }
    }
}
