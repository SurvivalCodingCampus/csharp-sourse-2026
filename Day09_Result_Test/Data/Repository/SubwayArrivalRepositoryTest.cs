using System;
using System.Threading.Tasks;
using Day09_Result.Common;
using Day09_Result.Common.Error;
using Day09_Result.Data.Models;
using Day09_Result.Data.Repository;
using Day09_Result_Test.Data.DataSource;
using NUnit.Framework;

namespace Day09_Result_Test.Data.Repository;

[TestFixture]
[TestOf(typeof(SubwayArrivalRepository))]
public class SubwayArrivalRepositoryTest
{
    [Test]
    public async Task 정상_응답()
    {
        // Given
        var mock = new MockSubwayDataSource("""
            {
              "errorMessage": {
                "status": 200,
                "code": "INFO-000",
                "message": "정상 처리되었습니다."
              },
              "realtimeArrivalList": [
                {
                  "statnNm": "서울",
                  "bstatnNm": "광운대",
                  "recptnDt": "2026-09-21 14:31:00"
                }
              ]
            }
            """);

        var repository = new SubwayArrivalRepository(mock);

        // When
        var result = await repository.GetArrivalByNameAsync("서울");

        // Then: 현재 구현은 Arrival에 정보 생성 시각을 저장한다.
        Assert.That(result, Is.EqualTo(
            new Result<SubwayArrival, SubwayArrivalError>.Success(
                new SubwayArrival(
                    200, "서울", "광운대", "2026-09-21 14:31:00"))));
    }

    [TestCase("INFO-100", SubwayArrivalError.InvalidApiKey)]
    [TestCase("INFO-200", SubwayArrivalError.NotFound)]
    [TestCase("ERROR-500", SubwayArrivalError.ServerError)]
    [TestCase("ERROR-600", SubwayArrivalError.ServerError)]
    [TestCase("ERROR-601", SubwayArrivalError.ServerError)]
    [TestCase("ERROR-300", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-301", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-310", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-331", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-332", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-333", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-334", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-335", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-336", SubwayArrivalError.InvalidRequest)]
    [TestCase("ERROR-999", SubwayArrivalError.Unknown)]
    public async Task 내부_오류코드_따라_분류(
        string code, SubwayArrivalError expected)
    {
        // HTTP 상태는 200, JSON 본문의 status는 500
        var mock = new MockSubwayDataSource($$"""
            {
              "errorMessage": {
                "status": 500,
                "code": "{{code}}",
                "message": "오류 설명"
              }
            }
            """);

        var repository = new SubwayArrivalRepository(mock);

        var result = await repository.GetArrivalByNameAsync("서울");

        Assert.That(result, Is.EqualTo(
            new Result<SubwayArrival, SubwayArrivalError>.Failure(expected)));
    }

    [TestCase(400)]
    [TestCase(404)]
    [TestCase(500)]
    public async Task HTTP_오류_Unknown_반환(int statusCode)
    {
        var mock = new MockSubwayDataSource(
            "<html>서버 오류</html>", statusCode);

        var repository = new SubwayArrivalRepository(mock);

        var result = await repository.GetArrivalByNameAsync("서울");

        Assert.That(result, Is.EqualTo(
            new Result<SubwayArrival, SubwayArrivalError>.Failure(
                SubwayArrivalError.Unknown)));
    }

    [TestCase("{")]
    [TestCase("")]
    public async Task 값이_이상한_JSON_SerializationFailed_반환(
        string body)
    {
        var mock = new MockSubwayDataSource(body);
        var repository = new SubwayArrivalRepository(mock);

        var result = await repository.GetArrivalByNameAsync("서울");

        Assert.That(result, Is.EqualTo(
            new Result<SubwayArrival, SubwayArrivalError>.Failure(
                SubwayArrivalError.SerializationFailed)));
    }

    [Test]
    public async Task TimeoutException_NetworkTimeout_반환()
    {
        var mock = new MockSubwayDataSource(new TimeoutException());
        var repository = new SubwayArrivalRepository(mock);

        var result = await repository.GetArrivalByNameAsync("서울");

        Assert.That(result, Is.EqualTo(
            new Result<SubwayArrival, SubwayArrivalError>.Failure(
                SubwayArrivalError.NetworkTimeout)));
    }

    [Test]
    public async Task 예상못한예외_Unknown_반환()
    {
        var mock = new MockSubwayDataSource(
            new InvalidOperationException());

        var repository = new SubwayArrivalRepository(mock);

        var result = await repository.GetArrivalByNameAsync("서울");

        Assert.That(result, Is.EqualTo(
            new Result<SubwayArrival, SubwayArrivalError>.Failure(
                SubwayArrivalError.Unknown)));
    }
    
    [Test]
    public async Task 최상위_오류코드_분류()
    {
        var mock = new MockSubwayDataSource("""
            {
              "status": 500,
              "code": "ERROR-336",
              "message": "조회 범위 오류",
              "total": 0
            }
            """);

        var repository = new SubwayArrivalRepository(mock);

        var result = await repository.GetArrivalByNameAsync("서울");

        Assert.That(result, Is.EqualTo(
            new Result<SubwayArrival, SubwayArrivalError>.Failure(
                SubwayArrivalError.InvalidRequest)));
    }
}