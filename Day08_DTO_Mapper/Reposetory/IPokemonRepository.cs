namespace Day08_DTO_Mapper;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
    Task<Pokemon?> GetPokemonByIdAsync(int pokemonId);

}