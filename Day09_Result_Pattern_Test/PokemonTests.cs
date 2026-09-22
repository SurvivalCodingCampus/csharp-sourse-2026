using Day09_Result_Pattern_Test.Mocks;
using Day09_Result_Pattern.Data.Common;
using Day09_Result_Pattern.Data.Common.Errors;
using Day09_Result_Pattern.Data.Models;
using Day09_Result_Pattern.Data.Repositories;

namespace Day09_Result_Pattern_Test;

public class PokemonTests
{
    [Test]
    public async Task TimeoutException이_발생하면_NetworkTimeout을_반환()
    {
        var dataSource =
            new MockTimeoutDataSource();

        var repository =
            new PokemonRepository(dataSource);

        var result =
            await repository.GetPokemonByNameAsync(
                "dittooo"
            );

        Assert.That(
            result,
            Is.TypeOf<Result<Pokemon, PokemonError>.Error>()
        );

        var errorResult =
            (Result<Pokemon, PokemonError>.Error)result;

        Assert.That(
            errorResult.error,
            Is.EqualTo(PokemonError.NetworkTimeout)
        );
    }

    [Test]
    public async Task JsonSerializationException이_발생하면_JsonSerializationError를_반환()
    {
        var dataSource =
            new MockJsonSerializationDataSource();

        var repository =
            new PokemonRepository(dataSource);

        var result =
            await repository.GetPokemonByNameAsync(
                "dittooo"
            );

        Assert.That(
            result,
            Is.TypeOf<Result<Pokemon, PokemonError>.Error>()
        );

        var errorResult =
            (Result<Pokemon, PokemonError>.Error)result;

        Assert.That(
            errorResult.error,
            Is.EqualTo(PokemonError.JsonSerializationError)
        );
    }
}