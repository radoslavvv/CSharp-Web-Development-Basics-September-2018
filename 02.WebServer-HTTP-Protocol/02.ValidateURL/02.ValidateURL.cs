using System;
using System.Net;
using System.Text.RegularExpressions;

namespace _02.ValidateURL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string decodedURL = WebUtility.UrlDecode(input);

            Regex validURLregex = new Regex(@"(?<protocol>http|https):\/\/(www\.)*(?<host>[A-Za-z-]+)\.(?<domain>\w+)(?<port>:\d+)*(?<path>.+)*");

            Regex queryRegex = new Regex(@"(?<path>.+)\?(?<query>.+)(?<fragment>#.*)*");

            Regex fragmentRegex = new Regex(@"(?<fragment>#.*)");

            if (validURLregex.IsMatch(input))
            {
                Console.WriteLine("yes");
                Match match = validURLregex.Match(input);

                string protocol = match.Groups["protocol"].Value;
                string host = match.Groups["host"].Value;
                string domain = match.Groups["domain"].Value;
                string port = match.Groups["port"].Value;
                string path = match.Groups["path"].Value;
               
                if (host == "https" && port != "443" || host == "http" && port != "80")
                {
                    Console.WriteLine("Invalid URL");
                }
                else
                {
                    Console.WriteLine($"Protocol: {protocol}");
                    Console.WriteLine($"Host: {host}.{domain}");

                    string defaultPort = protocol == "https" ? "443" : "80";
                    Console.WriteLine($"Port: {defaultPort}");

                    if (queryRegex.IsMatch(path))
                    {
                        Match queryMatch = queryRegex.Match(path);
                        string query = queryMatch.Groups["query"].Value;
                        string queryPath = queryMatch.Groups["path"].Value;

                        Console.WriteLine($"Path: {queryPath}");
                        Console.WriteLine($"Query: {query}");

                        if(fragmentRegex.IsMatch(query))
                        {
                            Match fragmentMatch = fragmentRegex.Match(query);
                            string fragment = fragmentMatch.Groups["fragment"].Value;
                            //TODO:

                            Console.WriteLine($"Fragment: {fragment}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Path: {path}");
                    }
                }
            }
        }
    }
}
