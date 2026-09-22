# Test 설계 가이드

> 대상 환경: Unity Test Framework / NUnit  
> 목적: Domain 규칙, Service의 저장 조건, 변환 및 저장 일관성을 빠르게 검증한다.

## [규칙]

- 순수 C# Domain과 Service 테스트는 Unity EditMode에서 실행한다.
- 테스트는 Arrange → Act → Assert 구조를 따른다.
- Domain 테스트는 하나의 불변조건과 상태 변화에 집중한다.
- Service 테스트에는 실제 JSON 파일 대신 `FakeGameRepository`를 주입한다.
- 성공 경로뿐 아니라 실패 후 상태 보존과 저장 미호출을 검증한다.
- Mapper는 `Domain → DTO → Domain` 왕복 변환을 검증한다.
- Repository는 오류 변환과 새 인스턴스 반환을 검증한다.
- JsonDataSource 테스트에서만 임시 파일을 사용한다.
- 테스트 간 파일, 상태, 싱글턴을 공유하지 않는다.
- DTO의 단순 getter/setter처럼 의미 없는 구현 세부사항은 테스트하지 않는다.
- PlayMode 테스트는 MonoBehaviour, UI, 씬 연결이 생겼을 때만 추가한다.

## [우선순위]

| 우선순위 | 대상 | 핵심 검증 |
| --- | --- | --- |
| P0 | Domain | 불변조건과 실패 후 상태 보존 |
| P0 | GameService | 성공 시 1회 저장, 실패 시 저장하지 않음 |
| P1 | Mapper | 왕복 변환과 손상 데이터 거부 |
| P1 | Repository | 예외를 ErrorType으로 변환 |
| P1 | JsonDataSource | 저장 후 재로드, 기존 파일 보호 |
| P2 | 통합 흐름 | Task 완료 및 게임 종료 후 재로드 |

## [Good 예시 코드]

```csharp
public sealed class FakeGameRepository : IGameRepository
{
    public Result<GameState> LoadResult { get; set; }
    public Result SaveResult { get; set; } = Result.Success();
    public int SaveCallCount { get; private set; }
    public GameState? LastSavedState { get; private set; }

    public Result<GameState> Load(string playerId)
        => LoadResult;

    public Result Save(GameState state)
    {
        SaveCallCount++;
        LastSavedState = state;
        return SaveResult;
    }
}
```

Service 실패 경로 테스트:

```csharp
[Test]
public void CompleteTask_WhenKeysAreInsufficient_DoesNotSave()
{
    // Arrange
    GameState state = GameStateFixture.TaskCompletableState(goldenKeys: 0);
    var repository = new FakeGameRepository
    {
        LoadResult = Result<GameState>.Success(state)
    };
    var service = new GameService(repository);

    // Act
    Result<GameState> result = service.CompleteTask("player-1", taskId: 10);

    // Assert
    Assert.That(result.IsSuccess, Is.False);
    Assert.That(result.Error, Is.EqualTo(ErrorType.InsufficientKeys));
    Assert.That(repository.SaveCallCount, Is.EqualTo(0));
}
```

Service 성공 경로 테스트:

```csharp
[Test]
public void CompleteTask_WhenValid_SavesExactlyOnce()
{
    GameState state = GameStateFixture.TaskCompletableState(goldenKeys: 5);
    var repository = new FakeGameRepository
    {
        LoadResult = Result<GameState>.Success(state)
    };
    var service = new GameService(repository);

    Result<GameState> result = service.CompleteTask("player-1", taskId: 10);

    Assert.That(result.IsSuccess, Is.True);
    Assert.That(repository.SaveCallCount, Is.EqualTo(1));
    Assert.That(repository.LastSavedState, Is.SameAs(result.Value));
}
```

Domain 실패 후 상태 보존 테스트:

```csharp
[Test]
public void ReduceGoldenKeys_WhenInsufficient_PreservesState()
{
    var player = new Player(goldenKey: 2);

    Result result = player.ReduceGoldenKeys(3);

    Assert.That(result.IsSuccess, Is.False);
    Assert.That(result.Error, Is.EqualTo(ErrorType.InsufficientKeys));
    Assert.That(player.GoldenKey, Is.EqualTo(2));
}
```

## [Bad 예시 코드 — 과거 설계에서 피해야 할 실수]

```csharp
[Test]
public void CompleteTask_Test()
{
    var service = new GameService(
        new LocalGameRepository(
            new JsonGameDataSource("C:/real-save/save.json")));

    service.CompleteTask("player", 1);

    Assert.Pass();
}
```

문제점:

- 실제 사용자 파일 경로에 의존한다.
- 결과와 상태를 검증하지 않는다.
- 실패 원인을 찾을 수 없고 테스트 간 상태가 공유될 수 있다.

```csharp
[Test]
public void CompleteTask_ReturnsSuccess()
{
    Result<GameState> result = service.CompleteTask("player", 1);
    Assert.That(result.IsSuccess, Is.True);
}
```

문제점:

- 열쇠, Task, Entity, Zone의 실제 변경을 확인하지 않는다.
- Repository가 몇 번 저장됐는지 확인하지 않는다.
- 중복 보상과 부분 저장 결함을 발견할 수 없다.

## [권장 폴더 구조]

```text
Assets/Tests/EditMode/
├─ Domain/
│  ├─ PlayerTests.cs
│  ├─ TaskTests.cs
│  ├─ ZoneTests.cs
│  └─ GameSessionTests.cs
├─ Application/
│  └─ GameServiceTests.cs
├─ Infrastructure/
│  ├─ GameSaveMapperTests.cs
│  ├─ LocalGameRepositoryTests.cs
│  └─ JsonGameDataSourceTests.cs
└─ TestDoubles/
   ├─ FakeGameRepository.cs
   └─ GameStateFixture.cs
```

## [검토 체크리스트]

- [ ] 정상 경로와 실패 경로가 모두 있는가?
- [ ] 실패 시 Domain 상태가 유지되는가?
- [ ] Service 실패 시 `SaveCallCount`가 0인가?
- [ ] Service 성공 시 저장이 정확히 한 번 호출되는가?
- [ ] 실제 파일은 DataSource 테스트에서만 사용하는가?
- [ ] 각 테스트가 다른 테스트와 독립적인가?
- [ ] 테스트 이름이 조건과 기대 결과를 설명하는가?

