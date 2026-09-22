using Day09_Result.Error;

namespace Day09_Result.Common;

//1. Result패턴
public abstract record Result3<TData, TError> {
    //abstract(추상)이 있으므로 Result생성자는 없어도 되지만 확실하게 매듬짓기위함
    private Result3() { }

    public sealed record Success(TData S) : Result3<TData, TError>;
    public sealed record Failure(TError F) : Result3<TData, TError> {
        //public PokemonError3 Error { get; set; }
    }
}