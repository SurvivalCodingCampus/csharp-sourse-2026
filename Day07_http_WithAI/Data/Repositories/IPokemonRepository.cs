using Day07_http_WithAI.Models;

namespace Day07_http_WithAI.Data.Repositories;

public interface IPokemonRepository
{
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
}