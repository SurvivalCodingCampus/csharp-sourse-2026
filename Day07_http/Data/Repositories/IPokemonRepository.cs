using Day07_http.Data;
using Day07_http.Models;

namespace Day07_http.Data.Interfaces;

public interface IPokemonRepository
{
    Task<Result<Pokemon>> GetPokemonByNameAsync(string pokemonName);
}
