using Day07_Network.Data.Interface;
using Day07_Network.Model;
using Newtonsoft.Json;

namespace Day07_Network.Data.DataSource;

public class PokemonRepository(IPokemonApiDataSource source) : IPokemonRepository
{
    private IPokemonApiDataSource Source { get; set; } = source;
    
    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        Response response = await source.GetPokemonAsync(pokemonName);
        if (response.StatusCode == 404)
        {
            throw new Exception("Pokemon not found");
        }

        Pokemon? pokemon = JsonConvert.DeserializeObject<Pokemon>(response.Body);

        return pokemon;
    }
}