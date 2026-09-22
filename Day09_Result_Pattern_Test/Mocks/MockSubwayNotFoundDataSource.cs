using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.DTO;

namespace Day09_Result_Pattern_Test.Mocks;

public class MockSubwayNotFoundDataSource
    : ISubwayApiDataSource
{
    public Task<Response<SubwayResponseDto>> GetArrivalsAsync(
        string stationName)
    {
        var dto = new SubwayResponseDto
        {
            RealtimeArrivalList =
                new List<SubwayArrivalDto>()
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