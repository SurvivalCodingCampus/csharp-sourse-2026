using Day09_Result.Data.Models;

namespace Day09_Result.Data.DataSources;

public interface ISubwayApiDataSource
{
    Task<Response> GetSubwayAsync(string statnName);
}