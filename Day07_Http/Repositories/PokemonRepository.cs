
using Day07_Http.Data.Interfaces;
using Day07_Http.Models;
using Newtonsoft.Json;

namespace Day07_Http.Repositories;

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
        var response =
            await _dataSource.GetPokemonAsync(pokemonName);

        return JsonConvert.DeserializeObject<Pokemon>(
            response.Body
        );
    }
}