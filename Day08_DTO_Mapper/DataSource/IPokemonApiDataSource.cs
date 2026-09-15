using Day08_DTO_Mapper;

namespace Day08_DTO_Mapper;

public interface IPokemonApiDataSource
{

    public Task<Response<PokemonDTO>> GetByNameAsync(string pokemonName);
    
    public Task<Response<PokemonDTO>> GetByIdAsync(int id);
    
    
}