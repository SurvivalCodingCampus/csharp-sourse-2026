using System.Text.Json.Serialization;
using Day07_HTTP.obj;
using Newtonsoft.Json;

namespace Day07_HTTP;
using System.Net.Http.Json;

public class PokemonRepository : IPokemonRepository
{
    //Data organization 데이터 정리
    private readonly IPokemonApiDataSource<Pokemon> _apiDataSource;

    public PokemonRepository(IPokemonApiDataSource<Pokemon> apiDataSource)
    {
        _apiDataSource = apiDataSource;
    }

    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        var response = await _apiDataSource.GetByNameAsync(pokemonName);
        return response.StatusCode == 200 ? response.Body : null;
    }

    public async Task<Pokemon?> GetPokemonByIdAsync(int pokemonId)
    {
        var response = await _apiDataSource.GetByIdAsync(pokemonId);
        return response.StatusCode == 200 ? response.Body : null;
    }

    // 단일 포켓몬 정보에서 특정 타입/스탯 조건 검증 예시
    public async Task<Pokemon?> GetPokemonByTypeAsync(string pokemonName, string typeName)
    {
        var pokemon = await GetPokemonByNameAsync(pokemonName);
        if (pokemon?.Types == null) return null;

        bool hasType = pokemon.Types.Any(t => t.Type?.Name?.Equals(typeName, StringComparison.OrdinalIgnoreCase) == true);
        return hasType ? pokemon : null;
    }

}