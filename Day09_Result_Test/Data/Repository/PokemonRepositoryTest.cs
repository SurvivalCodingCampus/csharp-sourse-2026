using System;
using System.Threading.Tasks;
using Day09_Result_Test.Data.DataSource;
using Day09_Result.Common;
using Day09_Result.Common.Error;
using Day09_Result.Data.Interfaces;
using Day09_Result.Data.Models;
using Day09_Result.Data.Repository;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Day09_Result_Test.Data.Repository;

[TestFixture]
[TestOf(typeof(PokemonRepository))]
public class PokemonRepositoryTest
{

[Test]
    public void 기존_Mock에서_TimeoutException이_발생한다()
    {
        var mock = new MockDataSource();

        Assert.ThrowsAsync<TimeoutException>(async () =>
        {
            await mock.GetPokemonAsyncTimeoutException(
                "pikachu",
                PokemonError.NetworkTimeout);
        });
    }

    [Test]
    public void 기존_Mock에서_JsonSerializationException이_발생한다()
    {
        var mock = new MockDataSource();

        Assert.ThrowsAsync<JsonSerializationException>(async () =>
        {
            await mock.GetPokemonAsyncJsonSerializationException(
                "pikachu",
                PokemonError.SerializationFailed);
        });
    }

    [Test]
    public async Task TimeoutException을_NetworkTimeout으로_변환한다()
    {
        // Given: 기존 Mock을 Repository에 연결한다.
        IPokemonApiDataSource source = new TimeoutMockAdapter();
        IPokemonRepository repository = new PokemonRepository(source);

        // When
        Result<Pokemon, PokemonError> result =
            await repository.GetPokemonByNameAsync("pikachu");

        // Then: 실패 결과와 오류 종류를 함께 확인한다.
        Assert.That(
            result,
            Is.EqualTo(
                new Result<Pokemon, PokemonError>.Failure(
                    PokemonError.NetworkTimeout)));
    }

    [Test]
    public async Task JsonSerializationException을_SerializationFailed로_변환한다()
    {
        // Given
        IPokemonApiDataSource source = new SerializationMockAdapter();
        IPokemonRepository repository = new PokemonRepository(source);

        // When
        Result<Pokemon, PokemonError> result =
            await repository.GetPokemonByNameAsync("pikachu");

        // Then
        Assert.That(
            result,
            Is.EqualTo(
                new Result<Pokemon, PokemonError>.Failure(
                    PokemonError.SerializationFailed)));
    }

    // 기존 Mock의 타임아웃 메서드를 인터페이스에 연결한다.
    private sealed class TimeoutMockAdapter : IPokemonApiDataSource
    {
        private readonly MockDataSource _mock = new MockDataSource();

        public Task<Response> GetPokemonAsync(string pokemonName)
        {
            return _mock.GetPokemonAsyncTimeoutException(
                pokemonName,
                PokemonError.NetworkTimeout);
        }
    }

    // 기존 Mock의 JSON 예외 메서드를 인터페이스에 연결한다.
    private sealed class SerializationMockAdapter : IPokemonApiDataSource
    {
        private readonly MockDataSource _mock = new MockDataSource();

        public Task<Response> GetPokemonAsync(string pokemonName)
        {
            return _mock.GetPokemonAsyncJsonSerializationException(
                pokemonName,
                PokemonError.SerializationFailed);
        }
    }
}