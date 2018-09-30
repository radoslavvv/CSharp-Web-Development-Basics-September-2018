using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _03.RequestParser
{
    public class RequestParser
    {
        public static void Main(string[] args)
        {
            List<string> allowedPaths = new List<string>();

            string input = Console.ReadLine();
            while (input != "END")
            {
                allowedPaths.Add(input);

                input = Console.ReadLine();
            }

            string[] fullInputRequest = Console.ReadLine()
                .Split(new char[] { ' '},StringSplitOptions.RemoveEmptyEntries);

            string path = $"{fullInputRequest[1]}/{fullInputRequest[0].ToLower()}";

            HTTPResponse httpResponse = new HTTPResponse();
            if (!allowedPaths.Contains(path))
            {
                httpResponse.StatusCode = "404 Not Found";
                httpResponse.ResponseText = "NotFound";
            }
            else
            {
                httpResponse.StatusCode = "200 OK";
                httpResponse.ResponseText = "OK";
            }

            Console.WriteLine(httpResponse);
        }
    }
}
