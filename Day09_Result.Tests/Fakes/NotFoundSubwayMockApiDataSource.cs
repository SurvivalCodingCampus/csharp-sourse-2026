using Day09_Result.Data.DataSources;
using Day09_Result.Data.DTOs;

namespace Day09_Result_Test.Data.DataSource;

public sealed class NotFoundSubwayMockApiDataSource : ISubwayApiDataSource
{
    public Task<SubwayResponseDto> GetArrivalsAsync(string stationName, CancellationToken cancellationToken = default) =>
        Task.FromResult(new SubwayResponseDto
        {
            ErrorMessage = new ApiErrorDto { Status = 200, Code = "INFO-200", Message = "해당하는 데이터가 없습니다." }
        });
}