namespace Day08_Dto_Mapper.Data.DataSources;

public class Response2{
    public int StatusCode { get; }
    public IReadOnlyDictionary<string, string> Headers { get; }
    public String Body { get; }

    public Response2(int statusCode, Dictionary<string, string> headers, string body){
        StatusCode = statusCode;
        Headers = headers;
        Body = body;
    }
}