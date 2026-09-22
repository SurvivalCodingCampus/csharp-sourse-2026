using System.Text.Json;
using Day09_Result.Data.Common;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Mapper;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly IPokemonApiDataSource _apiDataSource;

    public PokemonRepository(IPokemonApiDataSource apiDataSource)
    {
        _apiDataSource = apiDataSource ?? throw new ArgumentNullException(nameof(apiDataSource));
    }

    public async Task<Result<Pokemon>> GetPokemonByNameAsync(string pokemonName)
    {
        if (string.IsNullOrWhiteSpace(pokemonName))
        {
            return Result<Pokemon>.Failure("포켓몬 이름이 유효하지 않음", PokemonErrorType.InvalidInput);
        }

        try
        {
            var dataSourceResult = await _apiDataSource.GetPokemonAsync(pokemonName);
            if (dataSourceResult.IsFailure)
            {
                return Result<Pokemon>.Failure(dataSourceResult.ErrorMessage, dataSourceResult.ErrorType);
            }

            var pokemon = dataSourceResult.Value!.ToDomain();
            return Result<Pokemon>.Success(pokemon);
        }
        catch (TimeoutException ex)
        {
            return Result<Pokemon>.Failure($"요청 시간 초과 : {ex.Message}", PokemonErrorType.Timeout);
        }
        catch (JsonException ex)
        {
            return Result<Pokemon>.Failure($"JSON 데이터 파싱 실패 : {ex.Message}", PokemonErrorType.SerializationError);
        }
        catch (Exception ex)
        {
            return Result<Pokemon>.Failure($"예기치 못한 오류 발생 : {ex.Message}", PokemonErrorType.NetworkError);
        }
    }
}