using Day09_Result_패턴.Common.Errors.PokemonError;
using Day09_Result_패턴.Data.Common;
using Day09_Result_패턴.Data.Models;

namespace Day09_Result_패턴.Data.Repositories;

public interface IPokemonRepository
{
    //Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
    Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(string pokemonName);
    
}