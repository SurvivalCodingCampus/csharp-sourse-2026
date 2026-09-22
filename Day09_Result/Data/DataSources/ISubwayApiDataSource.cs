using Day09_Result.Data.DTOs;

namespace Day09_Result.Data.DataSources;

public interface ISubwayApiDataSource
{
    Task<SubwayResponseDto> GetArrivalsAsync(
        string stationName,
        CancellationToken cancellationToken = default);
}