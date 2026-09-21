namespace Day07_http.Data.Common;

public abstract record Result<TData, TError>
{
    public sealed record Success(TData Data) : Result<TData, TError>;
    public sealed record Failure(TError Error) : Result<TData, TError>;
}