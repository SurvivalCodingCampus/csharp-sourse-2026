using System.Collections.Generic;
using System.Threading.Tasks;
using Day09_Result.Data.DataSources;

namespace Day09_Result_Test.Data.DataSource;

public class SuccessSubwayMockApiDataSource : ISubwayApiDataSource
{
    public Task<Response> GetSubwayAsync(string statnName)
    {
        string validJsonBody = @"{
          ""errorMessage"": {
            ""status"": 200,
            ""code"": ""INFO-000"",
            ""message"": ""정상 처리되었습니다."",
            ""total"": 1
          },
          ""realtimeArrivalList"": [
            {
              ""statnNm"": """ + statnName + @""",
              ""updnLine"": ""상행"",
              ""trainLineNm"": ""검암행 - 공덕방면"",
              ""recptnDt"": ""2026-09-21 13:44:47"",
              ""arvlMsg2"": ""서울 출발""
            }
          ]
        }";

        return Task.FromResult(new Response(
            statusCode: 200,
            headers: new Dictionary<string, string>(),
            body: validJsonBody
        ));
    }
}