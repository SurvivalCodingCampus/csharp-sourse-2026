using System.Net;
using Day08_DTO_Mapper.Mapper;
using Day09_Result.Common;
using Day09_Result.Common.Error;

namespace Day08_DTO_Mapper;

public class Repository(IPokemonApiDataSource dataSource) : IPokemonRepository
{
    private IPokemonApiDataSource _dataSource = dataSource;

    public async Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(string pokemonName)
    {
        try
        {
            Response<PokemonDTO> response = await _dataSource.GetByNameAsync(pokemonName);
            switch (response.StatusCode)
            {
                case 200:
                    return new Result<Pokemon, PokemonError>.Success(response.Body.ToModel());
                case 404:
                    return new Result<Pokemon, PokemonError>.Error(PokemonError.NotFound);
                case -1:
                    return new Result<Pokemon, PokemonError>.Error(PokemonError.NetworkTimeOut);
                default:
                    return new Result<Pokemon, PokemonError>.Error(PokemonError.Unknown);
            }
        }
        catch
        {
            return new Result<Pokemon, PokemonError>.Error(PokemonError.Unknown);
        }
    }

    public async Task<Result<Pokemon, PokemonError>> GetPokemonByIdAsync(int pokemonId)
    {
        try
        {
            Response<PokemonDTO> response = await _dataSource.GetByIdAsync(pokemonId);
            switch (response.StatusCode)
            {
                case 200:
                    return new Result<Pokemon, PokemonError>.Success(response.Body.ToModel());
                case 404:
                    return new Result<Pokemon, PokemonError>.Error(PokemonError.NotFound);
                case -1:
                    return new Result<Pokemon, PokemonError>.Error(PokemonError.NetworkTimeOut);
                default:
                    return new Result<Pokemon, PokemonError>.Error(PokemonError.Unknown);
            }
        }
        catch
        {
            return new Result<Pokemon, PokemonError>.Error(PokemonError.Unknown);
        }
    }
    /*
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

    public async Task<Result<Pokemon, PokemonError>> GetPokemonByTypeAsync(string pokemonName, string typeName)
    {
        var pokemon = await GetPokemonByNameAsync(pokemonName);
        if (pokemon is null) return null;

        bool hasType = pokemon.Types.Any(
            t => string.Equals(t, typeName, StringComparison.OrdinalIgnoreCase));
        return hasType ? pokemon : null;
    }
    */
}
