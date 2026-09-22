using Day09_MetroService.DTO;
using Newtonsoft.Json;
namespace Day09_MetroService.DataSource;
public sealed class DataSource(HttpClient httpClient, string apiKey) : IMetroApiDataSource
{
    public async Task<Response<MetroDTO>> GetByNameAsync(string stationName)
    {
        var key = Uri.EscapeDataString(apiKey);
        var station = Uri.EscapeDataString(stationName.Trim());
        var end = apiKey == "sample" ? 5 : 100;
        var url = $"http://swopenapi.seoul.go.kr/api/subway/{key}/json/realtimeStationArrival/0/{end}/{station}";
        using var response = await httpClient.GetAsync(url);
        // HTTP 실패 본문은 JSON이 아닐 수 있으므로 역직렬화하지 않습니다.
        if (!response.IsSuccessStatusCode)
            return new Response<MetroDTO>((int)response.StatusCode, null);
        var json = await response.Content.ReadAsStringAsync();
        return new Response<MetroDTO>((int)response.StatusCode,
            JsonConvert.DeserializeObject<MetroDTO>(json));
    }
}
