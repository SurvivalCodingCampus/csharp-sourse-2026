namespace Day09_Result.Common;

public abstract record class Result<TData, TError>
{
    private Result()
    {
        //
    }

    public sealed record Success(TData data) : Result<TData, TError>;
    public sealed record Error(TError error) : Result<TData, TError>;
}

