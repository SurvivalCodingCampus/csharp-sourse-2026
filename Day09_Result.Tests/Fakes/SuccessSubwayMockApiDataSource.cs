using Day09_Result.Data.DataSources;
using Day09_Result.Data.DTOs;

namespace Day09_Result_Test.Data.DataSource;

public sealed class SuccessSubwayMockApiDataSource : ISubwayApiDataSource
{
    public Task<SubwayResponseDto> GetArrivalsAsync(string stationName, CancellationToken cancellationToken = default) =>
        Task.FromResult(new SubwayResponseDto
        {
            RealtimeArrivalList =
            [
                new SubwayArrivalDto
                {
                    SubwayId = "1001", TrainLineName = "인천국제공항철도 - 검암방면",
                    ArrivalMessage = "서울 도착", ArrivalMessageDetail = "서울", UpDownLine = "상행"
                }
            ]
        });
}