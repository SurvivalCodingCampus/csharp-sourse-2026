using Day08_DTO_Mapper.Data.DataSources;
using Day08_DTO_Mapper.Data.Interfaces;
using Day08_DTO_Mapper.Data.Models;
using Day08_DTO_Mapper.Data.Repository;

namespace Day08_DTO_Mapper;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        IPokemonRepository repo = new PokemonRepository(new PokemonApiDataSource(new HttpClient()));

        var text = await Console.In.ReadLineAsync();

        Pokemon? pokemon = await repo.GetPokemonByNameAsync(text ?? "");
        
        Console.WriteLine(pokemon?.ToString());
    }
}