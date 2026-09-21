using Day07_http.Data.Interfaces;
using Newtonsoft.Json;

namespace Day07_http.Data.Mocks;

public class JsonErrorMockDataSource : IPokemonApiDataSource
{
    public Task<Response> GetPokemonAsync(string pokemonName)
    {
        throw new JsonSerializationException($"'{pokemonName}' 응답을 역직렬화하는 중 오류가 발생했습니다.");
    }
}
