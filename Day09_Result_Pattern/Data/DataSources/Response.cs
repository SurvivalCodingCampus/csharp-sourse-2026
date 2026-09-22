namespace Day09_Result_Pattern.Data.DataSources;

public class Response<T>
{
    public int StatusCode { get; set; }
    public T? Body { get; set; }
}