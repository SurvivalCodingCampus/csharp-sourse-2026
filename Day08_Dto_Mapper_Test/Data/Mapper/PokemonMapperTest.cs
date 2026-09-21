using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using Day08_Dto_Mapper.Data.DataSources;
using Day08_Dto_Mapper.Data.Mapper;
using Day08_Dto_Mapper.Data.Repositories;
using NUnit.Framework;

namespace Day08_Dto_Mapper_Test.Data.Mapper;

[TestFixture]
[TestOf(typeof(PokemonMapper))]
public class PokemonMapperTest
{
    IPokemonRepository repository = new PokemonRepository(new PokemonApiDataSource(new HttpClient()));
    
    [Test]
    public async Task 시나리오1정상데이터매핑테스트()
    {
        var pokemon = await repository.GetPokemonByNameAsync("pikachu");
        
        Assert.That(pokemon?.Name, Is.EqualTo("pikachu"));
        Assert.That(pokemon.ImageUrl,Is.EqualTo("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/25.png"));
    }

    [Test]
    public async Task 시나리오2이름이비어있거나공백인경우테스트()
    {
        var pokemon = await repository.GetPokemonByNameAsync("");
        
        Assert.That(pokemon?.Name, Is.EqualTo("Unknown"));
        Assert.That(pokemon.ImageUrl,Is.EqualTo("default_img.png"));
    }

    [Test]
    public async Task 시나리오3Dto자체가Null인경우()
    {
        PokemonDto? dto = null;

        var model = dto.ToModel();
        
        Assert.That(model, Is.Not.Null);
        Assert.That(model.Name, Is.EqualTo("Unknown"));
        Assert.That(model.ImageUrl, Is.EqualTo("default_img.png"));
    }

    [Test]
    public async Task 시나리오4스프라이트가누락된경우()
    {
        var dto = new PokemonDto
        {
            Name = "ditto",
            Sprites = null
        };
        
        var model = dto.ToModel();
        
        Assert.That(model.Name, Is.EqualTo("ditto"));
        Assert.That(model.ImageUrl, Is.EqualTo("default_img.png"));
    }
}