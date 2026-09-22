using Day07_http.Data.DataSources;
using Day07_http.Models;
using Day07_http.Repositories;

namespace Day07_http;

public class Tests
{
    private Pokemon? _pokemon;

    [SetUp]
    public async Task Setup()
    {
        var repository = new PokemonRepository(new PokemonDataSource());
        var result = await repository.GetPokemonByNameAsync("glimmora");
        _pokemon = result.IsSuccess ? result.Value : null;
    }

    [Test]
    public void 이름_확인()
    {
        Assert.That(_pokemon?.Name, Is.EqualTo("glimmora"));
        TestContext.WriteLine($"이름: {_pokemon?.Name}");
    }

    [Test]
    public void 높이_확인()
    {
        Assert.That(_pokemon?.Height, Is.EqualTo(15));
        TestContext.WriteLine($"높이: {_pokemon?.Height}");
    }

    [Test]
    public void 무게_확인()
    {
        Assert.That(_pokemon?.Weight, Is.EqualTo(450));
        TestContext.WriteLine($"무게: {_pokemon?.Weight}");
    }

    [Test]
    public void 타입_확인()
    {
        var types = _pokemon?.Types?.Select(t => t.Type?.Name).ToList();
        Assert.That(types, Is.EqualTo(new[] { "rock", "poison" }));
        TestContext.WriteLine($"타입: {string.Join(", ", types ?? [])}");
    }
    }
