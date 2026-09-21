namespace Day09_Result.Common;

public abstract record Result<TData, TError>
{
    private Result()
    {
    }

    public sealed record Success(TData Data) : Result<TData, TError>;
    public sealed record Failure(TError Error) : Result<TData, TError>;
}