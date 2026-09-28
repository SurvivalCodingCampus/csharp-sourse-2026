# DataSource 가이드

AI는 [공통 가이드](README.md)를 적용한다. 이번 요청에서 지정한 **DTO 반환 규칙**이 과거 `Response` 반환 코드보다 우선한다.

## [규칙]

- DataSource는 외부 데이터(API·DB·파일) 접근만 담당한다. 외부 구조를 보존한 DTO를 반환하고 도메인 Model로 변환하지 않는다.
- 외부 JSON을 DTO로 역직렬화하는 것은 허용한다. “날것의 데이터”는 Model로 해석하기 전의 데이터라는 뜻이며 JSON 문자열만 반환하라는 뜻이 아니다.
- 조회는 `Task<Dto>`, 저장은 DTO를 입력받는 `Task` 형태로 계약을 명시한다. 저장 결과 데이터가 필요할 때만 응답 DTO를 반환한다.
- `HttpClient`나 경로는 생성자로 전달받는다. DataSource 내부에서 매번 `HttpClient`를 만들거나 테스트 경로를 고정하지 않는다.
- 실제 I/O는 비동기 메서드와 `await`를 사용한다. 동기 I/O를 `Task.FromResult`로 감싸 비동기처럼 보이게 하지 않는다.
- HTTP 상태·외부 응답 형식에 맞는 실패를 전달한다. 통신 실패를 정상 DTO, `null`, 가짜 상태값으로 숨기지 않는다.
- HTTP 200이어도 본문에 기술적 오류 코드가 들어오는 API는 응답 계약에 맞게 확인한다. 게임 규칙 판단과 API의 기술적 오류 판별은 구분한다.
- 요청·응답 객체는 필요한 범위에서 해제한다. 주입받은 `HttpClient`의 수명은 생성한 쪽에서 관리한다.

## [Good 예시 코드]

예시 계약: `GET wallet`은 `WalletDto`, `PUT wallet`은 저장 성공 상태를 반환한다. API는 HTTP 상태로 실패를 표현한다고 가정한다. 호출 측에서 `HttpClient.BaseAddress`를 설정한다. 실제 URL이나 API 규격은 이 예시에 포함하지 않았다.

```csharp
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GuidelineExample;

public interface IWalletDataSource
{
    Task<WalletDto> GetWalletAsync();
    Task SaveWalletAsync(WalletDto wallet);
}

public class WalletApiDataSource(HttpClient httpClient) : IWalletDataSource
{
    public async Task<WalletDto> GetWalletAsync()
    {
        using HttpResponseMessage response = await httpClient.GetAsync("wallet");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<WalletDto>()
            ?? throw new JsonException("지갑 응답이 null입니다.");
    }

    public async Task SaveWalletAsync(WalletDto wallet)
    {
        using HttpResponseMessage response =
            await httpClient.PutAsJsonAsync("wallet", wallet);

        response.EnsureSuccessStatusCode();
    }
}
```

반환 전에 Mapper를 호출하지 않는다. `HttpRequestException`, `JsonException` 등 기술적 실패는 상위로 전달한다. 토큰 취소를 지원하는 기능을 추가할 경우 인터페이스부터 `CancellationToken`을 전달하고 사용자 취소와 타임아웃을 구분한다.

## [Bad 예시 코드]

**현재 코드에서 확인한 개선 대상:** 다음은 [Day05 JsonFileDataSource.cs](../Day05_DataSource/DataSources/JsonFileDataSource.cs)의 기존 처리 흐름을 축약한 것이다. 당시의 오류 경험이 기록된 것은 아니며 신규 규칙과 비교한 개선 사례다.

```csharp
public Task<List<Person>> GetPeopleAsync()
{
    List<Person>? people = JsonSerializer.Deserialize<List<Person>>(
        File.ReadAllText(Path), _option);

    if (people is null) throw new Exception("Path not found");
    return Task.FromResult(people);
}
```

- `Person` 모델을 직접 반환해 저장 형식과 Model이 결합된다.
- `File.ReadAllText`는 동기 I/O다. 뒤의 `Task.FromResult`가 읽기 작업을 비동기로 바꾸지는 않는다.
- JSON의 null과 파일 경로 문제를 같은 일반 예외 메시지로 취급한다.

파일 기반 신규 구현에서는 `ReadAllTextAsync` 또는 비동기 스트림을 사용하고 `PersonDto`를 반환한다. 모델 변환은 Repository가 Mapper에 위임한다.

**학습 기록에 남은 경험:** [Day06 TIL](../Day06_Model_Repository/TIL.md)에는 테스트 JSON 경로가 달라 생성자로 경로를 받도록 수정한 과정이 있다. 파일 경로를 외부에서 주입해야 실제 데이터와 테스트 데이터를 구분할 수 있다.

## [확인 기준]

- 반환 타입이 Model이나 Result가 아닌 DTO인가?
- 통신, 역직렬화 외의 비즈니스 판단이 없는가?
- HTTP 실패, 잘못된 JSON, JSON null을 각각 확인했는가?
- 단위 테스트에서 실제 서버·실제 DB에 연결하지 않고 응답을 주입할 수 있는가?

## [근거 파일]

- [Day09 PokemonApiDataSource.cs](../Day09_Result/Data/DataSources/PokemonApiDataSource.cs)
- [Day09 SubwayDataSource.cs](../Day09_Result/Data/DataSources/SubwayDataSource.cs)
- [Day05 JsonFileDataSource.cs](../Day05_DataSource/DataSources/JsonFileDataSource.cs)
- [Day06 TIL](../Day06_Model_Repository/TIL.md)
