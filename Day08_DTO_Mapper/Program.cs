namespace Day08_DTO_Mapper;

class Program
{
    static async Task Main(string[] args)
    {
        //DataSource Json 수신
        //DTO에서 데이터 옮기기
        //Model에서 정제
        //Repository에서 분류
        IPokemonApiDataSource dataSource = new DataSource(new HttpClient());
        IPokemonRepository repository = new Repository(dataSource);
        
        Pokemon? pokemon = await repository.GetPokemonByNameAsync("pikachu");
        
        Console.WriteLine(pokemon?.Name);
        Console.WriteLine(pokemon?.OfficialArtworkUrl?.ToString());
    }
}