using System.Text.Json;
using Day09_Result.Data.DTOs;

namespace Day09_Result.Data.DataSources;

public sealed class SubwayApiDataSource(HttpClient httpClient) : ISubwayApiDataSource
{
    private const string BaseUrl =
        "http://swopenapi.seoul.go.kr/api/subway/sample/json/realtimeStationArrival/0/5";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<SubwayResponseDto> GetArrivalsAsync(
        string stationName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(stationName);

        using HttpResponseMessage response = await httpClient.GetAsync(
            $"{BaseUrl}/{Uri.EscapeDataString(stationName.Trim())}", cancellationToken);

        response.EnsureSuccessStatusCode();
        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        return await JsonSerializer.DeserializeAsync<SubwayResponseDto>(stream, JsonOptions, cancellationToken)
               ?? throw new JsonException("서울시 API 응답을 읽을 수 없습니다.");
    }
}