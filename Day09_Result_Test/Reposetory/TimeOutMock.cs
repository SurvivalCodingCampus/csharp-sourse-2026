using System;
using System.Net.Http;
using System.Threading.Tasks;
using Day08_DTO_Mapper;
using Day09_Result.Common;
using Day09_Result.Common.Error;

namespace Day09_Result_Test.Reposetory;

public class TimeOutMockDataSource : IPokemonApiDataSource
{
    public Task<Response<PokemonDTO>> GetByNameAsync(string pokemonName)
    {
        return CreateTimeout();
    }

    public Task<Response<PokemonDTO>> GetByIdAsync(int id)
    {
        return CreateTimeout();
    }

    private static Task<Response<PokemonDTO>> CreateTimeout()
    {
        return Task.FromException<Response<PokemonDTO>>(
            new OperationCanceledException(
                "테스트용 타임아웃.",
                new TimeoutException()
            )
        );
    }
}