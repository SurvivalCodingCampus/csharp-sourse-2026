using Day07_http.Data.Interfaces;

namespace Day07_http.Data.Mocks;

public class TimeoutMockDataSource : IPokemonApiDataSource
{
    public Task<Response> GetPokemonAsync(string pokemonName)
    {
        throw new TimeoutException($"'{pokemonName}' 요청이 시간 초과되었습니다.");
    }
}
