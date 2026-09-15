using Day07_http_WithAI.Data.DataSources;
using Day07_http_WithAI.Models;

namespace Day07_http_WithAI.Data.Repositories;

using Newtonsoft.Json;

public class PokemonRepository : IPokemonRepository
{
    private readonly IPokemonApiDataSource _dataSource;

    public PokemonRepository(IPokemonApiDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Pokemon?> GetPokemonByNameAsync(
        string pokemonName)
    {
        Response response =
            await _dataSource.GetPokemonAsync(pokemonName);

        if (response.StatusCode != 200)
        {
            return null;
        }

        Pokemon? pokemon =
            JsonConvert.DeserializeObject<Pokemon>(
                response.Body
            );

        return pokemon;
    }
}