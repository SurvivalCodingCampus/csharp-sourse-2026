using System.Text.Json;
using Day07_http.Data.DataSources;
using Day07_http.Data.Models;
using Day07_http.Data.Repositories;

namespace Day07_http.Data.Interfaces;

public class PokemonRepository(IPokemonApiDataSource dataSource): IPokemonRepository
{
    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        try
        {
            Response response = await dataSource.GetPokemonAsync(pokemonName);

            if (response.StatusCode != 200)
            {
                return null;
            }

            Pokemon? pokemon = JsonSerializer.Deserialize<Pokemon>(response.Body);
            return pokemon;
        } catch (Exception)
        {
            throw new PokemonException();
        }
    }
}

public class PokemonException : Exception;