namespace Day07_Pokemon;

public class PokemonRepository : IPokemonRepository
{
    // API 통신을 담당하는 데이터소스 인터페이스를 주입받아 보관할 필드
    private readonly IPokemonApiDataSource _apiDataSource;

    public PokemonRepository(IPokemonApiDataSource apiDataSource)
    {
        _apiDataSource = apiDataSource;
    }

    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        var apiData = await _apiDataSource.GetPokemonAsync(pokemonName);
        if (apiData == null)
        {
            return null;
        }

        return new Pokemon
        {
            Id = apiData.Id,
            Name = apiData.Name,
            HeightDecimeter = apiData.Height,
            WeightHectogram = apiData.Weight,
            ImageUrl = apiData.Sprites.FrontDefault,
            //문자열 타입 이름만 뽑아서 리스트
            Types = apiData.Types.Select(t => t.Type.Name).ToList()
        };
    }
}