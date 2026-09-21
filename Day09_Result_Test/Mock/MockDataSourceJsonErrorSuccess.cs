using Day09_Result_Pattern.Data.DataSources;

namespace Day09_Result.Mock;


    public class MockDataSourceJsonErrorSuccess : IPokemonApiDataSource3 {
        public Task<Response3> GetPokemonAsync(string name) {
            var json = "{\"id\":132,\"name\":\"ditto\",\"height\":3,\"weight\":40}";
            return Task.FromResult(new Response3(200, json));
        }
}