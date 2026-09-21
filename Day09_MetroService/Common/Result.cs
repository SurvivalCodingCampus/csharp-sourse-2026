namespace Day09_MetroService.Common;
public abstract record Result<TData, TError>
{
    private Result() { }
    public sealed record Success(TData data) : Result<TData, TError>;
    public sealed record Error(TError error) : Result<TData, TError>;
}
