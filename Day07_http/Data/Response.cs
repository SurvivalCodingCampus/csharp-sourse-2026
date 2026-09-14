namespace Day07_http.Data;

// HttpResponseMessage를 감싸는 단순화된 응답 DTO
public class Response
{
    public int StatusCode { get; set; }
    public string Body { get; set; } = string.Empty;
}
