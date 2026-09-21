using Day07_HTTP.obj;

namespace Day07_HTTP;

class Program
{
    static async Task Main(string[] args)
    {
        IPokemonApiDataSource<Pokemon> dataSource = new PokemonApiDataSource(new HttpClient());
        IPokemonRepository repository = new PokemonRepository(dataSource);

        Pokemon? pokemon = await repository.GetPokemonByNameAsync("pikachu");
     
        Console.WriteLine(pokemon?.Name);
        //Console.WriteLine(pokemon?.OfficialArtwork?.FrontDefault);
    }
}