using System.Text;

public class HTTPResponse
{
    public string StatusCode { get; set; }

    public string ResponseText { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"HTTP/1.1 {this.StatusCode}");
        sb.AppendLine($"Content-Length: {this.ResponseText.Length}");
        sb.AppendLine($"Content-Type: text/plain");
        sb.AppendLine("");
        sb.AppendLine($"{this.ResponseText}");

        return sb.ToString().Trim();
    }
}