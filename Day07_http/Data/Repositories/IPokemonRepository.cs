using Day07_http.Models;

namespace Day07_http.Data.Repositories;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
}