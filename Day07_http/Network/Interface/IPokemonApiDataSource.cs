using System.Text;
using Day07_http.Network.DataSources;
using Newtonsoft.Json;
namespace Day07_http.Network.Interface;


// 재 사용할 포켓의 캐릭터가 있는 url
public interface IPokemonApiDataSource {
    Task<Response> GetPokemonAsync(string pokemonName);
}