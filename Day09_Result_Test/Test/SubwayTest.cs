using Day09_Result_Pattern.Data.DataSources;
using Day09_Result.Common;
using Day09_Result.DataSources;
using Day09_Result.Error;
using Day09_Result.Models;
using Day09_Result.Repositories;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var repository = new SubwayRepository3(new SubwayApiDataSource3());

foreach (var stationName in new[] { "서울", "없는역" }) {
    Console.WriteLine($"[{stationName}]");
    var result = await repository.GetArrivalsAsync(stationName);

    switch (result) {
        case Result3<List<Subway3>, Error3>.Success success:
            foreach (var arrival in success.S) {
                Console.WriteLine($"  {arrival.UpDown} {arrival.Direction} : {arrival.Message}");
            }
            break;
        case Result3<List<Subway3>, Error3>.Failure failure:
            Console.WriteLine($"  {ToMessage(failure.F)}");
            break;
    }
}

static string ToMessage(Error3 error) {
    switch (error) {
        case Error3.NotFound:
            return "역 이름을 다시 확인해 주세요.";
        case Error3.NetworkTimeout:
            return "응답이 너무 늦어요. 잠시 후 다시 시도해 주세요.";
        case Error3.JsonParsingFailed:
            return "받은 데이터를 읽을 수 없어요.";
        default:
            return "알 수 없는 오류가 났어요.";
    }
}