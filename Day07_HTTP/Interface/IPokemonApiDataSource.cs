namespace Day07_HTTP.obj;

public interface IPokemonApiDataSource<T>
{

    public Task<Response<T>> GetByNameAsync(string pokemonName);
    
    public Task<Response<T>> GetByIdAsync(int id);
    
    
}