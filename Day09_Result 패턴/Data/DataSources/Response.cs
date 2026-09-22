using Newtonsoft.Json;
namespace Day09_Result_패턴.Data.DataSources;

public class Response
{
    // 응답 코드 200, 404, 500 ...
    public int StatusCode { get; }

    // 헤더 정보
    public IReadOnlyDictionary<string, string> Headers { get; }

    // 실제 응답 내용
    public string Body { get; }

    public Response(
        int statusCode,
        Dictionary<string, string> headers,
        string body)
    {
        StatusCode = statusCode;
        Headers = headers;
        Body = body;
    }
    
}