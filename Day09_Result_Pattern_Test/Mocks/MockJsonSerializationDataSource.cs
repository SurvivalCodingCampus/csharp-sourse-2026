using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.DTO;
using Newtonsoft.Json;

namespace Day09_Result_Pattern_Test.Mocks;

public class MockJsonSerializationDataSource : IPokemonApiDataSource
{
    public Task<Response<PokemonDto>> GetPokemonAsync(
        string pokemonName)
    {
        throw new JsonSerializationException();
    }
}