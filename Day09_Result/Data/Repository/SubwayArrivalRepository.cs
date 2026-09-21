using System.Text.Json;
using Day09_Result.Common;
using Day09_Result.Common.Error;
using Day09_Result.Data.DTOs;
using Day09_Result.Data.Interfaces;
using Day09_Result.Data.Mapper;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repository;

public class SubwayArrivalRepository(ISubwayDataSource source) : ISubwayArrivalRepository
{
    public async Task<Result<SubwayArrival, SubwayArrivalError>> GetArrivalByNameAsync(string stationName)
    {
        try
        {
            Response response = await source.GetSubwayAsync(stationName);
            if (response.StatusCode == -1)
            {
                return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.NotFound);
            }
            if (response.StatusCode != 200)
            {
                return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.Unknown);
            }

            SubwayArrivalResponseDto subDto = JsonSerializer.Deserialize<SubwayArrivalResponseDto>(response.Body)!;
            switch (subDto.ErrorMessage?.Code ?? subDto.Code ?? "Unknown")
            {
                case "INFO-000":
                    return new Result<SubwayArrival, SubwayArrivalError>.Success(subDto.ToSubwayArrival());
                case "INFO-100":
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.InvalidApiKey);
                case "INFO-200":
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.NotFound);
                case "Unknown":
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.Unknown);
                case "ERROR-500" or "ERROR-600" or "ERROR-601":
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.ServerError);
                case "ERROR-300" or "ERROR-301" or "ERROR-310" or "ERROR-331" or "ERROR-332" or
                    "ERROR-333" or "ERROR-334" or "ERROR-335" or "ERROR-336":
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.InvalidRequest);
                default:
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.Unknown);
            }

            // switch (response.StatusCode)
            // {
            //     case 404:
            //         return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.NotFound);
            //     case 200:
            //         SubwayArrivalResponseDto subDto =
            //             JsonSerializer.Deserialize<SubwayArrivalResponseDto>(response.Body)!;
            //         return new Result<SubwayArrival, SubwayArrivalError>.Success(subDto.ToSubwayArrival());
            //     case 500:
            //         return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.RequestRangeExceeded);
            //     case -1:
            //         return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.NetworkTimeout);
            //     default:
            //         return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.Unknown);
            // }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            switch (e)
            {
                case JsonException:
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError
                        .SerializationFailed);
                case TimeoutException:
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.NetworkTimeout);
                default:
                    return new Result<SubwayArrival, SubwayArrivalError>.Failure(SubwayArrivalError.Unknown);
            }
        }
    }
}