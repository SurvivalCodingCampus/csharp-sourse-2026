using Day09_Result_Pattern.Data.Models;
using Day09_Result.Common;
using Day09_Result.Error;
using Day09_Result.Mock;
using Day09_Result.Repositories;

namespace Day09_Result_Test.Test;

public class Tests {
    [SetUp]
    public void Setup() {
    }
    // Failure _Timeout
    [Test]
    public async Task GetPokemon_WhenTimeout_ReturnsNetworkTimeout() {
        var repository = new PokemonRepository3(new MockDataSourceTimeoutExceptionFailure());

        var result = await repository.GetPokemonByNameAsync("ditto");

        var failure = (Result3<Pokemon3, Error3>.Failure)result;
        Assert.That(failure, Is.Not.Null);
        Assert.That(failure!.F, Is.EqualTo(Error3.NetworkTimeout));
    }
    // Failure _Json
    [Test]
    public async Task GetPokemon_WhenJsonError_ReturnsJsonParsingFailed() {
        var repository = new PokemonRepository3(new MockDataSourceJsonErrorFailure());

        var result = await repository.GetPokemonByNameAsync("ditto");

        var failure = (Result3<Pokemon3, Error3>.Failure)result;
        Assert.That(failure, Is.Not.Null);
        Assert.That(failure!.F, Is.EqualTo(Error3.JsonParsingFailed));
    }
    
    // Success _Timeout
    [Test]
    public async Task GetPokemon_WhenNoTimeout_ReturnsSuccess() {
        var repository = new PokemonRepository3(new MockDataSourceTimeoutExceptionSuccess());

        var result = await repository.GetPokemonByNameAsync("ditto");

        var success = result as Result3<Pokemon3, Error3>.Success;
        Assert.That(success, Is.Not.Null);
        Assert.That(success!.S, Is.EqualTo(new Pokemon3(132, "ditto", 3, 40)));
    }
    
    // Success _Json
    [Test]
    public async Task GetPokemon_WhenJsonOk_ReturnsSuccess() {
        var repository = new PokemonRepository3(new MockDataSourceJsonErrorSuccess());

        var result = await repository.GetPokemonByNameAsync("ditto");

        var success = result as Result3<Pokemon3, Error3>.Success;
        Assert.That(success, Is.Not.Null);
        Assert.That(success!.S, Is.EqualTo(new Pokemon3(132, "ditto", 3, 40)));
    }

}