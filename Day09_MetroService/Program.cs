using System.Text;
using Day09_MetroService.Common;
using Day09_MetroService.Common.Error;
using Day09_MetroService.Model;
using Day09_MetroService.Reposetory;
using MetroDataSource = Day09_MetroService.DataSource.DataSource;
namespace Day09_MetroService; 
internal static class Program //같은 어셈블리안에서만 접근 가능한 정적(생성자 사용 없이 메모리에 할당) 클래스 (현재는 같은 프로젝트안에서만 사용) => 클래스 자체의 캡슐화
{
    private static async Task<int> Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var key = Environment.GetEnvironmentVariable("SEOUL_SUBWAY_API_KEY")?.Trim();
        if (string.IsNullOrWhiteSpace(key)) key = "sample";
        var station = args.Length > 0 ? string.Join(" ", args).Trim() : "서울";
        if (key == "sample")
        {
            Console.WriteLine("샘플 모드: 서울역 도착정보 최대 5건을 조회합니다.");
            if (station != "서울")
            {
                Console.WriteLine("다른 역은 SEOUL_SUBWAY_API_KEY 환경 변수에 인증키를 설정하세요.");
                return 1;
            }
        }
        using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        IMetroRepository repository = new Repository(new MetroDataSource(client, key));
        var result = await repository.GetByStationNameAsync(station);
        switch (result)
        {
            case Result<IReadOnlyList<Metro>, MetroError>.Success success:
                Console.WriteLine($"\n[{station}] 실시간 지하철 도착정보");
                foreach (var train in success.data)
                {
                    Console.WriteLine($"\n{train.LineName} | {train.Direction} | {train.Destination}");
                    Console.WriteLine($"  {train.ArrivalMessage} / {train.LocationMessage}");
                    Console.WriteLine($"  열차: {train.TrainNumber} ({train.TrainType})");
                    Console.WriteLine($"  데이터 생성: {train.ReceivedAt?.ToString("yyyy-MM-dd HH:mm:ss zzz") ?? "정보 없음"}");
                    Console.WriteLine($"  예상 대기: {WaitText(train)}");
                }
                Console.WriteLine("\n출처: 서울 열린데이터광장 / TOPIS. 실제 운행 상황과 차이가 있을 수 있습니다.");
                return 0;
            case Result<IReadOnlyList<Metro>, MetroError>.Error error:
                Console.WriteLine(ErrorText(error.error));
                return 1;
            default: return 1;
        }
    }
    private static string WaitText(Metro train)
    {
        if (train.ArrivalSeconds is null or 0) return "도착 안내 메시지 참고";
        if (train.ReceivedAt is null) return $"수신 당시 {train.ArrivalSeconds}초 (생성 시각 없음)";
        var elapsed = Math.Max(0, (DateTimeOffset.UtcNow - train.ReceivedAt.Value).TotalSeconds);
        var remaining = train.ArrivalSeconds.Value - elapsed;
        return remaining <= 0 ? "예정 시각 경과 — 도착 안내 메시지 참고"
            : $"약 {(int)remaining / 60}분 {(int)remaining % 60}초 (생성 시각 기준 보정)";
    }
    private static string ErrorText(MetroError error) => error switch
    {
        MetroError.InvalidStationName => "역 이름을 입력하세요.",
        MetroError.NotFound => "요청한 정보를 찾을 수 없습니다.",
        MetroError.NoData => "현재 제공되는 도착정보가 없습니다. 역 이름과 운행 시간을 확인하세요.",
        MetroError.NetworkTimeOut => "요청 시간이 초과되었습니다.",
        MetroError.NetworkFailure => "네트워크 연결을 확인하세요.",
        MetroError.AuthenticationFailed => "서울시 API 인증키를 확인하세요.",
        MetroError.RateLimitExceeded => "요청 한도를 초과했습니다. 잠시 후 다시 시도하세요.",
        MetroError.InvalidRequest => "API 요청 인자가 올바르지 않습니다.",
        MetroError.ServiceUnavailable => "서울시 API 서비스를 일시적으로 사용할 수 없습니다.",
        MetroError.InvalidResponse => "API 응답 형식을 확인할 수 없습니다.",
        _ => "알 수 없는 오류가 발생했습니다."
    };
    
}
