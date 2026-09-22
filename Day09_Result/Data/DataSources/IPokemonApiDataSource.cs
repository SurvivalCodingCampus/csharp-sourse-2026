using Day09_Result.Data.Common;
using Day09_Result.Data.DTOs;

namespace Day09_Result.Data.DataSources;

public interface IPokemonApiDataSource
{
    Task<Result<PokemonDto>> GetPokemonAsync(string pokemonName);
}