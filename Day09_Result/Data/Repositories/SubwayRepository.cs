using System.Net;
using System.Text.Json;
using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.DTOs;
using Day09_Result.Data.Mapper;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repositories;

public sealed class SubwayRepository(ISubwayApiDataSource dataSource) : ISubwayRepository
{
    private const string NoDataCode = "INFO-200";

    public async Task<Result<IReadOnlyList<Subway>, SubwayError>> GetArrivalsByStationAsync(
        string stationName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(stationName))
            return new Result<IReadOnlyList<Subway>, SubwayError>.Failure(SubwayError.StationNotFound);

        try
        {
            SubwayResponseDto response = await dataSource.GetArrivalsAsync(stationName, cancellationToken);

            // 역 이름 오류 시 서울시 API는 보통 HTTP 200과 INFO-200(데이터 없음)을 함께 반환합니다.
            if (response.ErrorMessage?.Code == NoDataCode)
                return new Result<IReadOnlyList<Subway>, SubwayError>.Failure(SubwayError.StationNotFound);

            IReadOnlyList<Subway> arrivals = (response.RealtimeArrivalList ?? [])
                .Select(arrival => arrival.ToModel())
                .ToList();

            return arrivals.Count == 0
                ? new Result<IReadOnlyList<Subway>, SubwayError>.Failure(SubwayError.StationNotFound)
                : new Result<IReadOnlyList<Subway>, SubwayError>.Success(arrivals);
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
        {
            return new Result<IReadOnlyList<Subway>, SubwayError>.Failure(SubwayError.StationNotFound);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return new Result<IReadOnlyList<Subway>, SubwayError>.Failure(SubwayError.NetworkTimeout);
        }
        catch (HttpRequestException)
        {
            return new Result<IReadOnlyList<Subway>, SubwayError>.Failure(SubwayError.NetworkTimeout);
        }
        catch (JsonException)
        {
            return new Result<IReadOnlyList<Subway>, SubwayError>.Failure(SubwayError.Unknown);
        }
    }
}