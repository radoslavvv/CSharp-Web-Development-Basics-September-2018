namespace _02.ValidateURL
{
    using System;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class ValidateUrl
    {
        public static void Main()
        {
            while (true)
            {
                string encodedURL = Console.ReadLine();
                string decodedURL = DecodeURL(encodedURL);

                string validationResult = ValidateURL(decodedURL);

                Console.WriteLine(validationResult);
            }
        }

        private static string ValidateURL(string decodedURL)
        {
            StringBuilder sb = new StringBuilder();

            Uri url;
            bool URLIsValid = Uri.TryCreate(decodedURL, UriKind.Absolute, out url)
                && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps);

            if (URLIsValid)
            {
                if (!url.IsDefaultPort)
                {
                    return "Invalid URL";
                }
                else
                {
                    sb.AppendLine($"Protocol: {url.Scheme}");
                    sb.AppendLine($"Host: {url.Host}");
                    sb.AppendLine($"Port: {url.Port}");
                    sb.AppendLine($"Path: {url.AbsolutePath}");

                    if (url.Query != "")
                    {
                        sb.AppendLine($"Query: {url.Query.TrimStart('?')}");
                    }
                    if (url.Fragment != "")
                    {
                        sb.AppendLine($"Fragment: {url.Fragment.TrimStart('#')}");
                    }
                }
            }
            else
            {
                sb.AppendLine("Invalid URL");
            }

            return sb.ToString().Trim();
        }

        private static string DecodeURL(string encodedURL)
        {
            encodedURL = WebUtility.UrlDecode(encodedURL);
            return encodedURL;
        }
    }
}
