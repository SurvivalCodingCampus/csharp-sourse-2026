using System.Collections.Generic;
using System.Threading.Tasks;
using Day09_Result.Data.DataSources;

namespace Day09_Result_Test.Data.DataSource;

public class NotFoundSubwayMockApiDataSource : ISubwayApiDataSource
{
    public Task<Response> GetSubwayAsync(string statnName)
    {
        return Task.FromResult(new Response(
            statusCode: 404,
            headers: new Dictionary<string, string>(),
            body: "{}"
        ));
    }
}