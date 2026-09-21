using Day09_Result_Pattern.Data.DataSources;

namespace Day09_Result.DataSources;

public interface ISubwayApiDataSource3 {
    Task<Response3> GetArrivalAsync(string stationName);

}