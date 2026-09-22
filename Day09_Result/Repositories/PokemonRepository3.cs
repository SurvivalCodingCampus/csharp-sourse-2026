using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.DTO;
using Day09_Result_Pattern.Data.Models;
using Day09_Result.Common;
using Day09_Result.Error;
using Day09_Result.Mapper_필요한_것만_꺼내_;
using Newtonsoft.Json;

namespace Day09_Result.Repositories;

public class PokemonRepository3 : IPokemonRepository3 {
    private readonly IPokemonApiDataSource3 _dataSource;

    public PokemonRepository3(IPokemonApiDataSource3 dataSource) {
        _dataSource = dataSource;
    }

    public async Task<Result3<Pokemon3, Error3>> GetPokemonByNameAsync(string name) {
        try {
            Response3 response3 = await _dataSource.GetPokemonAsync(name);
            var dtoResult = ResponseMapper3.ToDto(response3);

            if (dtoResult is Result3<PokemonDto3, Error3>.Success success) {
                return new Result3<Pokemon3, Error3>.Success(PokemonMapper.ToModel(success.S));
            }
            var failure = (Result3<PokemonDto3, Error3>.Failure)dtoResult;
            return Fail(failure.F);
        } catch (TimeoutException) {
            return Fail(Error3.NetworkTimeout);
        } catch (JsonException) {
            // JsonSerializationException의 부모 클래스라서 JSON 관련 오류를 모두 잡아
            return Fail(Error3.JsonParsingFailed);
        } catch (Exception) {
            return Fail(Error3.Unknown);
        }
    }

    private static Result3<Pokemon3, Error3> Fail(Error3 error) {
        return new Result3<Pokemon3, Error3>.Failure(error);
    }
}