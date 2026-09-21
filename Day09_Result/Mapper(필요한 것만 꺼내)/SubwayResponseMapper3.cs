using Day09_Result_Pattern.Data.DataSources;
using Day09_Result.Common;
using Day09_Result.Error;
using Newtonsoft.Json;

namespace Day09_Result.Mapper_필요한_것만_꺼내_;

public static class SubwayResponseMapper3 {
    public static Result3<SubwayDto3, Error3> ToDto(Response3 response3) {
        switch (response3.StatusCode) {
            case 200: {
                var dto = JsonConvert.DeserializeObject<SubwayDto3>(response3.Body);
                if (dto == null || dto.realtimeArrivalList.Count == 0) {
                    return new Result3<SubwayDto3, Error3>.Failure(Error3.JsonParsingFailed);
                }

                return new Result3<SubwayDto3, Error3>.Success(dto);
            }
            default:
                return new Result3<SubwayDto3, Error3>.Failure(Error3.Unknown);
        }
    }
}