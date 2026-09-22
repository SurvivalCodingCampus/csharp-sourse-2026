namespace Day09_Result.Data.Common;

public enum SubwayErrorType
{
    None = 0,
    StationNotFound,    // 유효하지 않은 역 이름 (데이터 없음)
    InvalidInput,       // 입력값 누락/공백
    Timeout,            // 타임아웃
    SerializationError, // 역직렬화 오류
    NetworkError        // 통신 오류
}

public class SubwayResult<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string ErrorMessage { get; }
    public SubwayErrorType ErrorType { get; }

    private SubwayResult(bool isSuccess, T? value, string errorMessage, SubwayErrorType errorType)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }

    public static SubwayResult<T> Success(T value) 
        => new(true, value, string.Empty, SubwayErrorType.None);

    public static SubwayResult<T> Failure(string errorMessage, SubwayErrorType errorType) 
        => new(false, default, errorMessage, errorType);
}