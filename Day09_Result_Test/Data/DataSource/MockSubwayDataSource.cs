using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day09_Result.Common;
using Day09_Result.Data.Interfaces;

namespace Day09_Result_Test.Data.DataSource;

public class MockSubwayDataSource : ISubwayDataSource
{
    private readonly Response _response;
    private readonly Exception _exception;

    // 지정한 HTTP 상태와 JSON 본문을 반환한다.
    public MockSubwayDataSource(string body, int statusCode = 200)
    {
        _response = new Response(
            statusCode,
            new Dictionary<string, string>(),
            body);
    }

    // 네트워크 오류 등의 예외를 재현한다.
    public MockSubwayDataSource(Exception exception)
    {
        _exception = exception;
    }

    public Task<Response> GetSubwayAsync(string stationName)
    {
        if (_exception != null)
        {
            return Task.FromException<Response>(_exception);
        }

        return Task.FromResult(_response);
    }
}