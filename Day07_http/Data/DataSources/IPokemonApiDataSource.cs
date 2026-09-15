using Day07_http.Models;

namespace Day07_http.Data.DataSources;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}