using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.DTO;

namespace Day09_Result_Pattern_Test.Mocks;

public class MockSubwaySuccessDataSource
    : ISubwayApiDataSource
{
    public Task<Response<SubwayResponseDto>> GetArrivalsAsync(
        string stationName)
    {
        var dto = new SubwayResponseDto
        {
            RealtimeArrivalList = new List<SubwayArrivalDto>
            {
                new SubwayArrivalDto
                {
                    ArrivalSeconds = "180",
                    ArrivalMessage = "3분 후",
                    CurrentLocation = "남영",
                    ArrivalCode = "99",
                    Direction = "상행",
                    TrainLineName = "광운대행",
                    TerminalStation = "광운대",
                    StationName = "서울"
                }
            }
        };

        return Task.FromResult(
            new Response<SubwayResponseDto>
            {
                StatusCode = 200,
                Body = dto
            }
        );
    }
}