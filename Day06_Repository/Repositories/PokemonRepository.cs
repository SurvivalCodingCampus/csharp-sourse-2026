using Day06_Repository.DataSources;
using Day06_Repository.Models;
using Newtonsoft.Json;

namespace Day06_Repository.Repositories;

public class PokemonRepository(IPokeApiDataSource dataSource)
{
    public async Task<Pokemon?> GetPokemonAsync(string name)
    {
        var response = await dataSource.GetPokemonAsync(name);
        
        if (response.StatusCode == 200 && !string.IsNullOrEmpty(response.Body))
        {
           
            var dto = JsonConvert.DeserializeObject<PokemonDto>(response.Body);

            if (dto != null)
            {
                return new Pokemon(
                    dto.Id,
                    dto.Name,
                    dto.Sprites?.FrontDefault ?? string.Empty
                );
            }
        }
        
        return null;
    }
}