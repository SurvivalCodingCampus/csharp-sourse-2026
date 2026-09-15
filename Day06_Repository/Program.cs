using Day06_Repository.DataSources;
using Day06_Repository.Repositories;

namespace Day06_Repository;

class Program
{
    static async Task Main(string[] args)
    {

        using var httpClient = new HttpClient();
        IPokeApiDataSource dataSource = new PokemonApiDataSource(httpClient);
        var repository = new PokemonRepository(dataSource);

        Console.Write("조회할 포켓몬 이름을 입력하세요 (예: pikachu): ");
        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("\n데이터 요청 중...");
            
            var pokemon = await repository.GetPokemonAsync(input);
            
            if (pokemon != null)
            {
                Console.WriteLine($"ID: {pokemon.Id}");
                Console.WriteLine($"이름: {pokemon.Name}");
                Console.WriteLine($"이미지 URL: {pokemon.ImageUrl}");
            }
            else
            {
                Console.WriteLine("\n포켓몬을 찾을 수 없습니다.");
            }
        }
    }
}