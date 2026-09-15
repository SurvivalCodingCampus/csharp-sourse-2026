using Day07_http.Network.Interface;

namespace Day07_http.Network.DataSources;

public class MockPokemonApiDataSource : IPokemonApiDataSource {
    public Task<Response> GetPokemonAsync(string pokemonName) {
        // 서버 대신 내가 원하는 가짜 JSON을 바로 리턴
        string fakeJson = "{\"name\":\"raichu\"}";
        var fakeResponse = new Response(200, new Dictionary<string, string>(), fakeJson);
        return Task.FromResult(fakeResponse);
    }
}