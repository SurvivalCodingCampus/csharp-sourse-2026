using Day08_DTO_Mapper.Data.DataSources;
using Day08_DTO_Mapper.Data.Mapper;
using Day08_DTO_Mapper.Data.Models;

namespace Day08_DTO_Mapper.Data.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly IPokemonApiDataSource _apiDataSource;

    public PokemonRepository(IPokemonApiDataSource apiDataSource)
    {
        _apiDataSource = apiDataSource ?? throw new ArgumentNullException(nameof(apiDataSource));
    }

    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        // 1. 이름 파라미터 유효성 1차 검증
        if (string.IsNullOrWhiteSpace(pokemonName))
        {
            return null;
        }

        // 2. 데이터소스에서 DTO 조회
        var dto = await _apiDataSource.GetPokemonAsync(pokemonName);

        // 3. DTO가 null이면(존재하지 않거나 네트워크 오류) null 반환
        if (dto is null)
        {
            return null;
        }

        // 4. Mapper를 통해 도메인 모델로 변환하여 반환
        return dto.ToDomain();
    }
}