using Day07_http.Network.Models;
using Day09_Result_Pattern.Data.Models;
using Day09_Result.Common;
using Day09_Result.Error;

namespace Day09_Result.Repositories;

public interface IPokemonRepository3 {
    Task<Result3<Pokemon3, Error3>> GetPokemonByNameAsync(string name);
}