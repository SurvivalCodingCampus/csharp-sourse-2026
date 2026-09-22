# Repository 가이드

AI는 [공통 가이드](README.md)를 적용하고 **Day10의 데이터 접근 역할**을 우선한다. Day09의 Repository 직접 Result 반환 구조를 신규 기본값으로 복제하지 않는다.

## [규칙]

- Repository는 도메인별 데이터 조회·저장 계약을 제공한다. 잔액 부족, 슬롯 제한, 레벨 진입, 보상 수령 가능 여부는 Service가 판단한다.
- 인터페이스는 `Data.Interfaces`, 구현체는 `Data.Repository`에 둔다. 거대한 통합 Repository를 만들지 않는다.
- DataSource 인터페이스를 생성자로 주입한다. 직접 HTTP·파일 접근을 하거나 내부에서 구체 DataSource를 생성하지 않는다.
- 조회한 DTO는 Mapper로 Model로 변환한다. 저장할 Model도 Mapper로 DTO로 변환해 DataSource에 전달한다.
- Day10에 맞춰 조회는 `Task<Model>`, 저장은 `Task`를 반환한다. `Result` 생성은 Service에서 수행한다.
- 데이터가 없을 때 null인지 예외인지 계약을 정하고 실제 구현체와 Fake가 동일하게 지킨다. 예시의 non-null 반환 계약은 `KeyNotFoundException`을 사용한다.
- 알려진 기술적 오류를 계약상 오류로 바꿀 때 원본 예외를 보존한다. 모든 예외를 하나로 뭉개거나 실패를 가짜 모델로 반환하지 않는다.
- 비동기 호출은 끝까지 `await`로 연결한다. 필요한 캐시 정책이 없다면 임의 캐시를 추가하지 않는다.

## [Good 예시 코드]

Day10의 `IWalletRepository`와 `Wallet` 모델을 따른다. 다음 예외 계약은 UML에 없던 구현 세부 사항을 예시용으로 명시한 것이다: HTTP 404는 `KeyNotFoundException`, 나머지 HTTP 실패는 `HttpRequestException`, 데이터 형식 오류는 `JsonException`을 전달한다.

```csharp
using System.Net;
using System.Net.Http;

namespace GuidelineExample;

public interface IWalletRepository
{
    Task<Wallet> GetWalletAsync();
    Task SaveWalletAsync(Wallet wallet);
}

public class WalletRepository(IWalletDataSource source) : IWalletRepository
{
    public async Task<Wallet> GetWalletAsync()
    {
        try
        {
            WalletDto dto = await source.GetWalletAsync();
            return dto.ToModel();
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            throw new KeyNotFoundException("지갑을 찾을 수 없습니다.", e);
        }
    }

    public async Task SaveWalletAsync(Wallet wallet)
    {
        try
        {
            await source.SaveWalletAsync(wallet.ToDto());
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            throw new KeyNotFoundException("저장할 지갑을 찾을 수 없습니다.", e);
        }
    }
}
```

Repository는 수량을 차감하지 않는다. 차감 규칙은 [Result 가이드의 WalletService](guide_result.md)가 처리한다. 다중 사용자 환경의 동시 저장 충돌이나 여러 저장소의 원자성은 별도 요구사항이며 이 단순 예시가 보장하지 않는다.

## [Bad 예시 코드]

**현재 코드에서 확인한 개선 대상:** [Day06 InventoryRepository.cs](../Day06_Model_Repository/DataSources/InventoryRepository.cs)의 `AddItemAsync()` 일부다.

```csharp
var findItem = itemList.Find(n => n.ItemId == item.ItemId);

if (findItem != null && findItem.Count + item.Count <= MaxStack)
{
    findItem.Count += item.Count;
    await Source.SaveAllItemsAsync(itemList);
    return true;
}
```

저장소가 최대 스택 규칙과 수량 변경까지 담당한다. Day06 학습 단계의 구현을 잘못이라고 단정하는 것이 아니라, 최신 Day10의 Service 분리 기준에서는 해당 규칙을 Service로 옮겨야 한다는 뜻이다.

**학습 기록에 남은 경험:** [Day06 TIL](../Day06_Model_Repository/TIL.md)에는 `Contains(item)`로 원하는 아이템 일치 판정이 되지 않아 ID로 `Find`하도록 변경한 기록이 있다. 엔티티의 동일성을 판단할 기준도 먼저 정해야 한다. 규칙이 Service로 이동해도 이 기준은 유지한다.

## [확인 기준]

- Repository에는 데이터 접근과 변환만 있는가?
- Task 반환 계약과 데이터 없음 처리 방식이 인터페이스·Fake에서 동일한가?
- 실패를 ID `0`, ID `-1`, 빈 정상 모델로 감추지 않는가?
- 실제 통신 없이 Mapper 호출 결과와 예외 전달을 검증할 수 있는가?

## [근거 파일]

- [Day10 과제 2 UML](../Day10_DesignPattern/과제_2_Service_계층_분리_설계.puml)
- [Day06 InventoryRepository.cs](../Day06_Model_Repository/DataSources/InventoryRepository.cs)
- [Day06 TIL](../Day06_Model_Repository/TIL.md)
- [Day08 PokemonRepository.cs](../Day08_DTO_Mapper/Data/Repository/PokemonRepository.cs)
