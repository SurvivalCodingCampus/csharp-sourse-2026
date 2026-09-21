using Day09_Result_Pattern.Data.DataSources;

namespace Day09_Result.DataSources;

public class SubwayApiDataSource3 : ISubwayApiDataSource3 {
private readonly HttpClient _httpClient = new HttpClient {
    Timeout = TimeSpan.FromSeconds(10)
};

public async Task<Response3> GetArrivalAsync(string stationName) {
    try {
        var encoded = Uri.EscapeDataString(stationName);
        var url = $"http://swopenapi.seoul.go.kr/api/subway/sample/json/realtimeStationArrival/0/5/{encoded}";        var res = await _httpClient.GetAsync(url);
        var body = await res.Content.ReadAsStringAsync();
        return new Response3((int)res.StatusCode, body);
    } catch (TaskCanceledException) {
        throw new TimeoutException();
    }
}
}