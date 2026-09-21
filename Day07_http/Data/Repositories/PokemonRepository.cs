using Day07_http.Data;
using Day07_http.Data.Dto;
using Day07_http.Data.Interfaces;
using Day07_http.Data.Mappers;
using Day07_http.Models;
using Newtonsoft.Json;

namespace Day07_http.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly IPokemonApiDataSource _dataSource;

    public PokemonRepository(IPokemonApiDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Result<Pokemon>> GetPokemonByNameAsync(string pokemonName)
    {
        Response response;

        
        try
        {
            response = await _dataSource.GetPokemonAsync(pokemonName);
        }
        catch (TimeoutException ex)
        {
            return Result<Pokemon>.Failure($"요청 시간이 초과되었습니다: {ex.Message}", ErrorType.Timeout);
        }
        catch (JsonException ex)
        {
            return Result<Pokemon>.Failure($"응답 데이터를 파싱할 수 없습니다: {ex.Message}", ErrorType.ParsingError);
        }

        if (response.StatusCode < 200 || response.StatusCode >= 300)
        {
            return Result<Pokemon>.Failure(
                $"포켓몬 '{pokemonName}'을(를) 찾을 수 없습니다. (HTTP {response.StatusCode})",
                ErrorType.NotFound,
                response.StatusCode);
        }

        if (string.IsNullOrEmpty(response.Body))
        {
            return Result<Pokemon>.Failure("응답 본문이 비어 있습니다.", ErrorType.EmptyResponse, response.StatusCode);
        }

        PokemonDto? pokemonDto;
        try
        {
            pokemonDto = JsonConvert.DeserializeObject<PokemonDto>(response.Body);
        }
        catch (JsonException ex)
        {
            return Result<Pokemon>.Failure($"응답 데이터를 파싱할 수 없습니다: {ex.Message}", ErrorType.ParsingError, response.StatusCode);
        }

        if (pokemonDto is null)
        {
            return Result<Pokemon>.Failure("응답 데이터를 파싱할 수 없습니다.", ErrorType.ParsingError, response.StatusCode);
        }

        return Result<Pokemon>.Success(pokemonDto.ToModel());
    }
}
