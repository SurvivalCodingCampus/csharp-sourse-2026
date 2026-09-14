namespace Day06_Repository.DataSources;

public class Response
{
    public int StatusCode { get; }
    public IReadOnlyDictionary<string, string> Headers { get; }
    public string Body { get; }

    public Response(int statusCode, Dictionary<string, string> headers, string body)
    {
        StatusCode = statusCode;
        Headers = headers;
        Body = body;
    }
}