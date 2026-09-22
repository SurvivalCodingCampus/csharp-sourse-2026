namespace Day09_Result_Pattern.Data.DataSources;

public interface IPokemonApiDataSource3 {
    Task<Response3> GetPokemonAsync(string name);
}