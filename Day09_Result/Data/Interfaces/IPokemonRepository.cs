using Day09_Result.Common;
using Day09_Result.Common.Error;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Interfaces;

public interface IPokemonRepository
{
    Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(string pokemonName);
}