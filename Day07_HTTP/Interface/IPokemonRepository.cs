namespace Day07_HTTP;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
    Task<Pokemon?> GetPokemonByIdAsync(int pokemonId);
    //Task<Pokemon>  GetPokemonBySpriteAsync(string spriteName);
    //Task<Pokemon>  GetPokemonByTypeAsync(string pokemonType);
    //Task<Pokemon>  GetPokemonByStatsAsync(string statName, int statValue);
}