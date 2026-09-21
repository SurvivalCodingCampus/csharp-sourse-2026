namespace Day08_DTO_Mapper.Data.DataSources;

public class Response(int statusCode, Dictionary<string, string> headers, string body)
{
    public int StatusCode { get; } = statusCode;
    public IReadOnlyDictionary<string, string> Headers { get; } = headers;
    public String Body { get; } = body;
}