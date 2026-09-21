using Day08_DTO_Mapper.Data.DTO;
using Day08_DTO_Mapper.Data.Mapper;

namespace Day08_DTO_Mapper_Test;

public class Tests
{
    [Test]
    public void ToModel_정상적인_Dto를_Model로_변환()
    {
        var dto = new PokemonDto
        {
            Name = "jigglypuff",
            Height = 5,
            Weight = 55,

            Types = new List<PokemonTypeDto>
            {
                new PokemonTypeDto
                {
                    Type = new NamedApiResourceDto
                    {
                        Name = "normal"
                    }
                }
            },

            Stats = new List<PokemonStatDto>
            {
                CreateStat("attack", 45),
                CreateStat("defense", 20),
                CreateStat("speed", 20),
                CreateStat("special-attack", 45),
                CreateStat("special-defense", 25)
            }
        };
        
        var pokemon = dto.ToModel();
        
        Assert.Multiple(() =>
        {
            Assert.That(
                pokemon.Name,
                Is.EqualTo("jigglypuff")
            );

            Assert.That(
                pokemon.Types,
                Does.Contain("normal")
            );

            Assert.That(
                pokemon.Height,
                Is.EqualTo(0.5)
            );

            Assert.That(
                pokemon.Weight,
                Is.EqualTo(5.5)
            );

            Assert.That(
                pokemon.Attack,
                Is.EqualTo(45)
            );

            Assert.That(
                pokemon.Defense,
                Is.EqualTo(20)
            );

            Assert.That(
                pokemon.Speed,
                Is.EqualTo(20)
            );

            Assert.That(
                pokemon.SpecialAttack,
                Is.EqualTo(45)
            );

            Assert.That(
                pokemon.SpecialDefense,
                Is.EqualTo(25)
            );
        });
    }

    [Test]
    public void ToModel_Name이_Null이면_Unknown으로_변환()
    {
        var dto = new PokemonDto
        {
            Name = null
        };
        
        var pokemon = dto.ToModel();
        
        Assert.That(
            pokemon.Name,
            Is.EqualTo("Unknown")
        );
    }

    [Test]
    public void ToModel_Name이_공백이면_Unknown으로_변환()
    {
        var dto = new PokemonDto
        {
            Name = "   "
        };
        
        var pokemon = dto.ToModel();
        
        Assert.That(
            pokemon.Name,
            Is.EqualTo("Unknown")
        );
    }

    [Test]
    public void ToModel_Null값을_기본값으로_변환()
    {
        var dto = new PokemonDto
        {
            Name = null,
            Height = null,
            Weight = null,
            Types = null,
            Stats = null
        };
        
        var pokemon = dto.ToModel();
        
        Assert.Multiple(() =>
        {
            Assert.That(
                pokemon.Name,
                Is.EqualTo("Unknown")
            );

            Assert.That(
                pokemon.Types,
                Is.Empty
            );

            Assert.That(
                pokemon.Height,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.Weight,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.Attack,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.Defense,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.Speed,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.SpecialAttack,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.SpecialDefense,
                Is.EqualTo(0)
            );
        });
    }

    [Test]
    public void ToModel_Stat이_없으면_0으로_변환()
    {
        var dto = new PokemonDto
        {
            Name = "jigglypuff",

            Stats = new List<PokemonStatDto>
            {
                CreateStat("attack", 45)
            }
        };
        
        var pokemon = dto.ToModel();
        
        Assert.Multiple(() =>
        {
            Assert.That(
                pokemon.Attack,
                Is.EqualTo(45)
            );

            Assert.That(
                pokemon.Defense,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.Speed,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.SpecialAttack,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.SpecialDefense,
                Is.EqualTo(0)
            );
        });
    }

    [Test]
    public void ToModel_음수값을_0으로_변환()
    {
        // Arrange
        var dto = new PokemonDto
        {
            Name = "jigglypuff",
            Height = -10,
            Weight = -20,

            Stats = new List<PokemonStatDto>
            {
                CreateStat("attack", -100)
            }
        };

        // Act
        var pokemon = dto.ToModel();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(
                pokemon.Height,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.Weight,
                Is.EqualTo(0)
            );

            Assert.That(
                pokemon.Attack,
                Is.EqualTo(0)
            );
        });
    }

    [Test]
    public void ToModel_Dto가_Null이어도_기본_Model을_반환()
    {
        // Arrange
        PokemonDto? dto = null;

        // Act
        var pokemon = dto.ToModel();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(
                pokemon,
                Is.Not.Null
            );

            Assert.That(
                pokemon.Name,
                Is.EqualTo("Unknown")
            );

            Assert.That(
                pokemon.Types,
                Is.Empty
            );
        });
    }

    private static PokemonStatDto CreateStat(
        string name,
        int value)
    {
        return new PokemonStatDto
        {
            BaseStat = value,

            Stat = new NamedApiResourceDto
            {
                Name = name
            }
        };
    }
}