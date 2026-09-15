using System.Net.Http.Json;

namespace Day07_Pokemon;

public class PokemonApiDataSource : IPokemonApiDataSource
{
    private readonly HttpClient _httpClient;

    public PokemonApiDataSource(HttpClient httpClient)
    {
        _httpClient = httpClient;

        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }
    }

    public async Task<PokemonApiResponse?> GetPokemonAsync(string pokemonName)
    {
        var formattedName = pokemonName.Trim().ToLowerInvariant();
        var response = await _httpClient.GetAsync($"pokemon/{formattedName}");
        return await response.Content.ReadFromJsonAsync<PokemonApiResponse>();
    }
}