using System;
using System.Text.Json;
using System.Threading.Tasks;
using Day09_Result.Data.Common;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.DTOs;

namespace Day09_Result.Test.Data.Mocks;

public class TimeoutMockPokemonApiDataSource : IPokemonApiDataSource
{
    public Task<Result<PokemonDto>> GetPokemonAsync(string pokemonName)
    {
        // 비동기 작업 중 타임아웃 예외 발생 시뮬레이션
        throw new TimeoutException("The HTTP request timed out after 10000ms.");
    }
}


public class SerializationErrorMockPokemonApiDataSource : IPokemonApiDataSource
{
    public Task<Result<PokemonDto>> GetPokemonAsync(string pokemonName)
    {
        // JSON 파싱 중 손상된 데이터로 인한 역직렬화 실패 시뮬레이션
        throw new JsonException("The JSON value could not be converted to PokemonDto.");
    }
}