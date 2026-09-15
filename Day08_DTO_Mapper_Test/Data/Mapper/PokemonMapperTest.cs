using System.Collections.Generic;
using Day08_DTO_Mapper.Data.DTOs;
using Day08_DTO_Mapper.Data.Mapper;
using NUnit.Framework;

namespace Day08_DTO_Mapper_Test.Data.Mapper;

[TestFixture]
[TestOf(typeof(PokemonMapper))]
public class PokemonMapperTest
{
    
    
    [Test]
    public void 빈DTO_생성시_오류발생_테스트()
    {
        var noneDataDto = new PokemonDto();
        
        Assert.DoesNotThrow(() => noneDataDto.ToModel());
    }

    [Test]
    public void 정상적인_값을_가진_DTO_생성_테스트()
    {
        var dittoDataDto = new PokemonDto
        {
            Id = 132,
            Name = "ditto",
            Sprites = new SpritesDto
            {
                FrontDefault = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/132.png"
            },
            Types = new List<TypeDto>(
                [new TypeDto
                {
                    Slot = 1, Type = new NamedApiResourceDto
                    {
                        Name = "normal", Url = "https://pokeapi.co/api/v2/type/1/"
                    }
                }])
        };

        var toModel = dittoDataDto.ToModel();
        
        Assert.That(toModel.Name, Is.EqualTo(dittoDataDto.Name));
        Assert.That(toModel.Id, Is.EqualTo(dittoDataDto.Id));
        Assert.That(toModel.ImageUrl, Is.EqualTo(dittoDataDto.Sprites.FrontDefault));

    }
}