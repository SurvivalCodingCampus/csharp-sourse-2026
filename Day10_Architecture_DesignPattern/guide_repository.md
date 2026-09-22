# Repository 설계 가이드

> 대상 환경: Unity / C# 모바일 게임  
> 목적: Application 계층에 저장 계약을 제공하고 저장 기술의 차이를 숨긴다.

## [규칙]

- Repository 인터페이스는 이를 사용하는 Application 계층에 둔다.
- Repository 인터페이스는 Domain Model과 `Result`를 사용한다.
- Repository 구현체는 DataSource와 Mapper를 조합한다.
- Repository는 DTO를 Domain Model로 변환하고, Domain Model을 DTO로 변환하여 저장한다.
- 파일·네트워크·역직렬화 예외를 `ErrorType`으로 변환한다.
- 열쇠 부족, Task 완료 가능 여부, 승패 같은 비즈니스 판정을 하지 않는다.
- Service가 구체 Repository 구현체를 직접 생성하지 않게 한다.
- `Load`할 때 변경 가능한 캐시 객체를 그대로 공유하지 않는다.
- 소규모 로컬 게임에서는 전체 `GameState`를 하나의 저장 단위로 유지한다.

## [Good 예시 코드]

```csharp
// Application 계층
public interface IGameRepository
{
    Result<GameState> Load(string playerId);
    Result Save(GameState state);
}

// Infrastructure 계층
public sealed class LocalGameRepository : IGameRepository
{
    private readonly IGameDataSource _dataSource;

    public LocalGameRepository(IGameDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public Result<GameState> Load(string playerId)
    {
        try
        {
            GameSaveDto dto = _dataSource.Load(playerId);
            return GameSaveMapper.ToDomain(dto);
        }
        catch (FileNotFoundException)
        {
            return Result<GameState>.Failure(ErrorType.NotFound);
        }
        catch (IOException)
        {
            return Result<GameState>.Failure(ErrorType.IoError);
        }
    }

    public Result Save(GameState state)
    {
        try
        {
            GameSaveDto dto = GameSaveMapper.ToDto(state);
            _dataSource.Save(dto);
            return Result.Success();
        }
        catch (IOException)
        {
            return Result.Failure(ErrorType.SaveFailed);
        }
    }
}
```

좋은 이유:

- Application은 `IGameRepository`만 알고 저장 기술은 모른다.
- 구현체는 변환과 오류 매핑에 집중한다.
- 게임 규칙은 Domain Model과 Service에 남는다.

## [Bad 예시 코드 — 과거 설계에서 피해야 할 실수]

```csharp
public Result<TaskModel> CompleteTask(string playerId, int taskId)
{
    Player player = LoadPlayer(playerId);
    TaskModel task = LoadTask(taskId);

    if (player.GoldenKey < task.RequiredKeys)
        return Result<TaskModel>.Failure(ErrorType.InsufficientKeys);

    player.ReduceGoldenKeys(task.RequiredKeys);
    task.Complete();
    SavePlayer(player);
    SaveTask(task);

    return Result<TaskModel>.Success(task);
}
```

문제점:

- Repository가 비즈니스 유스케이스를 수행한다.
- Player 저장 성공 후 Task 저장이 실패하면 부분 저장이 발생한다.
- 같은 규칙을 UI나 다른 Repository에서 중복 구현할 가능성이 커진다.

```csharp
public sealed class GameService
{
    private readonly IGameRepository _repository =
        new LocalGameRepository(new JsonGameDataSource("save.json"));
}
```

문제점:

- Service가 구체 구현체를 직접 생성하여 DIP를 위반한다.
- 저장 기술을 교체하려면 Service까지 수정해야 한다.

## [구현체 조립 규칙]

구현체는 Composition Root에서만 연결한다.

```csharp
IGameDataSource dataSource = new JsonGameDataSource(savePath);
IGameRepository repository = new LocalGameRepository(dataSource);
GameService service = new GameService(repository);
```

## [검토 체크리스트]

- [ ] Repository 인터페이스가 Application 계층에 있는가?
- [ ] Service가 인터페이스에만 의존하는가?
- [ ] 구현체가 DataSource와 Mapper만 조합하는가?
- [ ] 게임 규칙이나 유스케이스가 Repository에 없는가?
- [ ] 기술 예외가 적절한 `ErrorType`으로 변환되는가?
- [ ] 여러 객체를 따로 저장하여 부분 저장이 발생하지 않는가?
- [ ] `Load`가 외부와 공유된 변경 가능 캐시를 반환하지 않는가?

