using System.Diagnostics;
using Day08_DTO_Mapper;
using Day09_Result.Common;
using Day09_Result.Common.Error;

namespace Day09_Result;

class Program
{
    private static readonly HttpClient client = new HttpClient();
    
    public static async Task Main(string[] args)
    {
        
        IPokemonRepository pokemonRepository = new Repository(new DataSource(client));
        var result = await pokemonRepository.GetPokemonByNameAsync("ditto");
        switch (result)
        {
            case Result<Pokemon, PokemonError>.Success successResult:
                Console.WriteLine($"포켓몬 이름: {successResult.data.Name}");
                Console.WriteLine($"이미지 URL: {successResult.data.OfficialArtworkUrl}");
                break;
            case Result<Pokemon, PokemonError>.Error errorResult:
                switch (errorResult.error)
                {
                    case PokemonError.NotFound:
                        Console.WriteLine("오류: 해당 포켓몬을 찾을수 없음.");
                        break;
                    case PokemonError.NetworkTimeOut:
                        Console.WriteLine("오류: 연결 시간 초과.");
                        break;
                    case PokemonError.AuthenticationFailed:
                        Console.WriteLine("오류: AuthenticationFailed.");
                        break;
                    
                }

            break;
        }
    }
}