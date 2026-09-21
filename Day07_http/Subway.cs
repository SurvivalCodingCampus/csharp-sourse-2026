using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Day07_http.Data;

namespace Day07_http;

public class Subway
{
    private const string ApiKey = "sample";
    private static readonly HttpClient HttpClient = new();

    public async Task<Result<List<SubwayArrival>>> GetArrivalsAsync(string stationName)
    {
        var encodedName = Uri.EscapeDataString(stationName);
        var url = $"http://swopenapi.seoul.go.kr/api/subway/{ApiKey}/json/realtimeStationArrival/0/5/{encodedName}";

        string json;
        try
        {
            json = await HttpClient.GetStringAsync(url);
        }
        catch (Exception ex) when (ex is TimeoutException or TaskCanceledException)
        {
            return BuildFailure(-1, stationName);
        }
        catch (HttpRequestException ex)
        {
            var httpStatus = ex.StatusCode.HasValue ? (int)ex.StatusCode.Value : -2;
            return BuildFailure(httpStatus, stationName);
        }

        var statusCode = ResolveStatusCode(json);

        return statusCode switch
        {
            200 => ParseArrivals(json, stationName),
            404 => BuildFailure(404, stationName),
            -1 => BuildFailure(-1, stationName),
            _ => BuildFailure(statusCode, stationName)
        };
    }

    private static int ResolveStatusCode(string json)
    {
        try
        {
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            if (root.TryGetProperty("errorMessage", out var errorMessageElement))
            {
                var code = errorMessageElement.GetProperty("code").GetString();
                return code == "INFO-000" ? 200 : 404;
            }

            if (root.TryGetProperty("code", out _))
            {
                return 404;
            }

            return -2;
        }
        catch (JsonException)
        {
            return -2;
        }
    }

    private static Result<List<SubwayArrival>> ParseArrivals(string json, string stationName)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("realtimeArrivalList", out var listElement)
            || listElement.ValueKind != JsonValueKind.Array
            || listElement.GetArrayLength() == 0)
        {
            return Result<List<SubwayArrival>>.Failure(
                $"'{stationName}' 역의 도착 정보가 없습니다.", ErrorType.EmptyResponse, 200);
        }

        var arrivals = JsonSerializer.Deserialize<List<SubwayArrival>>(listElement.GetRawText()) ?? [];
        return Result<List<SubwayArrival>>.Success(arrivals);
    }

    private static Result<List<SubwayArrival>> BuildFailure(int statusCode, string stationName) => statusCode switch
    {
        404 => Result<List<SubwayArrival>>.Failure(
            $"'{stationName}' 역 정보를 찾을 수 없습니다. 역 이름을 다시 확인해 주세요.", ErrorType.NotFound, 404),
        -1 => Result<List<SubwayArrival>>.Failure(
            "요청 시간이 초과되었습니다.", ErrorType.Timeout, -1),
        _ => Result<List<SubwayArrival>>.Failure(
            $"알 수 없는 오류입니다. (코드: {statusCode})", ErrorType.Unknown, statusCode)
    };

    public async Task PrintArrivalsAsync(string stationName)
    {
        var result = await GetArrivalsAsync(stationName);

        if (!result.IsSuccess)
        {
            Console.WriteLine($"에러 발생: {result.Error}");
            return;
        }

        Console.WriteLine($"===== '{stationName}' 역 실시간 도착 정보 =====");
        foreach (var arrival in result.Value!)
        {
            Console.WriteLine($"[{arrival.UpdnLine}] {arrival.TrainLineNm} - {arrival.ArvlMsg2} ({arrival.ArvlMsg3})");
        }
    }
}

public class SubwayArrival
{
    [JsonPropertyName("subwayId")]
    public string? SubwayId { get; set; }

    [JsonPropertyName("updnLine")]
    public string? UpdnLine { get; set; }

    [JsonPropertyName("trainLineNm")]
    public string? TrainLineNm { get; set; }

    [JsonPropertyName("statnNm")]
    public string? StatnNm { get; set; }

    [JsonPropertyName("arvlMsg2")]
    public string? ArvlMsg2 { get; set; }

    [JsonPropertyName("arvlMsg3")]
    public string? ArvlMsg3 { get; set; }
}
