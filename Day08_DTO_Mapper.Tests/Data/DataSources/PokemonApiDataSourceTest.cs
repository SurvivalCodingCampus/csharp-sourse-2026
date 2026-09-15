using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Day08_DTO_Mapper.Data.DataSources;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day08_DTO_Mapper.Tests.Data.DataSources;

[TestClass]
public class PokemonApiDataSourceTests
{
    [TestMethod]
    public async Task GetPokemonAsync_Success200_ReturnsValidDto()
    {
        // [Arrange] PokeAPI의 정상 JSON 응답 가짜 설정
        string fakeJson = """
        {
            "id": 25,
            "name": "pikachu",
            "height": 4,
            "weight": 60,
            "sprites": { "front_default": "https://example.com/pikachu.png" },
            "types": [
                {
                    "slot": 1,
                    "type": { "name": "electric", "url": "https://pokeapi.co/api/v2/type/13/" }
                }
            ]
        }
        """;

        var fakeHandler = new FakeHttpMessageHandler(HttpStatusCode.OK, fakeJson);
        using var client = new HttpClient(fakeHandler) { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };
        var dataSource = new PokemonApiDataSource(client);

        // [Act]
        var result = await dataSource.GetPokemonAsync("pikachu");

        // [Assert]
        Assert.IsNotNull(result);
        Assert.AreEqual(25, result.Id);
        Assert.AreEqual("pikachu", result.Name);
        Assert.AreEqual(4, result.Height);
        Assert.AreEqual(60, result.Weight);
        Assert.AreEqual("https://example.com/pikachu.png", result.Sprites.FrontDefault);
        Assert.AreEqual(1, result.Types.Count);
        Assert.AreEqual("electric", result.Types[0].Type.Name);
    }

    [TestMethod]
    public async Task GetPokemonAsync_NotFound404_ReturnsNull()
    {
        // [Arrange] 404 Not Found 상태 반환 설정
        var fakeHandler = new FakeHttpMessageHandler(HttpStatusCode.NotFound, "Not Found");
        using var client = new HttpClient(fakeHandler) { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };
        var dataSource = new PokemonApiDataSource(client);

        // [Act]
        var result = await dataSource.GetPokemonAsync("non_existing_pokemon");

        // [Assert] 예외 없이 null 반환
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetPokemonAsync_ServerError500_ReturnsNullWithoutThrowing()
    {
        // [Arrange] 500 Internal Server Error 상태 반환 설정
        var fakeHandler = new FakeHttpMessageHandler(HttpStatusCode.InternalServerError, "Server Error");
        using var client = new HttpClient(fakeHandler) { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };
        var dataSource = new PokemonApiDataSource(client);

        // [Act]
        var result = await dataSource.GetPokemonAsync("pikachu");

        // [Assert] 500 에러 발생 시에도 중단되지 않고 null 반환
        Assert.IsNull(result);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("   ")]
    public async Task GetPokemonAsync_EmptyOrWhitespaceName_ReturnsNullImmediately(string? emptyName)
    {
        // [Arrange]
        var fakeHandler = new FakeHttpMessageHandler(HttpStatusCode.OK, "{}");
        using var client = new HttpClient(fakeHandler) { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };
        var dataSource = new PokemonApiDataSource(client);

        // [Act]
        var result = await dataSource.GetPokemonAsync(emptyName!);

        // [Assert] 네트워크 요청을 보내지도 않고 바로 null 반환
        Assert.IsNull(result);
    }
}

/// <summary>
/// 단위 테스트용 가짜 HTTP 응답 핸들러
/// </summary>
public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpStatusCode _statusCode;
    private readonly string _content;

    public FakeHttpMessageHandler(HttpStatusCode statusCode, string content)
    {
        _statusCode = statusCode;
        _content = content;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent(_content, Encoding.UTF8, "application/json")
        };
        return Task.FromResult(response);
    }
}