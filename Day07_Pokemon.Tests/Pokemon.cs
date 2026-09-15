using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Day07_Pokemon;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day07_Pokemon.Tests;

[TestClass]
public class PokemonTests
{
    // ----------------------------------------------------
    // 1. 단위 테스트: 실제 API 호출 없이 Repository 로직 검증
    // ----------------------------------------------------
    [TestMethod]
    public async Task GetPokemonByNameAsync_ValidData_ReturnsMappedPokemon()
    {
        // [Arrange: 준비] 가짜 API 응답 데이터를 돌려주는 DataSource 준비
        var fakeDataSource = new FakePokemonApiDataSource();
        var repository = new PokemonRepository(fakeDataSource);

        // [Act: 실행] 레포지토리 메서드 호출
        var result = await repository.GetPokemonByNameAsync("pikachu");

        // [Assert: 검증] 데이터가 도메인 모델로 올바르게 변환되었는지 확인
        Assert.IsNotNull(result);
        Assert.AreEqual(25, result.Id);
        Assert.AreEqual("pikachu", result.Name);
        Assert.AreEqual(0.4, result.HeightMeter); // 4 decimeter -> 0.4m 계산 검증
        Assert.AreEqual(6.0, result.WeightKg);    // 60 hectogram -> 6.0kg 계산 검증
        Assert.AreEqual("electric", result.Types[0]);
    }

    [TestMethod]
    public async Task GetPokemonByNameAsync_NotFound_ReturnsNull()
    {
        // [Arrange: 준비]
        var fakeDataSource = new FakePokemonApiDataSource();
        var repository = new PokemonRepository(fakeDataSource);

        // [Act: 실행] 존재하지 않는 이름 전달
        var result = await repository.GetPokemonByNameAsync("unknown_pokemon");

        // [Assert: 검증] null이 반환되는지 확인
        Assert.IsNull(result);
    }

    // ----------------------------------------------------
    // 2. 통합 테스트: 실제 PokeAPI 서버와 통신 검증
    // ----------------------------------------------------
    [TestMethod]
    public async Task Integration_GetPokemonByNameAsync_RealApi_ReturnsDitto()
    {
        // [Arrange: 준비] 실제 HttpClient와 DataSource 인스턴스 생성
        using var httpClient = new HttpClient { BaseAddress = new System.Uri("https://pokeapi.co/api/v2/") };
        var realDataSource = new PokemonApiDataSource(httpClient);
        var repository = new PokemonRepository(realDataSource);

        // [Act: 실행] 실제 메타몽(ditto) 데이터 요청
        var result = await repository.GetPokemonByNameAsync("ditto");

        // [Assert: 검증]
        Assert.IsNotNull(result);
        Assert.AreEqual(132, result.Id);
        Assert.AreEqual("ditto", result.Name);
        Assert.IsTrue(result.Types.Contains("normal"));
    }
}

// 단위 테스트용 가짜(Fake) DataSource 클래스
public class FakePokemonApiDataSource : IPokemonApiDataSource
{
    public Task<PokemonApiResponse?> GetPokemonAsync(string pokemonName)
    {
        if (pokemonName == "pikachu")
        {
            var fakeResponse = new PokemonApiResponse
            {
                Id = 25,
                Name = "pikachu",
                Height = 4,   // 0.4m
                Weight = 60,  // 6.0kg
                Sprites = new PokemonSprites { FrontDefault = "https://example.com/pikachu.png" },
                Types = new()
                {
                    new PokemonTypeSlot { Slot = 1, Type = new NamedApiResource { Name = "electric" } }
                }
            };
            return Task.FromResult<PokemonApiResponse?>(fakeResponse);
        }

        // pikachu 외에는 검색 실패(null) 시뮬레이션
        return Task.FromResult<PokemonApiResponse?>(null);
    }
}