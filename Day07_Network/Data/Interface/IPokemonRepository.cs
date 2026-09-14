using Day07_Network.Model;

namespace Day07_Network.Data.Interface;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
}