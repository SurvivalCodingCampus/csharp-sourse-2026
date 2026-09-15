using Day07_http.Data.DataSources;
using Day07_http.Data.Interfaces;
using Day07_http.Data.Models;
using Day07_http.Data.Repositories;

namespace Day07_http;

class Program
{
    static async Task Main(string[] args)
    {
        IPokemonApiDataSource dataSource = new PokemonApiDataSource(new HttpClient());
        IPokemonRepository repository = new PokemonRepository(dataSource);
        
        Pokemon? pokemon = await repository.GetPokemonByNameAsync("pikachu");
        
        Console.WriteLine(pokemon?.Name);
        Console.WriteLine(pokemon?.OfficialArtwork?.FrontDefault);
    }
}