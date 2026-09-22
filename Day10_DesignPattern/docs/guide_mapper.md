# Mapper 가이드

AI는 [공통 가이드](README.md)를 적용하고, Day10의 record 모델과 Day08·09의 static 확장 메서드 스타일을 따른다.

## [규칙]

- Mapper는 DTO ↔ Model 변환만 담당한다. HTTP, 파일, DB 호출과 저장, 화면 출력을 하지 않는다.
- 상태를 보관하지 않는 `static` 클래스와 확장 메서드로 작성한다.
- 입력이 같으면 같은 의미의 값을 반환한다. DTO를 직접 변경하거나 현재 시간·랜덤값을 끼워 넣지 않는다.
- 필수 필드 누락과 정상 기본값을 구분한다. 필수 데이터 누락 시의 예외 또는 선택 필드의 기본값 정책을 먼저 명시한다.
- 잔액 부족, 레벨 잠금, 보상 수령 가능 여부 같은 비즈니스 판단은 Service에 둔다.
- nullable 중첩 객체와 빈 목록을 각각 처리한다. `list?[0]`은 null만 방어하며 빈 목록은 방어하지 못한다.
- 문자열 검사에는 `string.IsNullOrWhiteSpace(value)`처럼 null을 처리하는 방식을 사용한다.
- Model의 각 필드가 어떤 DTO 값에서 나오는지 명확히 하고 모든 변환 결과를 테스트한다.

## [Good 예시 코드]

예시 지갑 스키마에서는 두 수량 모두 필수다. 누락 시 `JsonException`으로 잘못된 외부 데이터를 알린다. 음수 허용 여부 등 도메인 정책은 별도 Service 규칙이며 여기서 임의로 보정하지 않는다.

```csharp
using System.Text.Json;

namespace GuidelineExample;

public record Wallet(int Coins, int GoldenKeys);

public static class WalletMapper
{
    public static Wallet ToModel(this WalletDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Wallet(
            dto.Coins ?? throw new JsonException("coins 필드가 없습니다."),
            dto.GoldenKeys ?? throw new JsonException("golden_keys 필드가 없습니다."));
    }

    public static WalletDto ToDto(this Wallet wallet)
    {
        ArgumentNullException.ThrowIfNull(wallet);

        return new WalletDto
        {
            Coins = wallet.Coins,
            GoldenKeys = wallet.GoldenKeys
        };
    }
}
```

Day08의 빈 DTO를 기본값으로 변환하는 정책을 모든 도메인에 강제하지 않는다. 선택 필드는 기본값을 사용할 수 있지만 지갑의 필수 수량을 `0`으로 바꾸면 데이터 누락을 정상 잔액으로 오해하게 된다.

## [Bad 예시 코드]

**학습 기록에 남은 경험:** [Day09 TIL](../Day09_Result/TIL.md)에 다음 변경 전 코드와 `FirstOrDefault()`로 수정한 코드가 남아 있다.

```csharp
dto.RealtimeArrivalList?[0].StatnNm ?? "역이름 없음"
```

목록이 `[]`이면 첫 원소가 없어 인덱스 예외가 발생할 수 있다. null 조건부 접근만으로 빈 목록까지 안전해지지는 않는다.

```csharp
// 해당 프로젝트의 기존 기본값 정책을 유지한 개선 형태
dto.RealtimeArrivalList?.FirstOrDefault()?.StatnNm ?? "역이름 없음"
```

첫 항목만 반환하는 것이 요구사항인 경우에만 이 방법을 사용한다. 여러 도착 정보를 반환해야 한다면 목록 전체를 변환해야 한다.

## [확인 기준]

- DTO가 null인 경우, 필수 필드 누락, 선택 필드 누락, 빈 목록을 구분했는가?
- 타입뿐 아니라 ID·이름·이미지·수량 등 필요한 모든 변환값을 검증했는가?
- DTO를 변경하지 않으며 I/O를 호출하지 않는가?
- 날짜나 상태처럼 의미가 다른 필드를 이름만 보고 잘못 연결하지 않았는가?

## [근거 파일]

- [Day09 SubwayArrivalMapper.cs](../Day09_Result/Data/Mapper/SubwayArrivalMapper.cs)
- [Day09 PokemonMapper.cs](../Day09_Result/Data/Mapper/PokemonMapper.cs)
- [Day08 PokemonMapperTest.cs](../Day08_DTO_Mapper_Test/Data/Mapper/PokemonMapperTest.cs)
- [Day09 TIL](../Day09_Result/TIL.md)
