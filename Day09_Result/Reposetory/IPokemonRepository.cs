using Day09_Result.Common;
using Day09_Result.Common.Error;

namespace Day08_DTO_Mapper;

public interface IPokemonRepository
{
    Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(string pokemonName);
    Task<Result<Pokemon, PokemonError>> GetPokemonByIdAsync(int pokemonId);
    
   

}