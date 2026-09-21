using Day09_Result_패턴.Common.Errors.PokemonError;
using Day09_Result_패턴.Data.Common;
using Day09_Result_패턴.Data.Models;
using Day09_Result_패턴.Data.Repositories;
using NUnit.Framework;

namespace Day09_Result_패턴.Tests;

public class PokemonRepositoryTests
{
    [Test]
    public async Task TimeoutException이_발생하면_NetworkTimeout을_반환한다()
    {
        // Arrange
        var dataSource =
            new TimeoutMockDataSource();

        var repository =
            new PokemonRepository(dataSource);

        // Act
        Result<Pokemon, PokemonError> result =
            await repository.GetPokemonByNameAsync("pikachu");

        // Assert
        Assert.That(
            result,
            Is.TypeOf<Result<Pokemon, PokemonError>.Failure>()
        );

        var failure =
            (Result<Pokemon, PokemonError>.Failure)result;

        Assert.That(
            failure.Error,
            Is.EqualTo(PokemonError.NetworkTimeout)
        );
    }


    [Test]
    public async Task JsonSerializationException이_발생하면_Unknown을_반환한다()
    {
        // Arrange
        var dataSource =
            new JsonSerializationMockDataSource();

        var repository =
            new PokemonRepository(dataSource);

        // Act
        Result<Pokemon, PokemonError> result =
            await repository.GetPokemonByNameAsync("pikachu");

        // Assert
        Assert.That(
            result,
            Is.TypeOf<Result<Pokemon, PokemonError>.Failure>()
        );

        var failure =
            (Result<Pokemon, PokemonError>.Failure)result;

        Assert.That(
            failure.Error,
            Is.EqualTo(PokemonError.Unknown)
        );
    }
}