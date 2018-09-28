namespace _02.WebServer_HTTP_Protocol
{
    using System;
    using System.Net;

    public class URLDecoder
    {
        public static void Main()
        {
            string encodedURL = Console.ReadLine();
            string decodedURL = DecodeURL(encodedURL);

            Console.WriteLine(decodedURL);
        }

        private static string DecodeURL(string encodedUrl)
        {
            string decodedURL = WebUtility.UrlDecode(encodedUrl);
            return decodedURL;
        }
    }
}
