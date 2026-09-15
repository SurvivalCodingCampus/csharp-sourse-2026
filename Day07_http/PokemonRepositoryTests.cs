using System.Net;
using Day07_http.Data;
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

        var pokemon = await repository.GetPokemonByNameAsync("glimmora");

        Assert.That(pokemon, Is.Not.Null);

        Assert.That(pokemon!.Name, Is.EqualTo("glimmora"));
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
    public async Task GetPokemonByNameAsync_NotFoundResponse_ReturnsNull()
    {
        var dataSource = new FakePokemonApiDataSource(HttpStatusCode.NotFound, "");
        var repository = new PokemonRepository(dataSource);

        var pokemon = await repository.GetPokemonByNameAsync("no-such-pokemon");

        Assert.That(pokemon, Is.Null);
    }
}
