using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.DTO;
using Day09_Result.Common;
using Day09_Result.Error;
using Newtonsoft.Json;

namespace Day09_Result.Mapper_필요한_것만_꺼내_;

public static class ResponseMapper3 {
    public static Result3<PokemonDto3, Error3> ToDto(Response3 response3) {
        switch (response3.StatusCode) {
            case 200: {
                var dto = JsonConvert.DeserializeObject<PokemonDto3>(response3.Body);
                if (dto == null) {
                    return new Result3<PokemonDto3, Error3>.Failure(F: Error3.JsonParsingFailed);
                }
                return new Result3<PokemonDto3, Error3>.Success(dto);
            }
            case 404:
                return new Result3<PokemonDto3, Error3>.Failure(F: Error3.NotFound);
            case 401:
            case 403:
                return new Result3<PokemonDto3, Error3>.Failure(F: Error3.AuthenticationFailed);
            default:
                return new Result3<PokemonDto3, Error3>.Failure(F: Error3.Unknown);
        }
    }
}