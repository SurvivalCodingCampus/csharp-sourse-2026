using Day08_Dto_Mapper.Data.Mapper;

namespace Day08_Dto_Mapper.Data.DataSources;

public class PokemonApiDataSource(HttpClient httpClient) : IPokemonApiDataSource
{
    private const string BaseUrl = "https://pokeapi.co/api/v2/pokemon";
    
    public async Task<Response> GetPokemonAsync(string pokemonName)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/{pokemonName}");
        return await response.ToResponse();
    }
}