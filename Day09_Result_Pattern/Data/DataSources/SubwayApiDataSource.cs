using Day09_Result_Pattern.Data.DTO;
using Newtonsoft.Json;

namespace Day09_Result_Pattern.Data.DataSources;

public class SubwayApiDataSource : ISubwayApiDataSource
{
    private const string BaseUrl =
        "http://swopenapi.seoul.go.kr/api/subway/sample/json/realtimeStationArrival/0/5";

    private readonly HttpClient _httpClient;

    public SubwayApiDataSource(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Response<SubwayResponseDto>> GetArrivalsAsync(
        string stationName)
    {
        if (string.IsNullOrWhiteSpace(stationName))
        {
            return new Response<SubwayResponseDto>
            {
                StatusCode = 400,
                Body = null
            };
        }

        try
        {
            var encodedStationName =
                Uri.EscapeDataString(
                    stationName.Trim()
                );

            var url =
                $"{BaseUrl}/{encodedStationName}";

            var response =
                await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new Response<SubwayResponseDto>
                {
                    StatusCode = (int)response.StatusCode,
                    Body = null
                };
            }

            var json =
                await response.Content
                    .ReadAsStringAsync();

            var dto =
                JsonConvert.DeserializeObject<SubwayResponseDto>(
                    json
                );

            return new Response<SubwayResponseDto>
            {
                StatusCode = 200,
                Body = dto
            };
        }
        catch (TaskCanceledException)
        {
            return new Response<SubwayResponseDto>
            {
                StatusCode = -1,
                Body = null
            };
        }
        catch (JsonSerializationException)
        {
            return new Response<SubwayResponseDto>
            {
                StatusCode = -2,
                Body = null
            };
        }
        catch
        {
            return new Response<SubwayResponseDto>
            {
                StatusCode = 0,
                Body = null
            };
        }
    }
}