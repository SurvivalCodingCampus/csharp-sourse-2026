using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.DTO;

namespace Day09_Result_Pattern_Test.Mocks;

public class MockTimeoutDataSource : IPokemonApiDataSource
{
    public Task<Response<PokemonDto>> GetPokemonAsync(
        string pokemonName)
    {
        throw new TimeoutException();
    }
}