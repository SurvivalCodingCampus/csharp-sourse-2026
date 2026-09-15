using System;
using System.Threading.Tasks;
using Day07_http.Data.Interfaces;
using Day07_http.Data.Models;
using Day07_http.Data.Repositories;
using Day07_http.Tests.Data.DataSources;
using NUnit.Framework;

namespace Day07_http.Tests.Data.Interfaces;

[TestFixture]
[TestOf(typeof(PokemonRepository))]
public class PokemonRepositoryTest
{
    [Test]
    public async Task 통신중_터졌을때()
    {
        IPokemonApiDataSource dataSource = new MockPokemonApiDataSource();
        IPokemonRepository repository = new PokemonRepository(dataSource);
        
        Assert.ThrowsAsync<PokemonException>(async () => await repository.GetPokemonByNameAsync("홍길동"));
    }
    
    [Test]
    public async Task 포켓몬_정보_없는경우()
    {
        IPokemonApiDataSource dataSource = new MockPokemonApiDataSource();
        IPokemonRepository repository = new PokemonRepository(dataSource);
        
        Pokemon pokemon = await repository.GetPokemonByNameAsync("unknown");
        Assert.IsNull(pokemon);
    }

    [Test]
    public async Task 포켓몬_정보_잘_가져오는지()
    {
        IPokemonApiDataSource dataSource = new MockPokemonApiDataSource();
        IPokemonRepository repository = new PokemonRepository(dataSource);
        
        Pokemon pokemon = await repository.GetPokemonByNameAsync("ditto");
        Assert.AreEqual("ditto", pokemon?.Name);
    }
}