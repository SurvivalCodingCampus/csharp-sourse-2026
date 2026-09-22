using Day09_Result.Data.Mapper;

namespace Day09_Result.Data.DataSources;

public class SubwayApiDataSource(HttpClient httpClient) : ISubwayApiDataSource
{
    private const string BaseUrl = "http://swopenapi.seoul.go.kr/api/subway/sample/json/realtimeStationArrival/0/5"; 
    
    public async Task<Response> GetSubwayAsync(string statnName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/{statnName}");
        return await response.ToResponse();
    }
}