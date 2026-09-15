using Day07_http.Models;

namespace Day07_http.Data.Interfaces;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
}
