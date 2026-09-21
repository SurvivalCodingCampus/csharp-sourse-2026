using Day09_Result.Common;

namespace Day09_Result.Data.Interfaces;

public interface IPokemonApiDataSource
{
    Task<Response> GetPokemonAsync(string pokemonName);
}