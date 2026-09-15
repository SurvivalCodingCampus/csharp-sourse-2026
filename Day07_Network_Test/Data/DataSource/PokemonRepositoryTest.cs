using System.Net.Http;
using System.Threading.Tasks;
using Day07_Network.Data.DataSources;
using Day07_Network.Data.Interfaces;
using Day07_Network.Models;
using NUnit.Framework;

namespace Day07_Network_Test.Data.DataSource;

[TestFixture]
[TestOf(typeof(PokemonRepository))]
public class PokemonRepositoryTest
{
    private readonly string _zapdos = "zapdos";

    private IPokemonApiDataSource _pokemonApiDataSource = null;
    private IPokemonRepository _repository = null;

    [SetUp]
    public void Setup()
    {
        // Given
        _pokemonApiDataSource = new FakePokemonApiDataSource();
        _repository = new PokemonRepository(_pokemonApiDataSource);

    }

    [Test]
    public async Task 이름_테스트()
    {

        // When
        Pokemon pokemon = await _repository.GetPokemonByNameAsync(_zapdos);

        // Then
        Assert.That(pokemon, Is.Not.Null);
        Assert.That(pokemon.Name, Is.EqualTo(_zapdos));
    }

    [Test]
    public async Task 멀티타입테스트()
    {
        // When
        Pokemon pokemon = await _repository.GetPokemonByNameAsync(_zapdos);

        // Then
        Assert.That(pokemon.Types.Length, Is.EqualTo(2));
        Assert.That(pokemon.Types[0].type.TypeName, Is.EqualTo("electric"));
        Assert.That(pokemon.Types[1].type.TypeName, Is.EqualTo("flying"));
    }

    [Test]
    public async Task 스프라이트테스트()
    {
        Pokemon pokemon = await _repository.GetPokemonByNameAsync(_zapdos);

        Assert.That(pokemon, Is.Not.Null);
        Assert.That(pokemon.Sprites.Other.OfficialArtwork.FrontDefault,
            Is.EqualTo(
                "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/145.png")
        );
    }
}