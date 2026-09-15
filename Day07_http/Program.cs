using Day07_http.Data.DataSources;
using Day07_http.Data.Repositories;
using Day07_http.Models;

namespace Day07_http;

class Program
{
    static async Task Main(string[] args)
    {
        PokemonRepository pokemon = new PokemonRepository(new PokemonApiDataSource(new HttpClient()));

        Pokemon? pokemon2 = await pokemon.GetPokemonByNameAsync("pikachu");

        Console.WriteLine(pokemon2.Id);
        Console.WriteLine(pokemon2.Name);
        Console.WriteLine(pokemon2.Sprites);
    }
}