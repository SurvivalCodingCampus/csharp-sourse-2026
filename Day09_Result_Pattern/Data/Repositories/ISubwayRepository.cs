using Day09_Result_Pattern.Data.Common;
using Day09_Result_Pattern.Data.Common.Errors;
using Day09_Result_Pattern.Data.Models;

namespace Day09_Result_Pattern.Data.Repositories;

public interface ISubwayRepository
{
    Task<Result<List<Subway>, SubwayError>>
        GetArrivalsAsync(string stationName);
}