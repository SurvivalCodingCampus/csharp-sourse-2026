using Day07_Http.Data.DataSources;
using Day07_Http.Data.Interfaces;
using Day07_Http.Repositories;
using Moq;


namespace Day07_Http_Test;

public class Tests
{
    private Mock<IPokemonApiDataSource> _mockDataSource = null!;
    private IPokemonRepository _repository = null!;

    private const string JigglypuffJson = """
                                          {
                                            "name": "jigglypuff",
                                            "height": 5,
                                            "weight": 55,
                                            "types": [
                                              {
                                                "type": {
                                                  "name": "normal"
                                                }
                                              },
                                              {
                                                "type": {
                                                  "name": "fairy"
                                                }
                                              }
                                            ],
                                            "stats": [
                                              {
                                                "base_stat": 45,
                                                "stat": {
                                                  "name": "attack"
                                                }
                                              },
                                              {
                                                "base_stat": 20,
                                                "stat": {
                                                  "name": "defense"
                                                }
                                              },
                                              {
                                                "base_stat": 20,
                                                "stat": {
                                                  "name": "speed"
                                                }
                                              },
                                              {
                                                "base_stat": 45,
                                                "stat": {
                                                  "name": "special-attack"
                                                }
                                              },
                                              {
                                                "base_stat": 25,
                                                "stat": {
                                                  "name": "special-defense"
                                                }
                                              }
                                            ]
                                          }
                                          """;

    [SetUp]
    public void Setup()
    {
        _mockDataSource = new Mock<IPokemonApiDataSource>();

        var response = new Response(
            200,
            new Dictionary<string, string>(),
            JigglypuffJson
        );

        _mockDataSource
            .Setup(dataSource =>
                dataSource.GetPokemonAsync("jigglypuff"))
            .ReturnsAsync(response);

        _repository =
            new PokemonRepository(_mockDataSource.Object);
    }

    [Test]
    public async Task 이름_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        Assert.That(pokemon!.Name, Is.EqualTo("jigglypuff"));
    }

    [Test]
    public async Task 키_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        Assert.That(pokemon!.Height, Is.EqualTo(5));
    }

    [Test]
    public async Task 몸무게_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        Assert.That(pokemon!.Weight, Is.EqualTo(55));
    }

    [Test]
    public async Task 속성_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        Assert.That(
            pokemon!.Types?[0].Type?.Name,
            Is.EqualTo("normal")
        );

        Assert.That(
            pokemon.Types?[1].Type?.Name,
            Is.EqualTo("fairy")
        );
    }

    [Test]
    public async Task 공격력_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        var attack =
            pokemon!.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "attack")?
                .BaseStat;

        Assert.That(attack, Is.EqualTo(45));
    }

    [Test]
    public async Task 방어력_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        var defense =
            pokemon!.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "defense")?
                .BaseStat;

        Assert.That(defense, Is.EqualTo(20));
    }

    [Test]
    public async Task 스피드_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        var speed =
            pokemon!.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "speed")?
                .BaseStat;

        Assert.That(speed, Is.EqualTo(20));
    }

    [Test]
    public async Task 특수공격_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        var specialAttack =
            pokemon!.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "special-attack")?
                .BaseStat;

        Assert.That(specialAttack, Is.EqualTo(45));
    }

    [Test]
    public async Task 특수방어_확인()
    {
        var pokemon =
            await _repository.GetPokemonByNameAsync("jigglypuff");

        var specialDefense =
            pokemon!.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "special-defense")?
                .BaseStat;

        Assert.That(specialDefense, Is.EqualTo(25));
    }
}