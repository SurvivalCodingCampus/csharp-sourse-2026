using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day09_Result.Data.DataSources;

namespace Day09_Result_Test.Data.DataSource;

public class ErrorMockApiDataSource : IPokemonApiDataSource
{
    private readonly ErrorType _errorType;

    public enum ErrorType
    {
        Timeout,
        JsonSerialization
    }
    
    public ErrorMockApiDataSource(ErrorType errorType)
    {
        _errorType = errorType;
    }
    
    public Task<Response> GetPokemonAsync(string pokemonName)
    {
        switch (_errorType)
        {
            case ErrorType.Timeout:
                throw new TimeoutException("The operation has timed out.");
                
            case ErrorType.JsonSerialization:
                return Task.FromResult(new Response(
                    statusCode: 200,
                    headers: new Dictionary<string, string>(),
                    body: "{ broken_json_data }" // 역직렬화 단계에서 예외를 유발하는 잘못된 바디
                ));
                
            default:
                throw new NotImplementedException();
        }
    }
}