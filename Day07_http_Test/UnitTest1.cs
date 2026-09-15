using Day07_http.Network.DataSources;
using Day07_http.Network.Models;

namespace Day07_http;

public class Tests {

/*
    [Test]
    public async Task Test1() {

        PokemonRepository pokemonRepository = new PokemonRepository(new PokemonApiDataSource(new HttpClient()));
        Pokemon pokemonName = await pokemonRepository.GetPokemonByNameAsync("pikachu");
        Assert.That(pokemonName.Name, Is.EqualTo("pikachu"));
    }
}
*/

//MockPokemonApiDataSourc를 새로 만들어서 테스트
    public async Task Test1() {
        PokemonRepository pokemonRepository = new PokemonRepository(new MockPokemonApiDataSource());
        Pokemon pokemonName = await pokemonRepository.GetPokemonByNameAsync("pikachu");
        Assert.That(pokemonName.Name, Is.EqualTo("pikachu"));
    }
}