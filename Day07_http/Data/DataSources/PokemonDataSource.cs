using Day07_http.Data.Interfaces;
using Day07_http.Data.Mappers;

namespace Day07_http.Data.DataSources;

public class PokemonDataSource : IPokemonApiDataSource
{
    private readonly HttpClient _httpClient;

    public PokemonDataSource()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/")
        };
    }

    public async Task<Response> GetPokemonAsync(string pokemonName)
    {
        var httpResponse = await _httpClient.GetAsync($"pokemon/{pokemonName.ToLowerInvariant()}");

        return await httpResponse.ToResponseAsync();
    }
}
