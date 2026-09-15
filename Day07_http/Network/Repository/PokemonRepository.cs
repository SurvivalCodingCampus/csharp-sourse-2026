using Day07_http.Network.Interface;
using Day07_http.Network.Models;
using Newtonsoft.Json;

namespace Day07_http;

public class PokemonRepository : IPokemonRepository {
    //
    private readonly IPokemonApiDataSource _pokemonRepository;

    public PokemonRepository(IPokemonApiDataSource pokemonRepository) {
        _pokemonRepository = pokemonRepository;
    }
    
    // 포켓몬이 없을 수도 있는 함수를 실행
    // 포켓몬의 Api중에서더  포켓몬 이름을 가져와서 변수에 넣어줄거야
    // 꺼내서 Header Body의 Name에 해당되는 것을 읽을거야
    public async Task<Pokemon?> GetPokemonByNameAsync(string pokemonName) {
        var pokemonNameFinal =  await _pokemonRepository.GetPokemonAsync(pokemonName);
        return JsonConvert.DeserializeObject<Pokemon>(pokemonNameFinal.Body);
    }
}





/*
p.54


*/