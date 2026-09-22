using Day09_Result.Data.Common;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repositories;

public interface ISubwayRepository
{
    // Result -> SubwayResult로 수정
    Task<SubwayResult<IReadOnlyList<SubwayArrivalInfo>>> GetArrivalInfosAsync(string stationName);
}

public class SubwayRepository : ISubwayRepository
{
    private readonly ISubwayApiDataSource _dataSource;

    public SubwayRepository(ISubwayApiDataSource dataSource)
    {
        _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
    }

    // Result -> SubwayResult로 수정
    public async Task<SubwayResult<IReadOnlyList<SubwayArrivalInfo>>> GetArrivalInfosAsync(string stationName)
    {
        var result = await _dataSource.GetRealtimeArrivalAsync(stationName);

        if (result.IsFailure)
        {
            // Result -> SubwayResult로 수정
            return SubwayResult<IReadOnlyList<SubwayArrivalInfo>>.Failure(result.ErrorMessage, result.ErrorType);
        }

        var list = result.Value!.RealtimeArrivalList!
            .Select(item => item.ToDomain())
            .ToList();

        // Result -> SubwayResult로 수정
        return SubwayResult<IReadOnlyList<SubwayArrivalInfo>>.Success(list);
    }
}