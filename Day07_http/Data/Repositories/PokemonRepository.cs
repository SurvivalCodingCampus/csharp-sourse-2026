using Day07_http.Data.Interfaces;
using Day07_http.Models;
using Newtonsoft.Json;

namespace Day07_http.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly IPokemonApiDataSource _dataSource;

    public PokemonRepository(IPokemonApiDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        var response = await _dataSource.GetPokemonAsync(pokemonName);

        if (response.StatusCode < 200 || response.StatusCode >= 300 || string.IsNullOrEmpty(response.Body))
        {
            return null;
        }

        return JsonConvert.DeserializeObject<Pokemon>(response.Body);
    }
}
