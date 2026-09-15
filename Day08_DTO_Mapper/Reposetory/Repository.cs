using System.Net;
using Day08_DTO_Mapper.Mapper;

namespace Day08_DTO_Mapper;

public class Repository(IPokemonApiDataSource dataSource) : IPokemonRepository
{
    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName)
    {
        var response = await dataSource.GetByNameAsync(pokemonName);
        return MapResponse(response);
    }

    public async Task<Pokemon?> GetPokemonByIdAsync(int pokemonId)
    {
        var response = await dataSource.GetByIdAsync(pokemonId);
        return MapResponse(response);
    }

    private static Pokemon? MapResponse(Response<PokemonDTO> response)
    {
        if (response.StatusCode == 404)
            return null;

        if (response.StatusCode != 200)
            throw new HttpRequestException(
                $"Pokemon request failed. Status code {response.StatusCode}",
                null, (HttpStatusCode)response.StatusCode);

        if (response.Body is null)
            throw new InvalidOperationException("Successful Pokemon response has no body.");

        return response.Body.ToModel();
    }

    public async Task<Pokemon?> GetPokemonByTypeAsync(string pokemonName, string typeName)
    {
        var pokemon = await GetPokemonByNameAsync(pokemonName);
        if (pokemon is null) return null;

        bool hasType = pokemon.Types.Any(
            t => string.Equals(t, typeName, StringComparison.OrdinalIgnoreCase));
        return hasType ? pokemon : null;
    }
}
