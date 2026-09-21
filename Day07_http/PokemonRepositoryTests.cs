using System.Net;
using Day07_http.Data;
using Day07_http.Data.Mocks;
using Day07_http.Repositories;

namespace Day07_http;

public class PokemonRepositoryTests
{
    private const string SampleJson =
        """
        {
          "name": "glimmora",
          "height": 15,
          "weight": 450,
          "types": [
            { "slot": 1, "type": { "name": "rock", "url": "https://pokeapi.co/api/v2/type/6/" } },
            { "slot": 2, "type": { "name": "poison", "url": "https://pokeapi.co/api/v2/type/4/" } }
          ],
          "sprites": {
            "other": {
              "official-artwork": {
                "front_default": "https://example.com/glimmora.png"
              }
            }
          }
        }
        """;

    [Test]
    public async Task GetPokemonByNameAsync_SuccessResponse_ReturnsMappedPokemon()
    {
        var dataSource = new FakePokemonApiDataSource(HttpStatusCode.OK, SampleJson);
        var repository = new PokemonRepository(dataSource);

        var result = await repository.GetPokemonByNameAsync("glimmora");

        Assert.That(result.IsSuccess, Is.True);

        var pokemon = result.Value!;

        Assert.That(pokemon.Name, Is.EqualTo("glimmora"));
        TestContext.WriteLine($"이름: {pokemon.Name}");

        Assert.That(pokemon.Height, Is.EqualTo(15));
        TestContext.WriteLine($"높이: {pokemon.Height}");

        Assert.That(pokemon.Weight, Is.EqualTo(450));
        TestContext.WriteLine($"무게: {pokemon.Weight}");

        Assert.That(pokemon.Types, Has.Count.EqualTo(2));
        Assert.That(pokemon.Types![0].Type?.Name, Is.EqualTo("rock"));
        TestContext.WriteLine($"타입: {string.Join(", ", pokemon.Types.Select(t => t.Type?.Name))}");

        Assert.That(pokemon.Sprites?.Other?.OfficialArtwork?.OfficialArtworkUrl, Is.EqualTo("https://example.com/glimmora.png"));
    }

    [Test]
    public async Task GetPokemonByNameAsync_NotFoundResponse_ReturnsFailureResult()
    {
        var dataSource = new FakePokemonApiDataSource(HttpStatusCode.NotFound, "");
        var repository = new PokemonRepository(dataSource);

        var result = await repository.GetPokemonByNameAsync("dittooo");

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));
        Assert.That(result.StatusCode, Is.EqualTo(404));
        Assert.That(result.Error, Is.Not.Null.And.Contains("dittooo"));
        TestContext.WriteLine($"에러 메시지: {result.Error}");
    }

    [Test]
    public async Task GetPokemonByNameAsync_TimeoutDataSource_ReturnsFailureResultWithTimeoutError()
    {
        var dataSource = new TimeoutMockDataSource();
        var repository = new PokemonRepository(dataSource);

        
        var result = await repository.GetPokemonByNameAsync("dittooo");

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ErrorType, Is.EqualTo(ErrorType.Timeout));
        Assert.That(result.Error, Is.Not.Null.And.Contains("시간 초과"));
        TestContext.WriteLine($"에러 메시지: {result.Error}");
    }

    
    [Test]
    public async Task GetPokemonByNameAsync_JsonErrorDataSource_ReturnsFailureResultWithParsingError()
    {
        var dataSource = new JsonErrorMockDataSource();
        var repository = new PokemonRepository(dataSource);

        var result = await repository.GetPokemonByNameAsync("dittooo");

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ErrorType, Is.EqualTo(ErrorType.ParsingError));
        Assert.That(result.Error, Is.Not.Null.And.Contains("파싱"));
        TestContext.WriteLine($"에러 메시지: {result.Error}");
    }
}
