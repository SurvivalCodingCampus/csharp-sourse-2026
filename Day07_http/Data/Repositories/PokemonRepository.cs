using System.Text.Json.Serialization;
using Day07_http.Data.DataSources;
using Day07_http.Models;

namespace Day07_http.Data.Repositories;

public class PokemonRepository : IPokemonRepository
{
    // C# 14. 네트워크 통신 p.42 참조 
    private IPokemonApiDataSource<Pokemon> _pokemonApiDataSource;

    public PokemonRepository(IPokemonApiDataSource<Pokemon> pokemonApiDataSource)
    {
        _pokemonApiDataSource = pokemonApiDataSource;
    }

    public async Task<List<Pokemon>> GetPokemonsAsync()
    {
        //async
        // C# 14. 네트워크 통신 p.45 참조 
        var response = await _pokemonApiDataSource.GetAllAsync();
        return JsonConverter.DeserializeObject<List<Pokemon>>(response) ?? new List<Pokemon>(); //response.body?
        
        
    }

    public Task<Pokemon?> GetPokemonBynameAsync(string name)
    {
        // C# 14. 네트워크 통신 p.46 참조
        throw new NotImplementedException();
    }

    public Task<Pokemon?> CreatePokemonAsync(Pokemon pokemon)
    {
        // C# 14. 네트워크 통신 p.47 참조
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePokemonAsync(Pokemon pokemon)
    {
        // C# 14. 네트워크 통신 p.48 참조
        throw new NotImplementedException();
    }

    public Task<bool> DeletePokemonAsync(string name)
    {
        // C# 14. 네트워크 통신 p.49 참조
        throw new NotImplementedException();
    }
}