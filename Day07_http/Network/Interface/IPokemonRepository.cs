using Day07_http.Network.Models;

namespace Day07_http;

//
public interface IPokemonRepository {
    Task<Pokemon?> GetPokemonByNameAsync(string pokemonName);
}



