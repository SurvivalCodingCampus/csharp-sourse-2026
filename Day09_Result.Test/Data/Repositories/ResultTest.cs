using System.Threading.Tasks;
using Day09_Result.Data.Common;
using Day09_Result.Data.DataSources;
using Day09_Result.Data.DTOs;
using Day09_Result.Data.Repositories;
using Day09_Result.Test.Data.Mocks;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day09_Result.Test.Data.Repositories;

[TestClass]
public class PokemonRepositoryErrorTests
{
    [TestMethod]
    public async Task GetPokemonByNameAsync_WhenTimeoutOccurs_ReturnsTimeoutFailureResult()
    {
        // [Arrange] 항상 타임아웃을 발생시키는 Mock 주입
        var timeoutMock = new TimeoutMockPokemonApiDataSource();
        var repository = new PokemonRepository(timeoutMock);

        // [Act]
        var result = await repository.GetPokemonByNameAsync("pikachu");

        // [Assert]
        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsFailure);
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Value);
        Assert.AreEqual(PokemonErrorType.Timeout, result.ErrorType);
        StringAssert.Contains(result.ErrorMessage, "요청 시간이 초과되었습니다");
    }

    [TestMethod]
    public async Task GetPokemonByNameAsync_WhenSerializationFails_ReturnsSerializationErrorResult()
    {
        // [Arrange] 항상 JSON 파싱 에러를 발생시키는 Mock 주입
        var serializationMock = new SerializationErrorMockPokemonApiDataSource();
        var repository = new PokemonRepository(serializationMock);

        // [Act]
        var result = await repository.GetPokemonByNameAsync("ditto");

        // [Assert]
        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsFailure);
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Value);
        Assert.AreEqual(PokemonErrorType.SerializationError, result.ErrorType);
        StringAssert.Contains(result.ErrorMessage, "JSON 데이터 파싱에 실패했습니다");
    }
}