using Day07_Network.Models;

namespace Day07_Network.Data.Interfaces;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
}