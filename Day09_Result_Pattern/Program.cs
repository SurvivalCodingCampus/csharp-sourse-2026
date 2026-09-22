using Day09_Result_Pattern.Data.Common;
using Day09_Result_Pattern.Data.Common.Errors;
using Day09_Result_Pattern.Data.DataSources;
using Day09_Result_Pattern.Data.Models;
using Day09_Result_Pattern.Data.Repositories;

namespace Day09_Result_Pattern;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("1. 포켓몬 정보");
        Console.WriteLine("2. 지하철 도착 정보");
        Console.Write("선택: ");

        var menu = Console.ReadLine();

        switch (menu)
        {
            case "1":
                await PokemonProgram();
                break;

            case "2":
                await SubwayProgram();
                break;

            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }
    }

    private static async Task PokemonProgram()
    {
        IPokemonApiDataSource dataSource =
            new PokemonApiDataSource(
                new HttpClient()
            );

        IPokemonRepository repository =
            new PokemonRepository(
                dataSource
            );

        Console.Write("포켓몬 이름: ");

        var pokemonName =
            Console.ReadLine() ?? "";

        var result =
            await repository.GetPokemonByNameAsync(
                pokemonName
            );

        switch (result)
        {
            case Result<Pokemon, PokemonError>.Success successResult:
                Console.WriteLine(
                    $"포켓몬 이름: {successResult.data.Name}"
                );

                Console.WriteLine(
                    $"이미지URL: {successResult.data.ImageUrl}"
                );
                break;

            case Result<Pokemon, PokemonError>.Error errorResult:
                switch (errorResult.error)
                {
                    case PokemonError.NotFound:
                        Console.WriteLine(
                            "오류: 해당 포켓몬을 찾을 수 없습니다."
                        );
                        break;

                    case PokemonError.NetworkTimeout:
                        Console.WriteLine(
                            "오류: 네트워크 연결 시간 초과."
                        );
                        break;

                    default:
                        Console.WriteLine(
                            "알 수 없는 오류가 발생했습니다."
                        );
                        break;
                }

                break;
        }
    }

    private static async Task SubwayProgram()
    {
        ISubwayApiDataSource dataSource =
            new SubwayApiDataSource(
                new HttpClient()
            );

        ISubwayRepository repository =
            new SubwayRepository(
                dataSource
            );

        Console.Write("역 이름: ");

        var stationName =
            Console.ReadLine() ?? "";

        var result =
            await repository.GetArrivalsAsync(
                stationName
            );

        switch (result)
        {
            case Result<List<Subway>, SubwayError>.Success successResult:
                foreach (var subway in successResult.data)
                {
                    Console.WriteLine();

                    Console.WriteLine(
                        $"역 이름: {subway.StationName}"
                    );

                    Console.WriteLine(
                        $"방향: {subway.Direction}"
                    );

                    Console.WriteLine(
                        $"열차 정보: {subway.TrainLineName}"
                    );

                    Console.WriteLine(
                        $"종착역: {subway.TerminalStation}"
                    );

                    Console.WriteLine(
                        $"도착 정보: {subway.ArrivalMessage}"
                    );

                    Console.WriteLine(
                        $"현재 위치: {subway.CurrentLocation}"
                    );

                    Console.WriteLine(
                        $"도착까지 남은 시간: {subway.ArrivalSeconds}초"
                    );

                    Console.WriteLine(
                        $"도착 상태 코드: {subway.ArrivalCode}"
                    );

                    Console.WriteLine(
                        "------------------------------"
                    );
                }

                break;

            case Result<List<Subway>, SubwayError>.Error errorResult:
                switch (errorResult.error)
                {
                    case SubwayError.StationNotFound:
                        Console.WriteLine(
                            "오류: 해당 역을 찾을 수 없습니다."
                        );
                        break;

                    case SubwayError.NetworkTimeout:
                        Console.WriteLine(
                            "오류: 네트워크 연결 시간 초과."
                        );
                        break;

                    case SubwayError.InvalidResponse:
                        Console.WriteLine(
                            "오류: 잘못된 응답 데이터입니다."
                        );
                        break;

                    default:
                        Console.WriteLine(
                            "알 수 없는 오류가 발생했습니다."
                        );
                        break;
                }

                break;
        }
    }
}