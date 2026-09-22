using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Models;
using Day09_Result.Data.Repositories;

namespace Day09_Result;

class Program
{
    static async Task Main(string[] args)
    {
        IPokemonRepository pokemonRepository = new PokemonRepository(new PokemonApiDataSource(new HttpClient()));

        var result = await pokemonRepository.GetPokemonByNameAsync("dittooo");
        switch (result)
        {
            case Result<Pokemon, PokemonError>.Success successResult:
                Console.WriteLine($"포켓몬 이름: {successResult.data.Name}");
                Console.WriteLine($"이미지URL: {successResult.data.ImageUrl}");
                break;
            case Result<Pokemon, PokemonError>.Error errorResult:
                switch (errorResult.error)
                {
                    case PokemonError.NotFound:
                        Console.WriteLine("오류: 해당 포켓몬을 찾을 수 없습니다.");
                        break;
                    case PokemonError.NetworkTimeout:
                        Console.WriteLine("오류: 네트워크 연결 시간 초과.");
                        break;
                    default:
                        Console.WriteLine("알 수 없는 오류가 발생했습니다.");
                        break;
                }

                break;
        }
    }
}