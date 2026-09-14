using Day07_http_WithAI.Data.DataSources;
using Day07_http_WithAI.Data.Repositories;
using Day07_http_WithAI.Models;
using System.Threading.Tasks;

namespace Day07_http_WithAI;

class Program
{
    static async Task Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();

        IPokemonApiDataSource dataSource =
            new PokemonApiDataSource(httpClient);

        IPokemonRepository repository =
            new PokemonRepository(dataSource);

        Pokemon? pokemon =
            await repository.GetPokemonByNameAsync("pikachu");

        if (pokemon == null)
        {
            Console.WriteLine("포켓몬을 찾을 수 없습니다.");
            return;
        }

        Console.WriteLine($"이름 : {pokemon.Name}");

        Console.WriteLine(
            $"기본 이미지 : {pokemon.Sprites?.FrontDefault}"
        );

        Console.WriteLine(
            $"공식 이미지 : " +
            $"{pokemon.Sprites?.Other?.OfficialArtwork?.FrontDefault}"
        );
    }
    
    // HttpClient
    // ↓
    // DataSource
    // ↓
    // Response
    // ↓
    // Repository
    // ↓
    // Pokemon
    
    
}