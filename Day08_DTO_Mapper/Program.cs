using Day08_DTO_Mapper.Data.DataSources;
using Day08_DTO_Mapper.Data.Repositores;

public class Program
{
    public static async Task Main()
    {
        var httpClient = new HttpClient();

        var dataSource =
            new PokemonApiDataSource(httpClient);

        var repository =
            new PokemonRepository(dataSource);

        var pokemon =
            await repository.GetPokemonByNameAsync(
                "jigglypuff"
            );

        Console.WriteLine(pokemon);
    }
}