# 과제 2. Island 모바일 게임 Service 계층 분리 설계

## 설계 기준

과제 1의 도메인 모델을 유지하면서 `Service`를 추가했다. `.fig`의 레벨 잠금 화면, 골든 키 안내, 부스터 선택, `Target`·`Moves`·`Score`, 작업 완료 전후의 `zone 1` 진행도(`2/10` → `4/10`), 승리 보상, 프로필·설정 화면을 근거로 책임을 나눴다. 화면에 적힌 `42`, `24`, `50+` 등은 예시 표시값이며 규칙의 고정 상수로 사용하지 않는다.

## 과제 1과 과제 2 비교

| 항목 | 과제 1: Model + Repository | 과제 2: Model + Service + Repository |
| --- | --- | --- |
| 도메인 모델 | 플레이어, 자원, Zone·Level 진행도, 작업, 부스터, 게임 상태를 표현 | 기존 모델을 사용하고, `LevelOverview`·`TaskOverview`를 Service의 화면용 조회 결과로 추가 |
| Repository 반환형 | `Task<Result<T, RepositoryError>>` | 조회는 `Task<T>` 또는 `Task<T?>`, 저장은 저장된 값을 담은 `Task<T>` |
| Repository 책임 | 데이터 조회·저장 인터페이스를 정의했으나 비즈니스 규칙의 위치는 명시하지 않음 | 데이터 조회·저장만 담당. 레벨 해금, 보유량 판정, 보상 계산은 하지 않음 |
| Service | 없음 | 여러 Repository의 데이터를 조합하고 게임 규칙을 판단 |
| 화면에 전달하는 결과 | Repository 결과를 받은 호출자가 추가 판단을 해야 함 | Service가 `Task<Result<T, GameError>>`를 반환해 성공 데이터와 실패 이유를 제공 |
| 오류 처리 | `RepositoryError`가 Repository 계약에 포함 | 조회 결과 없음은 `null`로 받고, 예상 가능한 저장소·통신 오류와 비즈니스 오류를 Service에서 `GameError`로 구분 |

과제 1은 인터페이스 설계만 있었으므로 Repository 구현에 실제로 비즈니스 로직이 있었다고 단정할 수 없다. 과제 2에서는 앞으로 구현할 때의 책임 경계를 명시한 것이다.

## Service별 책임

| Service | 판단·가공 책임 | 사용하는 Repository 또는 Service |
| --- | --- | --- |
| `ProfileService` | 닉네임·프로필 이미지 입력 검증, 음악·효과음 설정 변경 | `IPlayerRepository`, `ISettingsRepository` |
| `WalletService` | 자원 증감량 검증, 골든 키나 코인 등의 잔액 부족 판정 | `IResourceRepository` |
| `ProgressService` | 레벨 잠금 여부와 별·최고 점수 계산, Zone 진행도 구성, 승리 후 진행도 갱신 | `ILevelRepository`, `ILevelProgressRepository`, `IZoneProgressRepository`, `WalletService` |
| `TaskService` | `Lamp`·`Well` 등 작업의 진행 상태 결합, 완료 가능 여부 판정, 완료 후 Zone 진행도 반영 | `ITaskRepository`, `ProgressService` |
| `GameplayService` | 레벨 시작 가능 여부, 선택한 부스터의 보유량, 목표 달성·남은 이동 횟수·승리 보상 판정 | `IBoosterRepository`, `ProgressService`, `WalletService` |

### 예: 레벨 완료

1. `GameplayService`가 `GameSession`과 `Level`의 목표를 비교해 승리 조건을 판단한다.
2. 성공하면 `ProgressService`가 최고 점수·별·잠금 상태를 갱신하고, `WalletService`가 획득 자원을 반영한다.
3. 각 Repository는 전달받은 상태를 조회하거나 저장한다. Repository가 승리 여부나 보상량을 결정하지 않는다.
4. `GameplayService`가 최종 `Result<GameResult, GameError>`를 화면에 반환한다.

### 예: 작업 완료

`TaskService`는 작업 정의와 플레이어의 진행도를 함께 읽고 완료 가능 여부를 판단한다. 완료되면 `TaskProgress`를 저장하고 `ProgressService`를 통해 `ZoneProgress`를 갱신한다. `.fig`의 작업 전후 화면에 보이는 `2/10`에서 `4/10`으로의 변화가 이 흐름에 해당한다.

## 반환형과 오류 경계

- `GetByIdAsync` 같은 단일 조회는 `Task<T?>`를 사용한다. 데이터가 없으면 Service가 `GameError.NotFound` 또는 해당 상황에 맞는 오류로 바꾼다.
- 목록 조회는 `Task<IReadOnlyList<T>>`를 사용한다. 항목이 없는 정상 상태는 빈 목록이다.
- 저장은 `Task<T>`로 저장된 모델을 반환한다.
- Repository는 저장소·네트워크 실패를 예외로 전달한다. Service는 예상 가능한 실패를 `GameError.DataUnavailable`로 바꿔 UI에 반환한다. 예기치 못한 프로그래밍 오류까지 일괄 변환하지 않는다.
- Service는 입력·규칙을 검사한 뒤 `Task<Result<T, GameError>>`를 반환한다. `Result<TData, TError>` 형식은 기존 C# 수업 예제와 맞췄다.

## 책임 분리 효과와 구현 시 유의점

레벨 목록 화면과 실제 퍼즐 시작 화면은 모두 같은 레벨 해금 규칙을 `ProgressService`에서 사용할 수 있다. 작업 화면과 홈 화면의 Zone 진행도도 같은 계산을 공유한다. Repository는 저장 방식이 바뀌어도 게임 규칙을 수정하지 않아도 되고, Service는 가짜 Repository로 규칙을 독립적으로 시험할 수 있다.

여러 저장이 한 번에 일어나는 레벨 완료·작업 완료에서는 중간 실패로 점수와 보상이 어긋나지 않도록 실제 구현 시 트랜잭션이나 동등한 원자적 갱신이 필요하다. 별 산정식, 골든 키 지급 조건, 보상량은 `.fig`에 명시되지 않아 이 설계에서 수치로 확정하지 않았다. `GameSession`은 이어하기 화면이 없어 저장 대상으로 지정하지 않았다.
