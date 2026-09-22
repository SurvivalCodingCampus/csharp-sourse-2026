namespace Day08_DTO_Mapper;
using System.Collections.Generic;


public class Response<T>(int statusCode, Dictionary<string, string> headers, T body)
{
    public int StatusCode { get; } = statusCode;
    public IReadOnlyDictionary<string, string> Headers { get; } = headers;
    public T? Body { get; } = body;
    
}
