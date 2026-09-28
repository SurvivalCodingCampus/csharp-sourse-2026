# 과제 3. SOLID 원칙 점검

## 검토 대상

「PlantUML 모델 설계」 대화와 과제 1·2의 PlantUML 파일을 기준으로, Service 계층을 분리한 설계의 SOLID 준수 여부와 보완할 부분을 정리한다. 기존 프로젝트의 작성 스타일을 반영한 UML 수정 결과도 포함한다. 실제 구현 코드가 아닌 설계 검토이므로, 메서드 내부 동작에 따라 달라지는 문제는 잠재적 문제로 구분한다.

## 1. SRP — 단일 책임 원칙

- `PlayerProfile`과 `Wallet`을 분리하여 프로필과 재화의 책임을 구분했다.
- `Level`과 `LevelProgress`를 분리하여 레벨 원본 데이터와 플레이어 진행도를 구분했다.
- Repository는 데이터 조회·저장, Service는 입력 검증과 비즈니스 규칙을 담당하도록 설계했다.

**보완할 부분:** `GameSessionService.FinishGameAsync()`가 진행도까지 직접 갱신한다면 `ProgressService.CompleteLevelAsync()`와 책임이 겹칠 수 있다. 게임 세션 상태 관리는 `GameSessionService`, 클리어 기록과 최고 점수 갱신은 `ProgressService`가 담당하도록 경계를 명확히 한다. 다른 Service의 작업을 호출해 흐름을 조정하는 것 자체가 SRP 위반은 아니다.

## 2. OCP — 개방 폐쇄 원칙

현재 `Reward`는 `Coins`, `GoldenKeys`를 직접 보유한다. 새로운 보상 종류가 추가되면 `Reward`와 보상 처리 로직을 함께 수정해야 할 가능성이 있다.

**판단:** 현재 두 종류의 보상만 필요하다면 단순한 구조로 유지할 수 있다. 보상 종류가 자주 늘어날 때 보상 추상화와 처리 방식의 확장을 검토한다. 인터페이스를 추가하는 것만으로 OCP가 자동으로 충족되지는 않으므로, 현 단계에서는 확장 시 검토할 사항으로 기록한다.

## 3. LSP — 리스코프 치환 원칙

Repository의 실제 구현체와 Fake 구현체는 동일한 인터페이스 계약을 지켜야 한다. 예를 들어 `GetLevelAsync()`에서 데이터가 없을 때 한 구현체는 `null`, 다른 구현체는 예외를 반환한다면 호출 측의 처리 방식이 달라질 수 있다.

**보완할 부분:** 성공 반환값, 데이터 없음, 조회·저장 실패의 처리 규칙을 인터페이스 계약으로 정의한다. 현재는 구현체가 제시되지 않아 위반 여부를 확정할 수 없다. LSP는 클래스 상속뿐 아니라 인터페이스 구현체의 대체 가능성에도 적용된다.

## 4. ISP — 인터페이스 분리 원칙

`IProfileRepository`, `IWalletRepository`, `ILevelRepository`, `IProgressRepository`, `IMissionRepository`, `ISettingsRepository`, `IGameSessionRepository`가 도메인별로 나뉘어 있다.

**판단:** 하나의 거대한 Repository보다 책임과 사용 목적이 명확하며, 현재 설계에서 뚜렷한 위반은 보이지 않는다. 실제 사용자가 필요 없는 메서드에 의존하게 되는지는 구현 시 확인한다. 인터페이스를 더 잘게 나누는 것이 항상 좋은 것은 아니다.

## 5. DIP — 의존성 역전 원칙

Service가 구체적인 Repository 구현 클래스가 아닌 인터페이스에 의존하므로 DIP를 적용할 수 있는 구조다.

**수정한 부분:** 기존 UML에는 인터페이스 필드만 있었으나, 수정본에는 모든 Service의 생성자 주입을 명시했다. Day08·09 Repository에서 사용한 C# 기본 생성자 스타일을 적용하면 다음과 같다.

```csharp
public class MissionService(
    IMissionRepository missionRepository,
    IWalletRepository walletRepository)
{
    // 메서드에서 missionRepository와 walletRepository를 사용한다.
}
```

위 코드는 의존성 주입 형태만 보여 주는 예시다. UML에서는 `<<primary constructor>>`와 생성자 항목으로 표현했다. 생성자 표기가 없다는 사실만으로 DIP 위반을 확정할 수는 없다. View에서 Service 교체나 테스트 대역이 필요하다면 `IMissionService` 등의 추상화를 검토하되, 모든 Service에 일괄적으로 인터페이스를 추가할 필요는 없다.

## 6. 추가 점검 — Result, 의존 방향, 설계 누락

- **Result 계약:** Day09의 `Result<TData, TError>`에 맞춰 추상 record와 `Success(Data)`, `Failure(Error)`로 수정했다. 성공과 실패를 타입으로 구분하며, 기존 `IsSuccess`·`Value` 동시 보유 구조를 대체한다. Repository의 `Task<T>` 실패를 Service에서 어떤 `GameError`로 변환할지는 추가로 정의해야 한다.
- **의존 방향:** Service가 UI나 특정 저장 방식에 의존하지 않도록 유지한다. 안정된 계약을 향해 의존하도록 하는 SDP 관점에서도 확인할 사항이다. 현재 UML만으로 모듈의 실제 안정성을 단정할 수는 없다.
- **모델 생략:** 과제 1에 있는 `LevelGoal`, `GoalType`, `Board`, `Tile`, `TileType`과 관련 관계는 과제 2에서 생략되어 있다. 수정본에 Service 중심의 요약도라는 주석을 추가했다. 전체 모델 구조는 과제 1과 함께 확인한다.
- **보상 저장 일관성:** `MissionService`에서 Wallet과 Mission을 각각 저장하므로, 한쪽만 저장된 경우 재시도 시 보상이 중복 지급되지 않도록 실패 처리 방침을 정한다. 이는 SOLID 위반 여부와 별개로 확인할 설계 사항이다.

## 7. 개선 우선순위와 트레이드오프

1. Service 간 세션 종료·진행도 갱신 책임을 명확히 한다.
2. 생성자 주입 표기는 반영했으며, Repository의 반환·실패 계약을 추가로 명시한다.
3. Service의 Result 변환 규칙과 보상 저장 실패 처리 방침을 정한다.
4. 전체 설계 검토 시 과제 1의 모델과 관계를 함께 확인한다.
5. 보상 종류 확장이나 Service 교체 요구가 생길 때 추가 추상화를 검토한다.

Service를 분리하면 규칙을 재사용하고 테스트하기 쉬워지지만, 클래스 수와 의존성 연결 작업이 늘어난다. 단순 조회·저장만 필요한 기능까지 계층을 일률적으로 추가하지 않고, 실제 규칙과 변경 가능성에 맞춰 적용한다.

## 8. 기존 작성 스타일 반영

| 확인한 프로젝트 코드 | UML에 반영한 방식 |
|---|---|
| Day08·09의 `Pokemon`, `SubwayArrival` record | 데이터 모델을 `<<record>>`, 생성자, `get; init;` 속성으로 표현 |
| Day08·09 Repository의 기본 생성자 | Service 생성자에 Repository 인터페이스를 주입하도록 표시 |
| Day09의 `Result<TData, TError>` | 추상 record와 중첩 `Success`·`Failure` record 및 상속 관계 표현 |
| `Data.Models`, `Data.Interfaces`, `Common`, `Common.Error` 네임스페이스 | UML 패키지 이름에 반영 |
| PascalCase 속성·메서드, camelCase 일반 매개변수, 비동기 메서드의 `Async` 접미사 | 기존 이름을 유지하면서 메서드 선언을 한 줄로 정리 |

초기 프로젝트에는 일반 클래스와 변경 가능한 속성도 있어 모든 코드의 스타일이 동일하지는 않다. 이번에는 최근 Day08·09 스타일을 기준으로 삼았다. 위치 기반 record의 생성자 매개변수는 기존 record 코드처럼 PascalCase로 표기한다.

모델의 `get; init;` 속성은 생성 후 직접 대입할 수 없으므로, 구현 시 `wallet with { Coins = ... }`처럼 변경된 값을 가진 새 record를 만들어 저장한다. 과제 요구에 맞춰 Repository는 `Task<T>`, Service는 `Task<Result<T, GameError>>`를 반환하는 역할을 유지했다.

## 결론

현재 설계는 책임 분리와 Repository 인터페이스 의존을 통해 SRP·ISP·DIP를 고려하고 있다. 이번 수정으로 생성자 주입과 성공·실패 결과 구조를 명확히 했다. Service 간 책임 경계와 실패 계약은 추가로 정해야 하며, OCP 확장 구조와 Service 인터페이스 추가는 실제 필요에 따라 결정하는 것이 적절하다.

## 참고 자료

- 대화: 「PlantUML 모델 설계」의 과제 1·2·3 설명
- 설계 파일: `과제_1_Model_Repository_설계.puml`, `과제_2_Service_계층_분리_설계.puml`
- 검토 범위: 대화 본문과 로컬 설계 파일. 대화에 첨부된 슬라이드와 UI 이미지 원본은 이번 문서 작성에서 별도로 검증하지 않았다.
