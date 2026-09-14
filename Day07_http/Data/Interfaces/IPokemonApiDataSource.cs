using Day07_http.Data;

namespace Day07_http.Data.Interfaces;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}
