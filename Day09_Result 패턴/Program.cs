using Day09_Result_패턴.Common.Errors.PokemonError;
using Day09_Result_패턴.Data.Common;
using Day09_Result_패턴.Data.DataSources;
using Day09_Result_패턴.Data.Repositories;
using Day09_Result_패턴.Data.Models;

namespace Day09_Result_패턴;

class Program
{
    static async Task Main(string[] args)
    {
        IPokemonApiDataSource dataSource = new PokemonApiDataSource(new HttpClient());
        IPokemonRepository repository = new PokemonRepository(dataSource);

        Result<Pokemon, PokemonError> result = await repository.GetPokemonByNameAsync("dittoooo");

        switch (result)
        {
            case Result<Pokemon, PokemonError>.Success success:
                Pokemon pokemon = success.Data;
                Console.WriteLine(pokemon.Name);
                Console.WriteLine(pokemon.ImageUrl);
                break;
            case Result<Pokemon, PokemonError>.Failure failure:
                switch (failure.Error)
                {
                    case PokemonError.NotFound:
                        Console.Write("Not Found");
                        break;
                    case PokemonError.NetworkTimeout:
                        Console.Write("Network Timeout");
                        break;
                    case PokemonError.Unknown:
                        Console.Write("Unknown error");
                        break;
                    default:
                        Console.Write("Unknown error");
                        break;
                }
                break;
        }
    }
}