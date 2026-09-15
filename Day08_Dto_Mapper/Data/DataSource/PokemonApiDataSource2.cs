using Day08_Dto_Mapper.Data.DataSources;

namespace Day08_Dto_MapperData.DataSources;

public class PokemonApiDataSource2(HttpClient httpClient) : IPokemonApiDataSource2{
    private const string BaseUrl = "https://pokeapi.co/api/v2/pokemon";

    public async Task<Response2> GetPokemonAsync(string pokemonName){
        HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/{pokemonName}");
        return await response.ToResponse();
    }
}