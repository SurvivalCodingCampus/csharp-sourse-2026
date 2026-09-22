using System.Text.Json;
using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Mapper;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repositories;

public class SubwayRepository(ISubwayApiDataSource dataSource) : ISubwayRepository
{
    public async Task<Result<List<Subway>, SubwayError>> GetSubwayByNameAsync(string statnName)
    {
        try
        {
            Response response = await dataSource.GetSubwayAsync(statnName);

            switch (response.StatusCode)
            {
                case 200:
                    SubwayResponseDto? dto = JsonSerializer.Deserialize<SubwayResponseDto>(response.Body);
                    var subways = dto?.RealtimeArrivalList.ToModel() ?? new List<Subway>();
                    return new Result<List<Subway>, SubwayError>.Success(subways);
                case 404:
                    return new Result<List<Subway>, SubwayError>.Error(SubwayError.NotFound);
                case -1:
                    return new Result<List<Subway>, SubwayError>.Error(SubwayError.NetworkTimeout);
                default:
                    return new Result<List<Subway>, SubwayError>.Error(SubwayError.Unknown);
            }
        }
        catch (Exception)
        {
            return new Result<List<Subway>, SubwayError>.Error(SubwayError.Unknown);            
        }
    }
}