using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repositories;

public interface ISubwayRepository
{
    Task<Result<IReadOnlyList<Subway>, SubwayError>> GetArrivalsByStationAsync(
        string stationName,
        CancellationToken cancellationToken = default);
}