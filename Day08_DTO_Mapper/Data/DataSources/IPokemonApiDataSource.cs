using Day08_DTO_Mapper.Data.DTO;

namespace Day08_DTO_Mapper.Data.DataSources;

public interface IPokemonApiDataSource
{
    Task<PokemonDto?> GetPokemonAsync(
        string pokemonName
    );
}