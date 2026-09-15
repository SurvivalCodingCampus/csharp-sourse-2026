using Day07_Network.Data;
using Day07_Network.Data.Interfaces;
using Day07_Network.Models;
using Newtonsoft.Json;

namespace Day07_Network.Data.DataSources;

public class PokemonRepository(IPokemonApiDataSource source) : IPokemonRepository
{
    private IPokemonApiDataSource Source { get; set; } = source;

    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        try
        {
            Response response = await Source.GetPokemonAsync(pokemonName);
            if (response.StatusCode != 200)
            {
                throw new PokemonException();
            }

            Pokemon? pokemon = JsonConvert.DeserializeObject<Pokemon>(response.Body);

            return pokemon;
        }
        catch (Exception)
        {
            throw new PokemonException();
        }
    }
}

public class PokemonException : Exception;