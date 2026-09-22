using System;
using System.Net.Http;
using System.Threading.Tasks;
using Day09_Result.Data.Common;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Repositories;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day09_Result.Test.Data.Repositories;

[TestClass]
public class SubwayIntegrationTests
{
    // 실제 서울시 API로 '서울' 역 조회
    [TestMethod]
    public async Task Integration_GetArrivalInfosAsync_RealApi_ReturnsSeoulStationData()
    {
        // [Arrange]
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://swopenapi.seoul.go.kr/api/subway/sample/json/realtimeStationArrival/0/5/")
        };
        var dataSource = new SubwayApiDataSource(httpClient);
        var repository = new SubwayRepository(dataSource);

        // [Act]
        var result = await repository.GetArrivalInfosAsync("서울");

        // [Assert]
        Assert.IsTrue(result.IsSuccess, $"API 호출 실패 사유: {result.ErrorMessage}");
        Assert.IsNotNull(result.Value);
        Assert.IsTrue(result.Value.Count > 0);
        Assert.AreEqual("서울", result.Value[0].StationName);
    }

    // 실제 서울시 API에 존재하지 않는 역명을 보냈을 때 StationNotFound 반환 검증
    [TestMethod]
    public async Task Integration_GetArrivalInfosAsync_WrongStation_ReturnsStationNotFound()
    {
        // [Arrange]
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://swopenapi.seoul.go.kr/api/subway/sample/json/realtimeStationArrival/0/5/")
        };
        var dataSource = new SubwayApiDataSource(httpClient);
        var repository = new SubwayRepository(dataSource);

        // [Act] 존재하지 않는 역
        var result = await repository.GetArrivalInfosAsync("가짜역12345");

        // [Assert]
        Assert.IsTrue(result.IsFailure);
        Assert.AreEqual(SubwayErrorType.StationNotFound, result.ErrorType);
    }
}