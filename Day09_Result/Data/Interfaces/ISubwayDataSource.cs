using Day09_Result.Common;

namespace Day09_Result.Data.Interfaces;

public interface ISubwayDataSource
{
    public Task<Response> GetSubwayAsync(string stationName);
}