using Day07_Http.Models;

namespace Day07_Http.Data.Interfaces;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
}