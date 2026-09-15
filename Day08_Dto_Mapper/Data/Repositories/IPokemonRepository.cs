using Day08_Dto_Mapper.Data.Models;

namespace Day08_Dto_Mapper.Data.Repositories;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
}