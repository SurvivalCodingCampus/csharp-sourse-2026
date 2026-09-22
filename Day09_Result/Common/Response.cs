namespace Day09_Result.Common;

public class Response(int statusCode, Dictionary<string, string> headers, string body)
{
    public int StatusCode { get; } = statusCode;
    public IReadOnlyDictionary<string, string> Headers { get; } = headers;
    public String Body { get; } = body;
}