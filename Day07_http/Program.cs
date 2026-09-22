using Day07_http.Data.Common;
using Day07_http.Data.Common.Errors;
using Day07_http.Data.DataSources;
using Day07_http.Data.Interfaces;
using Day07_http.Data.Models;
using Day07_http.Data.Repositories;

namespace Day07_http;

class Program
{
    static async Task Main(string[] args)
    {
        IPokemonApiDataSource dataSource = new PokemonApiDataSource(new HttpClient());
        IPokemonRepository repository = new PokemonRepository(dataSource);
        
        Result<Pokemon, PokemonError> result = await repository.GetPokemonByNameAsync("pikachu");

        switch (result)
        {
            case Result<Pokemon, PokemonError>.Success success:
                Pokemon pokemon = success.Data;
                Console.WriteLine(pokemon.Name);
                Console.WriteLine(pokemon.ImageUrl);
                break;
            case Result<Pokemon, PokemonError>.Failure failure:
                // 패턴 매칭
                string message = failure.Error switch
                {
                    PokemonError.NotFound => "Not found",
                    PokemonError.NetworkTimeout => "Network timeout",
                    _ => "Unknown error"
                };
                Console.WriteLine(message);
                break;
        }
        
        
    }
}