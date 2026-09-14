using Day07_http.Models;

namespace Day07_http.Data.Repositories;

public interface IPokemonRepository
{
    // C# 14. 네트워크 통신 p.39 참조 
    Task<List<Pokemon>> GetPokemonsAsync();
    
    Task<Pokemon?> GetPokemonBynameAsync(string name);
    Task<Pokemon?> CreatePokemonAsync(Pokemon pokemon);
    Task<bool> UpdatePokemonAsync(Pokemon pokemon);
    Task<bool> DeletePokemonAsync(string name);
}