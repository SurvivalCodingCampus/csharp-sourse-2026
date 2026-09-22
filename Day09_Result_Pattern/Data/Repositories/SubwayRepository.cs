using Day09_Result_Pattern.Data.Common;
using Day09_Result_Pattern.Data.Common.Errors;
using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.Mapper;
using Day09_Result_Pattern.Data.Models;

namespace Day09_Result_Pattern.Data.Repositories;

public class SubwayRepository : ISubwayRepository
{
    private readonly ISubwayApiDataSource _dataSource;

    public SubwayRepository(
        ISubwayApiDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Result<List<Subway>, SubwayError>>
        GetArrivalsAsync(string stationName)
    {
        try
        {
            var response =
                await _dataSource.GetArrivalsAsync(
                    stationName
                );

            switch (response.StatusCode)
            {
                case 200:
                    if (response.Body == null)
                    {
                        return new Result<List<Subway>, SubwayError>.Error(
                            SubwayError.InvalidResponse
                        );
                    }

                    if (response.Body.RealtimeArrivalList == null ||
                        response.Body.RealtimeArrivalList.Count == 0)
                    {
                        return new Result<List<Subway>, SubwayError>.Error(
                            SubwayError.StationNotFound
                        );
                    }

                    var subwayList =
                        response.Body.RealtimeArrivalList
                            .Select(dto => dto.ToModel())
                            .ToList();

                    return new Result<List<Subway>, SubwayError>.Success(
                        subwayList
                    );

                case 400:
                    return new Result<List<Subway>, SubwayError>.Error(
                        SubwayError.StationNotFound
                    );

                case -1:
                    return new Result<List<Subway>, SubwayError>.Error(
                        SubwayError.NetworkTimeout
                    );

                case -2:
                    return new Result<List<Subway>, SubwayError>.Error(
                        SubwayError.InvalidResponse
                    );

                default:
                    return new Result<List<Subway>, SubwayError>.Error(
                        SubwayError.Unknown
                    );
            }
        }
        catch
        {
            return new Result<List<Subway>, SubwayError>.Error(
                SubwayError.Unknown
            );
        }
    }
}