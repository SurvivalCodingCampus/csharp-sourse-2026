namespace Day07_http.Data.DataSources;

public class Response
{
    // 응답 코드 200
    //응답 객체: HTTP 통신에서 꼭 필요한 데이터를 캡슐화 하는 객체
    // C# 14. 네트워크 통신 p.40 참조 
    public int StatusCode { get; }
    public IReadOnlyDictionary<string, string> Headers { get; } //헤더 정보들
    public string Body { get; } // Body -> Pokemon

    public Response(int statusCode, IReadOnlyDictionary<string, string> headers, string body)
    {
        StatusCode = statusCode;
        Headers = headers;
        Body = body;
    }
    // C# 14. 네트워크 통신 p.51 참조 
}