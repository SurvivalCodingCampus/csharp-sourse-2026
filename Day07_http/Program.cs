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

        var pokemon = await repository.GetPokemonByNameAsync("glimmora");

        if (pokemon is null)
        {
            Console.WriteLine("포켓몬 정보를 찾을 수 없습니다.");
            return;
        }
    
        Console.WriteLine($"이름: {pokemon.Name}");
        Console.WriteLine($"높이: {pokemon.Height}");
        Console.WriteLine($"무게: {pokemon.Weight}");
        Console.WriteLine($"타입: {string.Join(", ", pokemon.Types?.Select(t => t.Type?.Name) ?? [])}");
    }
}