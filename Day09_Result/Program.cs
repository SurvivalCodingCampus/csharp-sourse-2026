using Day09_Result.Common;
using Day09_Result.Common.Error;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Interfaces;
using Day09_Result.Data.Models;
using Day09_Result.Data.Repository;

namespace Day09_Result;

class Program
{
    static async Task Main(string[] args)
    {
        // IPokemonRepository repo = new PokemonRepository(new PokemonApiDataSource(new HttpClient()));
        //
        // var text = await Console.In.ReadLineAsync();
        //
        // Result<Pokemon, PokemonError> pokemon = await repo.GetPokemonByNameAsync(text ?? "");
        //
        // Console.WriteLine(pokemon?.ToString());

        ISubwayArrivalRepository repo = new SubwayArrivalRepository(new SubwayDataSource(new HttpClient()));

        var text = await Console.In.ReadLineAsync();
        
        Result<SubwayArrival, SubwayArrivalError> result = await repo.GetArrivalByNameAsync(text ?? "1");
        
        Console.WriteLine(result.ToString());
    }
}