using Day09_Result.Data.Mapper;

namespace Day09_Result.Data.DataSources;

public class PokemonApiDataSource(HttpClient httpClient) : IPokemonApiDataSource
{
    private const string BaseUrl = "https://pokeapi.co/api/v2/pokemon";
    
    public async Task<Response> GetPokemonAsync(string pokemonName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/{pokemonName}");
        return await response.ToResponse();
    }
}