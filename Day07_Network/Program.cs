using Day07_Network.Data.DataSources;
using Day07_Network.Data.Interfaces;

namespace Day07_Network;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        IPokemonApiDataSource pokemonApiDataSource = new PokemonApiDataSource(new HttpClient());
        IPokemonRepository repository = new PokemonRepository(pokemonApiDataSource);

        Console.WriteLine(repository.GetPokemonByNameAsync("ZapDos").Result);
    }
}