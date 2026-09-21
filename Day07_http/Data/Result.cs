namespace Day07_http.Data;

public enum ErrorType
{
    None = 0,     
    NotFound,      
    Timeout,       
    ParsingError,  
    EmptyResponse, 
    Unknown        
}

public class Result<T>
{
    public bool IsSuccess { get; }

    public T? Value { get; }

    public string? Error { get; }

    public int? StatusCode { get; }

    public ErrorType ErrorType { get; }

    private Result(bool isSuccess, T? value, string? error, int? statusCode, ErrorType errorType)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        StatusCode = statusCode;
        ErrorType = errorType;
    }

    public static Result<T> Success(T value) => new(true, value, null, null, ErrorType.None);

    public static Result<T> Failure(string error, ErrorType errorType = ErrorType.Unknown, int? statusCode = null) =>
        new(false, default, error, statusCode, errorType);
}
