using System.Security.Authentication.ExtendedProtection;
using Day07_http.Data.DataSources;
using Day07_http.Data.Repositories;
using Day07_http.Models;

namespace Day07_http_Test;

public class Tests
{
    PokemonRepository pokemon = new PokemonRepository(new PokemonApiDataSource(new HttpClient()));
    
    [Test] // ID 및 이름 검사
    public async Task Test1()
    {
        Pokemon? pokemon2 = await pokemon.GetPokemonByNameAsync("pikachu");
        
        Assert.That(pokemon2.Id, Is.EqualTo("25"));
        Assert.That(pokemon2.Name, Is.EqualTo("pikachu"));
        
        Pokemon? pokemon3 = await pokemon.GetPokemonByNameAsync("Charmander");
        
        Assert.That(pokemon3.Id, Is.EqualTo("4"));
        Assert.That(pokemon3.Name, Is.EqualTo("charmander"));
    }
}