# DataSource 설계 가이드

> 대상 환경: Unity / C# 모바일 게임  
> 목적: 파일, API, DB 같은 외부 저장 기술을 도메인과 분리한다.

## [규칙]

- DataSource는 외부 데이터와의 실제 통신만 담당한다.
- 입력과 출력은 Domain Model이 아니라 DTO를 사용한다.
- 게임 규칙, 재화 판정, 진행도 계산을 포함하지 않는다.
- DTO를 Domain Model로 변환하지 않는다. 변환은 Mapper의 책임이다.
- 파일 없음, 역직렬화 실패, 네트워크 실패 같은 기술 오류를 숨기지 않는다.
- DataSource는 기술 예외를 발생시키고, Repository가 이를 `Result`로 변환한다.
- 저장은 가능하면 `임시 파일 작성 → 검증 → 실제 파일 교체` 순서로 수행한다.
- DataSource 구현체는 Repository 또는 Composition Root를 직접 생성하지 않는다.
- 동기와 비동기 API를 한 프로젝트 안에서 임의로 섞지 않는다.

## [Good 예시 코드]

```csharp
public interface IGameDataSource
{
    GameSaveDto Load(string playerId);
    void Save(GameSaveDto dto);
}

public sealed class JsonGameDataSource : IGameDataSource
{
    private readonly string _savePath;

    public JsonGameDataSource(string savePath)
    {
        _savePath = savePath;
    }

    public GameSaveDto Load(string playerId)
    {
        if (!File.Exists(_savePath))
            throw new FileNotFoundException("Save file was not found.", _savePath);

        string json = File.ReadAllText(_savePath);
        return JsonSerializer.Deserialize<GameSaveDto>(json)
               ?? throw new InvalidDataException("Save data is empty.");
    }

    public void Save(GameSaveDto dto)
    {
        string tempPath = _savePath + ".tmp";
        string json = JsonSerializer.Serialize(dto);

        File.WriteAllText(tempPath, json);
        File.Move(tempPath, _savePath, overwrite: true);
    }
}
```

좋은 이유:

- 파일 I/O와 직렬화만 수행한다.
- Domain Model이나 게임 규칙을 알지 못한다.
- 기술 오류를 Repository가 분류할 수 있도록 숨기지 않는다.

## [Bad 예시 코드 — 과거 설계에서 피해야 할 실수]

```csharp
public GameState LoadGame(string playerId)
{
    GameSaveDto dto = ReadJson(playerId);
    GameState state = ConvertToDomain(dto);

    // DataSource가 게임 규칙까지 판단하고 있다.
    if (state.Player.GoldenKey < 0)
        state.Player.AddGoldenKeys(10);

    return state;
}
```

문제점:

- DTO 변환, 상태 보정, 게임 규칙이 DataSource에 섞여 있다.
- 저장 방식을 JSON에서 API로 변경하면 게임 규칙까지 다시 작성해야 한다.
- 손상된 데이터를 조용히 수정하여 오류 원인을 감춘다.

```csharp
public GameSaveDto? Load(string playerId)
{
    try
    {
        return ReadJson(playerId);
    }
    catch
    {
        return null;
    }
}
```

문제점:

- 파일 없음, JSON 손상, 권한 오류를 모두 `null`로 처리한다.
- Repository가 정확한 `ErrorType`으로 변환할 수 없다.

## [검토 체크리스트]

- [ ] 반환 타입이 DTO인가?
- [ ] Domain Model을 참조하지 않는가?
- [ ] 코인, 열쇠, 승패, Task 완료 판정이 없는가?
- [ ] Mapper를 직접 호출하지 않는가?
- [ ] 기술 오류를 `null`이나 기본값으로 숨기지 않는가?
- [ ] 저장 실패 시 기존 파일을 보호하는가?
- [ ] 실제 경로와 구현체 생성이 Composition Root에서 주입되는가?

