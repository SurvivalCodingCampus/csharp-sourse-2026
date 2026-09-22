using Day09_Result_Pattern.Data.Models;
using Day09_Result_Pattern.Data.Common;
using Day09_Result_Pattern.Data.Common.Errors;

namespace Day09_Result_Pattern.Data.Repositories;

public interface IPokemonRepository
{
    Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(
        string pokemonName
    );
}