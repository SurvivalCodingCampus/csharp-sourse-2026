# DTO 가이드

AI는 [공통 가이드](README.md)의 우선순위와 Day10의 Model 분리 원칙을 적용한다. DTO 구현은 Day09의 `Data.DTOs`와 `JsonPropertyName` 사용을 참고한다.

## [규칙]

- DTO는 외부 API·DB·파일의 데이터 구조를 전달하는 객체다. 도메인 Model과 분리한다.
- 외부 필드 이름은 `JsonPropertyName`으로 연결하고 C# 속성은 PascalCase, 타입 이름은 `Dto` 접미사를 사용한다.
- 누락·null 가능성이 있는 값은 nullable로 선언한다. 값이 없는 것과 실제 `0`, `false`인 것을 구분한다.
- DTO 안에 계산, 보상 지급, DB 접근, Model 변환 메서드를 넣지 않는다.
- 성공 응답과 오류 응답의 구조가 다르면 두 구조에서 필요한 필드를 표현한다. HTTP 상태만으로 본문의 성공 여부를 단정하지 않는다.
- 도메인에서 필수인 값의 존재 여부와 변환 정책은 Mapper·Repository 계약으로 정한다. DTO 초기값으로 가짜 정상 데이터를 채우지 않는다.
- 확인되지 않은 필드를 임의로 추가하지 않는다. 필요한 필드만 사용하는 DTO인지 전체 응답 DTO인지 명시한다.

## [Good 예시 코드]

예시 API의 지갑 응답 중 필요한 두 필드만 표현한다. `null`은 누락된 값, `0`은 실제 수량이다. 이 JSON 스키마는 기존 Day10 UML에 없는 교육용 계약이다.

```csharp
using System.Text.Json.Serialization;

namespace GuidelineExample;

public class WalletDto
{
    [JsonPropertyName("coins")]
    public int? Coins { get; set; }

    [JsonPropertyName("golden_keys")]
    public int? GoldenKeys { get; set; }
}
```

DTO는 변경 가능한 속성으로 역직렬화를 받고, 내부 Model은 Day10처럼 `record Wallet(int Coins, int GoldenKeys)`로 분리한다. 외부 값이 모두 있어야 모델로 변환할 수 있다는 조건은 [Mapper](guide_mapper.md)에 둔다.

## [Bad 예시 코드]

**학습 기록에 남은 경험:** [Day09 TIL](../Day09_Result/TIL.md)에는 오류 응답에서 `errorMessage` 없이 최상위 `code`가 오는 경우를 처리하기 위해 DTO를 수정한 과정이 있다. 아래는 수정 전의 구조를 문제 설명용으로 재구성한 코드다.

```csharp
public class SubwayArrivalResponseDto
{
    public ErrorMessageDto? ErrorMessage { get; set; }
    public List<RealtimeArrivalDto>? RealtimeArrivalList { get; set; }
    // 최상위 code를 받을 속성이 없다.
}

string code = dto.ErrorMessage?.Code ?? "Unknown";
```

오류 코드가 최상위에 있으면 실제 오류를 놓친다. 현재 코드는 `[JsonPropertyName("code")] public string? Code { get; set; }`를 추가하고 `dto.ErrorMessage?.Code ?? dto.Code`를 확인하도록 개선되어 있다.

**AI가 반복하지 말아야 할 점:** 응답 샘플 하나만 보고 모든 응답의 구조가 같다고 가정하지 않는다. 오류 응답도 별도 fixture로 역직렬화해 확인한다.

## [확인 기준]

- 누락된 수량은 `null`, 실제 수량 `0`은 `0`으로 구분되는가?
- JSON 필드 이름과 속성이 정확히 연결되는가?
- DTO가 도메인 규칙이나 화면 문구를 포함하지 않는가?
- 성공·오류 응답의 구조 차이를 테스트했는가?

## [근거 파일]

- [Day09 SubwayArrivalResponseDto.cs](../Day09_Result/Data/DTOs/SubwayArrivalResponseDto.cs)
- [Day09 PokemonDTO.cs](../Day09_Result/Data/DTOs/PokemonDTO.cs)
- [Day09 TIL](../Day09_Result/TIL.md)
