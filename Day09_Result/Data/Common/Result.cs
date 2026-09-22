namespace Day09_Result.Data.Common;

public enum PokemonErrorType
{
    None = 0,
    NotFound,
    Timeout,
    SerializationError,
    NetworkError,
    InvalidInput
}
public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string ErrorMessage { get; }
    public PokemonErrorType ErrorType { get; }

    private Result(bool isSuvvess, T? value, string errorMessage, PokemonErrorType errorType)
    {
        IsSuccess = isSuvvess;
        Value = value;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }
    
    public static Result<T> Success(T value) => new(true, value, string.Empty, PokemonErrorType.None);
    public static Result<T> Failure(string errorMessage, PokemonErrorType errorType) => new(false, default, errorMessage, errorType);
}