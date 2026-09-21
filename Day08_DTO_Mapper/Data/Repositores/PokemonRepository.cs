using Day08_DTO_Mapper.Data.DataSources;
using Day08_DTO_Mapper.Data.Mapper;
using Day08_DTO_Mapper.Data.Models;

namespace Day08_DTO_Mapper.Data.Repositores;

public class PokemonRepository : IPokemonRepository
{
    private readonly IPokemonApiDataSource _dataSource;

    public PokemonRepository(
        IPokemonApiDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<Pokemon> GetPokemonByNameAsync(
        string pokemonName)
    {
        var dto =
            await _dataSource.GetPokemonAsync(
                pokemonName
            );

        return dto.ToModel();
    }
}