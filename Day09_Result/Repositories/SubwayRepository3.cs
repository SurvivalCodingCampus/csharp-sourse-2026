using Newtonsoft.Json; 
using Day09_Result.Common;
using Day09_Result.DataSources;
using Day09_Result.Error;
using Day09_Result.Mapper_필요한_것만_꺼내_;
using Day09_Result.Models;

namespace Day09_Result.Repositories;

public class SubwayRepository3 : ISubwayRepository3 {
    private readonly ISubwayApiDataSource3 _dataSource;

    public SubwayRepository3(ISubwayApiDataSource3 dataSource) {
        _dataSource = dataSource;
    }

    public async Task<Result3<List<Subway3>, Error3>> GetArrivalsAsync(string stationName) {
        try {
            var response = await _dataSource.GetArrivalAsync(stationName);
            var dtoResult = SubwayResponseMapper3.ToDto(response);

            if (dtoResult is Result3<SubwayDto3, Error3>.Success success) {
                return new Result3<List<Subway3>, Error3>.Success(SubwayMapper3.ToModels(success.S));
            }

            var failure = (Result3<SubwayDto3, Error3>.Failure)dtoResult;
            return Fail(failure.F);
        }
        catch (TimeoutException) {
            return Fail(Error3.NetworkTimeout);
        }
        catch (JsonException) {
            return Fail(Error3.JsonParsingFailed);
        }
        catch (Exception) {
            return Fail(Error3.Unknown);
        }

        Result3<List<Subway3>, Error3> Fail(Error3 error) {
            return new Result3<List<Subway3>, Error3>.Failure(error);
        }
    }
}