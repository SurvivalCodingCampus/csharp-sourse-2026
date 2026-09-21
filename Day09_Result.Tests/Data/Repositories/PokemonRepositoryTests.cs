using System.Text.Json;
using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.Models;
using Day09_Result.Data.Repositories;
using Day09_Result.Tests.Fakes;
using Xunit;

namespace Day09_Result.Tests.Data.Repositories;

public class PokemonRepositoryTests
{
    [Fact]
    public async Task GetPokemonByNameAsync_WhenDataSourceTimesOut_ReturnsFailure()
    {
        // Arrange: 항상 TimeoutException을 던지는 Fake DataSource
        IPokemonApiDataSource dataSource = new ThrowingPokemonApiDataSource(new TimeoutException());
        IPokemonRepository repository = new PokemonRepository(dataSource);

        // Act
        Result<Pokemon, PokemonError> result = await repository.GetPokemonByNameAsync("dittooo");

        // Assert: 예외가 그대로 튀어나오지 않고 Result.Failure로 감싸져야 한다.
        // (Repository가 catch(Exception) 하나로만 처리하므로 에러 타입은 Unknown으로 떨어진다.)
        var failure = Assert.IsType<Result<Pokemon, PokemonError>.Failure>(result);
        Assert.Equal(PokemonError.Unknown, failure.Error);
    }

    [Fact]
    public async Task GetPokemonByNameAsync_WhenJsonDeserializationFails_ReturnsFailure()
    {
        // Arrange: 항상 JSON 역직렬화 예외(JsonException)를 던지는 Fake DataSource
        IPokemonApiDataSource dataSource = new ThrowingPokemonApiDataSource(new JsonException());
        IPokemonRepository repository = new PokemonRepository(dataSource);

        // Act
        Result<Pokemon, PokemonError> result = await repository.GetPokemonByNameAsync("dittooo");

        // Assert: 이 경우도 마찬가지로 Result.Failure로 감싸지고, 에러 타입은 Unknown이다.
        var failure = Assert.IsType<Result<Pokemon, PokemonError>.Failure>(result);
        Assert.Equal(PokemonError.Unknown, failure.Error);
    }
}