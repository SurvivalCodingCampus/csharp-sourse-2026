using Day07_http.Data.Interfaces;

namespace Day07_http.Data.DataSources;

public class PokemonApiDataSource(HttpClient httpClient) : IPokemonApiDataSource
{
    private const string BaseUrl = "https://pokeapi.co/api/v2/pokemon";

    public async Task<Response> GetPokemonAsync(string pokemonName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/{pokemonName}");
        return new Response((int)response.StatusCode, response.Headers.ToDictionary(
            header => header.Key,
            header => string.Join(", ", header.Value)
        ), await response.Content.ReadAsStringAsync());
    }
}