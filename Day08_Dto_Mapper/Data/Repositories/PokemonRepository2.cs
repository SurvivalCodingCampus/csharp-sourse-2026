using System.Text.Json;
using Day08_Dto_Mapper.Data.DataSources;
using Day08_Dto_Mapper.Data.Models;
using Day08_Dto_Mapper.Data.DTOs;


namespace Day08_Dto_Mapper.Data.Interfaces;

public class PokemonRepository2(IPokemonApiDataSource2 dataSource2): IPokemonRepository2{
  public async Task<Pokemon2?> GetPokemonByNameAsync(string pokemonName){
      try{
          Response2 response2 = await dataSource2.GetPokemonAsync(pokemonName);

          if (response2.StatusCode != 200){
              return null;
          }

          PokemonDto? pokemonDto = JsonSerializer.Deserialize<PokemonDto>(response2.Body);
          return pokemonDto?.ToModel(); //http에 널문 추가
      } catch (Exception){
          throw new PokemonException();
      }
  }
}

public class PokemonException : Exception;