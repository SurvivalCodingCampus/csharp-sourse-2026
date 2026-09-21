using Day07_http.Data.DataSources;
using Day07_http.Data.Interfaces;
using Day07_http.Repositories;
using Newtonsoft.Json;

namespace Day07_http;

public class Program
{
    public static async Task Main(string[] args)
    {
        var repository = new PokemonRepository(new PokemonDataSource());

        var result = await repository.GetPokemonByNameAsync("dittooo");

        if (!result.IsSuccess)
        {
            Console.WriteLine($"에러 발생: {result.Error}");
        }
        else
        {
            var pokemon = result.Value!;

            Console.WriteLine($"이름: {pokemon.Name}");
            Console.WriteLine($"높이: {pokemon.Height}");
            Console.WriteLine($"무게: {pokemon.Weight}");
            Console.WriteLine($"타입: {string.Join(", ", pokemon.Types?.Select(t => t.Type?.Name) ?? [])}");
        }

        Console.WriteLine();

        var subway = new Subway();
        await subway.PrintArrivalsAsync("서울");
        await subway.PrintArrivalsAsync("존재하지않는역");
    }
}