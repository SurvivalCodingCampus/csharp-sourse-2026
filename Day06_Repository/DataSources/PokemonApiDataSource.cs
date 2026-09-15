namespace Day06_Repository.DataSources;

public class PokemonApiDataSource(HttpClient httpClient) : IPokeApiDataSource
{
    private const string BaseUrl = "https://pokeapi.co/api/v2/pokemon";

    public async Task<Response> GetPokemonAsync(string pokemonName)
    {
        var httpResponse = await httpClient.GetAsync($"{BaseUrl}/{pokemonName.ToLower()}");
        var jsonString = await httpResponse.Content.ReadAsStringAsync();
        var headers = httpResponse.Headers.ToDictionary(
            h => h.Key,
            h => string.Join(", ", h.Value)
        );
        
        return new Response((int)httpResponse.StatusCode, headers, jsonString);
    }
}