using Day09_Result_Pattern.Data.DataSources;

namespace Day09_Result.Mock;

public class MockDataSourceTimeoutExceptionFailure:IPokemonApiDataSource3 {
    public Task<Response3> GetPokemonAsync(string name) {
        return Task.FromException<Response3>(new TimeoutException());
    }
}