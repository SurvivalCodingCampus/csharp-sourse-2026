using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.DTO;
using Day09_Result_Pattern.Data.Models;
using Day09_Result_Pattern.Data.Common;
using Day09_Result_Pattern.Data.Common.Errors;
using Day09_Result_Pattern.Data.Mapper;
using Newtonsoft.Json;

namespace Day09_Result_Pattern.Data.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private IPokemonApiDataSource _dataSource;

    public PokemonRepository(IPokemonApiDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Result<Pokemon, PokemonError>> GetPokemonByNameAsync(
        string pokemonName)
    {
        try
        {
            Response<PokemonDto> response =
                await _dataSource.GetPokemonAsync(pokemonName);

            switch (response.StatusCode)
            {
                case 200:
                    return new Result<Pokemon, PokemonError>.Success(
                        response.Body.ToModel()
                    );

                case 404:
                    return new Result<Pokemon, PokemonError>.Error(
                        PokemonError.NotFound
                    );

                case -1:
                    return new Result<Pokemon, PokemonError>.Error(
                        PokemonError.NetworkTimeout
                    );

                default:
                    return new Result<Pokemon, PokemonError>.Error(
                        PokemonError.Unknown
                    );
            }
        }
        catch (TimeoutException)
        {
            return new Result<Pokemon, PokemonError>.Error(
                PokemonError.NetworkTimeout
            );
        }
        catch (JsonSerializationException)
        {
            return new Result<Pokemon, PokemonError>.Error(
                PokemonError.JsonSerializationError
            );
        }
        catch (Exception e)
        {
            return new Result<Pokemon, PokemonError>.Error(
                PokemonError.Unknown
            );
        }
    }
}