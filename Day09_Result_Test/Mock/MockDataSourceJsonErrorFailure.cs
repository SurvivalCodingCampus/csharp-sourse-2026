using Day09_Result_Pattern.Data.DataSources;
using Newtonsoft.Json;

namespace Day09_Result.Mock;


public class MockDataSourceJsonErrorFailure : IPokemonApiDataSource3 {
    public Task<Response3> GetPokemonAsync(string name) {
        return Task.FromException<Response3>(new JsonSerializationException("잘못된 JSON"));
    }
}
