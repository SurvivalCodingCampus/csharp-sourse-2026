using System.Threading.Tasks;
using Day08_DTO_Mapper_Test.Data.DataSources;
using Day08_DTO_Mapper.Data.DTOs;
using Day08_DTO_Mapper.Data.Interfaces;
using Day08_DTO_Mapper.Data.Models;
using Day08_DTO_Mapper.Data.Repository;
using NUnit.Framework;

namespace Day08_DTO_Mapper_Test.Data.Repository;

[TestFixture]
[TestOf(typeof(PokemonRepository))]
public class PokemonRepositoryTest
{
    private IPokemonApiDataSource _source = null;
    private IPokemonRepository _repository = null;
    
    private readonly string _pikachu = "pikachu";
    private readonly string _unknown = "unknown";
    private readonly string _another = "Another";

    [SetUp]
    public void SetUp()
    {
        // Given
        _source = new FakePokemonApiDataSource();
        _repository = new PokemonRepository(_source);
    }

    [Test]
    public async Task 정상실행확인()
    {
        // When
        Pokemon pokemon = await _repository.GetPokemonByNameAsync(_pikachu);
        
        // Then
        Assert.That(pokemon, Is.Not.Null);
        Assert.That(pokemon.Name, Is.EqualTo(_pikachu));
    }
    
    [Test]
    public async Task 대소문자인식확인()
    {
        // When
        Pokemon pokemon = await _repository.GetPokemonByNameAsync(_pikachu.ToUpper());
        
        // Then
        Assert.That(pokemon, Is.Not.Null);
        Assert.That(pokemon.Name, Is.EqualTo(_pikachu));
    }
    
    [Test]
    public async Task Unknown_404처리확인()
    {
        // When
        Pokemon pokemon = await _repository.GetPokemonByNameAsync(_unknown);
        
        // Then
        Assert.That(pokemon, Is.Not.Null);
        Assert.That(pokemon.Name, Is.EqualTo("MissingNo."));
        Assert.That(pokemon.Id, Is.EqualTo(0));
    }

    [Test]
    public async Task AnotherStatusCode()
    {
        // When
        Pokemon pokemon = await _repository.GetPokemonByNameAsync(_another);
        
        // Then
        Assert.That(pokemon, Is.Not.Null);
        Assert.That(pokemon.Name, Is.EqualTo(_another));
        Assert.That(pokemon.Id, Is.EqualTo(-1));
    }
}