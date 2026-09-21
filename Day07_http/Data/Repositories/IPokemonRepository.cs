using Day07_http.Data.Common;
using Day07_http.Data.Common.Errors;
using Day07_http.Data.Models;

namespace Day07_http.Data.Repositories;

public interface IPokemonRepository
{
    Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(string pokemonName);
}