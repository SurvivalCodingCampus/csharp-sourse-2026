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
                case 408:
                    return new Result<Pokemon, PokemonError>.Error(PokemonError.RequestTiemOutOfRange);
                default:
                    return new Result<Pokemon, PokemonError>.Error(PokemonError.Unknown);
            }
        }
        catch
        {
            return new Result<Pokemon, PokemonError>.Error(PokemonError.Unknown);
        }
    }
}
