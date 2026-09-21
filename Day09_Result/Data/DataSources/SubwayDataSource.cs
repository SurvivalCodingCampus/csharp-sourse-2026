using Day09_Result.Common;
using Day09_Result.Data.Interfaces;
using Day09_Result.Data.Mapper;

namespace Day09_Result.Data.DataSources;

public class SubwayDataSource(HttpClient httpClient) : ISubwayDataSource
{
    private const string Key = "sample";

    private const string BaseUrl =
        "http://swopenapi.seoul.go.kr/api/subway/" + Key + "/json/realtimeStationArrival/0/5";

    public async Task<Response> GetSubwayAsync(string stationName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/{stationName}");
        return await response.ToResponse();
    }
}