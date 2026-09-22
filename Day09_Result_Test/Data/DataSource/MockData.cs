using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Day09_Result.Common;
using Day09_Result.Common.Error;
using Day09_Result.Data.Mapper;
using Newtonsoft.Json;

namespace Day09_Result_Test.Data.DataSource;

public class MockData
{
    public async Task<Response> GetPokemonAsyncTimeoutException(string pokemonName, PokemonError errorType)
    {
        throw new TimeoutException();
    }

    public async Task<Response> GetPokemonAsyncJsonSerializationException(string pokemonName, PokemonError errorType)
    {
        throw new JsonSerializationException();
    }
    
}