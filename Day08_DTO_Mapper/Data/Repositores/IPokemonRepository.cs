using Day08_DTO_Mapper.Data.Models;

namespace Day08_DTO_Mapper.Data.Repositores;

public interface IPokemonRepository
{
    Task<Pokemon> GetPokemonByNameAsync(
        string pokemonName
    );
}