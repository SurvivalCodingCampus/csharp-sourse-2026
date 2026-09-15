using System.Threading.Tasks;

public class PokemonRepository
{
    private readonly PokemonApiDataSource _dataSource;

    public PokemonRepository(PokemonApiDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Pokemon> GetPokemonAsync(string pokemonName)
    {
        // 1. DataSource를 통해 DTO 가져오기 (통신)
        PokemonDto? dto = await _dataSource.FetchPokemonAsync(pokemonName);

        // 2. Mapper를 사용하여 DTO를 안전한 Model로 변환하여 반환
        return dto.ToModel();
    }
}