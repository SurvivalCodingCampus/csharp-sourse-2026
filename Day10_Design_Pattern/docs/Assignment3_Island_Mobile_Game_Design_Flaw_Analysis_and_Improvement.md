# 과제 3. Island 모바일 게임 설계 결함 분석 및 보강

## 1. 검토 범위

과제 1의 `island_game_model_repository.puml`, 과제 2의 `island_game_service_layer.puml`, 원본 `.fig`의 화면 구조를 비교했다. Rider 프로젝트의 `Day10_Design_Pattern`에는 세 PlantUML 파일과 기본 `Program.cs`가 있으나, 게임용 Repository·Service 구현 클래스는 없다. 따라서 **구현 코드에 비즈니스 로직이나 데이터 가공 코드가 실제로 섞여 있는지**는 확인할 수 없다. 아래 분석은 다이어그램으로 확인한 설계와 향후 구현 시의 위험을 구분한다.

과제 1의 Repository 인터페이스에는 조회·저장 메서드만 정의돼 있다. 과제 2에서는 `GetPlayableLevelAsync`, `RecordWinAsync`, `CompleteTaskAsync`, `CompleteLevelAsync`처럼 해금·진행·작업·승리 처리를 맡을 Service 메서드가 추가됐다. 즉, **설계상으로는 데이터 접근과 게임 규칙의 책임이 분리됐다.** 실제 구현에서도 이 경계가 지켜지는지는 아직 판단할 수 없다. DTO의 형식 변환은 Mapper가, 통신은 DataSource가 맡고, Repository는 이를 조합해 도메인 모델을 조회·저장하는 방향으로 구현한다.

## 2. 원칙별 분석

| 항목 | 확인한 근거 | 판정 | 보강 |
| --- | --- | --- | --- |
| SRP: Repository 책임 | 과제 1의 Repository에는 조회·저장 메서드만 있다. `Task<Result<T, RepositoryError>>`는 데이터 접근 오류를 표현한 계약이다. | Repository에 게임 규칙이나 데이터 가공이 섞였다는 증거는 없다. `Result` 사용 자체도 SRP 위반은 아니다. 구현 위험은 남는다. | DataSource는 통신, Mapper는 형식 변환, Repository는 도메인 모델 조회·저장 조합, Service는 해금·목표 달성·보상·작업 완료 판정을 맡는다. |
| SRP: Service 책임 | 과제 2의 `ProfileService`는 프로필 편집과 음악·효과음 설정을 함께 맡고, `ProgressService`는 레벨 진행과 Zone 진행을 함께 맡는다. | 서로 다른 변경 이유가 한 클래스에 모여 있다. | `ProfileService`/`SettingsService`, `LevelProgressService`/`ZoneProgressService`로 분리한다. |
| 중복 상태 | `TaskProgress.IsCompleted`와 `ZoneProgress.CompletedObjectives`를 각각 저장하는 Repository가 있다. `.fig`에는 작업 이후 Zone 수치가 `2/10`에서 `4/10`으로 바뀐다. | 둘을 별도로 갱신하면 수치가 어긋날 수 있는 구조적 위험이 있다. | `ZoneProgress`를 저장 엔터티가 아닌 조회 결과로 두고, `ZoneProgressService`가 작업·목표 기록으로 계산한다. 실제 목표 종류가 추가되면 계산 입력을 확장한다. |
| DIP / SDP: Repository 의존 | 과제 2의 Service 구현은 `IPlayerRepository`, `ILevelRepository` 등 Repository 인터페이스에 의존한다. | 데이터 접근 구현을 교체하기 쉬운 방향이다. 단, 인터페이스가 있다는 사실만으로 SDP 충족이나 실제 의존성 주입이 검증되지는 않는다. | 구현보다 안정적인 계약으로 의존 방향을 유지한다. 도메인 모델 같은 안정된 값 객체까지 무조건 인터페이스로 감쌀 필요는 없다. |
| DIP / SDP: Service 의존 | 과제 2에는 `GameplayService → ProgressService/WalletService`, `TaskService → ProgressService`, `ProgressService → WalletService`가 구체 클래스로 연결돼 있다. | 기능 변경 시 상위 Service가 구체 구현에 묶이는 결함이 확인된다. | `ILevelProgressService`, `IZoneProgressService`, `IWalletService` 등 서비스 계약을 두고 구현을 주입한다. |
| Result 패턴 | 과제 1은 Repository가 `Result`를 반환한다. 과제 2는 Repository가 `Task<T>` 계열을, Service가 `Task<Result<T, GameError>>`를 반환한다. | 과제 2 설계의 Service 경계에서 Result 패턴은 누락되지 않았다. 실제 오류 변환 코드는 아직 없다. | 조회 결과 없음, 저장소 오류, 게임 규칙 실패를 Service에서 명시적으로 `GameError`에 대응시킨다. |

## 3. 오류 처리 보강

과제 2와 보강안의 Repository 계약은 `Task<T>` 또는 `Task<T?>`이다. 단일 조회에서 `null`은 데이터 없음, 목록의 빈 값은 정상적인 ‘항목 없음’을 뜻한다. Repository가 임의의 게임 규칙 오류를 만들지 않도록 한다.

| 발생 상황 | Service의 처리 |
| --- | --- |
| 프로필·레벨 등 단일 조회 결과 없음 | `GameError.NotFound` |
| 레벨 진행 상태가 잠김 | `GameError.LevelLocked` |
| 부스터 재고 또는 자원 부족 | `GameError.BoosterUnavailable` 또는 `GameError.InsufficientResource` |
| 퍼즐 목표 미달 | `GameError.TargetNotMet` |
| 예상 가능한 저장소·네트워크 실패 | `GameError.DataUnavailable` |
| 호출 취소 | 취소를 그대로 전달하고 게임 실패로 변환하지 않음 |

예기치 못한 프로그래밍 오류까지 일괄적으로 `DataUnavailable`로 바꾸면 결함이 숨겨진다. `Result<TData, TError>`의 성공·오류 형태는 기존 C# 수업 예제와 맞춘다.

## 4. 보강 후 구조

보강 다이어그램 `island_game_design_review_refined.puml`에는 7개의 Service 계약과 구현, 7개의 Repository 계약이 있다. 화면은 Service 계약을 사용하고, Service 구현은 필요한 Repository 계약 또는 다른 Service 계약에 의존한다. `IZoneProgressRepository`는 제거하고 `ZoneProgress`를 계산된 조회 모델로 표시했다.

레벨 목록과 실제 게임 시작 화면은 같은 `ILevelProgressService`의 해금 판정을 사용한다. 작업 화면에서 `Lamp`와 `Well`을 완료하면 `TaskProgress`가 저장되고, `IZoneProgressService`가 Zone 진행도를 계산해 홈 화면의 수치를 제공한다. 승리 화면의 점수·보상은 `GameplayService`가 규칙을 판단한 뒤 `ILevelProgressService`와 `IWalletService`를 통해 반영한다.

## 5. 구조 변경의 이점과 트레이드오프

- **이점:** 저장 방식 변경이 게임 규칙으로 전파되는 범위가 줄고, 가짜 Repository로 해금·보상·작업 규칙을 독립적으로 검증할 수 있다. 화면 여러 곳에서 같은 판정 결과를 재사용할 수 있다.
- **비용:** Service 인터페이스와 구현 수가 늘어 파일 탐색과 의존성 구성이 복잡해진다. `ZoneProgress` 계산에는 작업 기록 조회가 필요하므로, 데이터가 커지면 캐시나 집계 저장을 검토해야 한다.
- **남은 구현 위험:** 레벨 완료 시 진행도와 보상을 여러 저장소에 기록한다면 중간 실패나 중복 호출로 상태가 어긋날 수 있다. 실제 저장 방식에 맞는 트랜잭션 또는 중복 처리 방지 키가 필요하다. 설계도만으로 원자성을 보장하지는 않는다.
- **확정되지 않은 규칙:** 별 산정식, 골든 키 지급 조건, Zone 목표의 세부 집계 방식은 `.fig`에 없다. 이 값은 구현 전에 별도로 정의해야 한다.

## 결론

과제 1의 Repository 인터페이스만으로 SRP 위반을 단정할 수는 없다. 과제 2는 규칙을 Service로 옮기는 방향을 제시했지만, 구체 Service 의존과 중복 저장 가능성이 남아 있었다. 보강안은 책임을 더 작게 나누고 의존 대상을 인터페이스로 통일하며, Zone 진행도 계산의 단일 근거를 마련한다. 실제 C# 구현의 동작과 오류 처리는 별도 구현·시험이 필요하다.
