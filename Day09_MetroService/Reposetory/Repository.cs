using Day09_MetroService.Common;
using Day09_MetroService.Common.Error;
using Day09_MetroService.DataSource;
using Day09_MetroService.Mapper;
using Day09_MetroService.Model;
using Newtonsoft.Json;
namespace Day09_MetroService.Reposetory;
public sealed class Repository(IMetroApiDataSource dataSource) : IMetroRepository
{
    public async Task<Result<IReadOnlyList<Metro>, MetroError>> GetByStationNameAsync(string stationName)
    {
        if (string.IsNullOrWhiteSpace(stationName)) return Fail(MetroError.InvalidStationName);
        try
        {
            var response = await dataSource.GetByNameAsync(stationName.Trim());
            if (response.StatusCode is < 200 or >= 300)
                return Fail(response.StatusCode switch
                {
                    401 or 403 => MetroError.AuthenticationFailed,
                    404 => MetroError.NotFound,
                    408 or 504 => MetroError.NetworkTimeOut,
                    429 => MetroError.RateLimitExceeded,
                    >= 500 => MetroError.ServiceUnavailable,
                    _ => MetroError.Unknown
                });
            var body = response.Body;
            if (body is null) return Fail(MetroError.InvalidResponse);
            var code = body.ErrorMessage?.Code ?? body.Code;
            if (!string.IsNullOrWhiteSpace(code) && code != "INFO-000")
                return Fail(code switch
                {
                    "INFO-100" => MetroError.AuthenticationFailed,
                    "INFO-200" => MetroError.NoData,
                    "ERROR-500" or "ERROR-600" or "ERROR-601" => MetroError.ServiceUnavailable,
                    "ERROR-300" or "ERROR-301" or "ERROR-310" or "ERROR-331" or
                    "ERROR-332" or "ERROR-333" or "ERROR-334" or "ERROR-335" or
                    "ERROR-336" => MetroError.InvalidRequest,
                    _ => MetroError.Unknown
                });
            if (body.Arrivals is null) return Fail(MetroError.InvalidResponse);
            var arrivals = body.Arrivals.Where(x => x is not null)
                .Select(x => x!.ToModel()).ToList();
            return arrivals.Count == 0 ? Fail(MetroError.NoData)
                : new Result<IReadOnlyList<Metro>, MetroError>.Success(arrivals);
        }
        catch (OperationCanceledException) { return Fail(MetroError.NetworkTimeOut); }
        catch (HttpRequestException) { return Fail(MetroError.NetworkFailure); }
        catch (JsonException) { return Fail(MetroError.InvalidResponse); }
    }
    private static Result<IReadOnlyList<Metro>, MetroError> Fail(MetroError error) =>
        new Result<IReadOnlyList<Metro>, MetroError>.Error(error);
}
