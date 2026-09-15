using Day08_Dto_Mapper.Data.Models;

public interface IPokemonRepository2 {
    Task<Pokemon2?> GetPokemonByNameAsync(string pokemonName);
}