namespace Day07_http.Network.DataSources;

public class Response {
// 응답 코드 200, 404, 500
public int StatusCode { get; }
public IReadOnlyDictionary<string, string> Headers { get; } 
public string Body { get; } // LH8


    public Response(int statusCode, Dictionary<string, string> headers, string body) {
        StatusCode = statusCode;
        Headers = headers;
        Body = body;
    }
}