# DTO 설계 가이드

> 대상 환경: Unity / C# 모바일 게임  
> 목적: 저장소의 데이터 표현과 Domain Model을 분리한다.

## [규칙]

- DTO는 저장·전송에 필요한 데이터만 가진다.
- DTO에는 비즈니스 메서드와 상태 판정 로직을 넣지 않는다.
- Domain Model을 상속하거나 Domain Model을 내부에 직접 보관하지 않는다.
- 직렬화 가능한 기본 타입, 문자열, DTO, DTO 목록을 사용한다.
- enum 저장 형식은 프로젝트 전체에서 문자열 또는 정수 중 하나로 통일한다.
- 현재 설계에서는 손상 값을 검출하기 쉽도록 enum을 문자열로 저장한다.
- 저장 데이터 최상위 DTO에는 `saveVersion`을 둔다.
- Domain 복원에 필요한 필드를 누락하지 않는다.
- DTO 기본값으로 손상 데이터를 조용히 정상화하지 않는다.
- DTO의 필드명 변경은 저장 호환성에 영향을 주므로 마이그레이션을 고려한다.

## [Good 예시 코드]

```csharp
[Serializable]
public sealed class GameSaveDto
{
    public int SaveVersion { get; set; }
    public PlayerDto Player { get; set; } = new();
    public List<TaskDto> Tasks { get; set; } = new();
    public List<EntityDto> Entities { get; set; } = new();
}

[Serializable]
public sealed class PlayerDto
{
    public string PlayerId { get; set; } = string.Empty;
    public string PlayerName { get; set; } = string.Empty;
    public int Coin { get; set; }
    public int GoldenKey { get; set; }
    public int CurrentZoneId { get; set; }
    public bool SoundEnabled { get; set; }
    public bool MusicEnabled { get; set; }
}

[Serializable]
public sealed class TaskDto
{
    public int TaskId { get; set; }
    public int ZoneId { get; set; }
    public int TargetEntityId { get; set; }
    public int CurrentProgress { get; set; }
    public int TotalRequired { get; set; }
    public bool IsCompleted { get; set; }
    public int RequiredKeys { get; set; }
}
```

좋은 이유:

- 저장 데이터의 구조만 표현한다.
- Model의 메서드나 불변조건을 포함하지 않는다.
- 참조 관계를 ID로 표현하여 직렬화가 단순하다.

## [Bad 예시 코드 — 과거 설계에서 피해야 할 실수]

```csharp
public sealed class PlayerDto
{
    public int Coin { get; set; }
    public int GoldenKey { get; set; }

    public bool ReduceGoldenKeys(int amount)
    {
        if (GoldenKey < amount)
            return false;

        GoldenKey -= amount;
        return true;
    }
}
```

문제점:

- DTO가 Domain Model의 행동을 대신하고 있다.
- 저장용 객체를 UI나 Service가 직접 조작할 위험이 생긴다.

```csharp
public sealed class PlayerDto
{
    public string PlayerId { get; set; }
    public int Coin { get; set; }
    // GoldenKey, CurrentZoneId, 설정값이 누락됨
}
```

문제점:

- 저장 후 다시 불러오면 Domain 상태 일부가 사라진다.
- Mapper가 임의의 기본값을 넣어야 하므로 오류가 숨겨진다.

## [검토 체크리스트]

- [ ] DTO가 데이터 필드만 가지는가?
- [ ] 게임 규칙이나 상태 변경 메서드가 없는가?
- [ ] Domain 복원에 필요한 값이 모두 존재하는가?
- [ ] 관계가 직렬화하기 쉬운 ID로 표현되는가?
- [ ] enum 저장 형식이 통일되어 있는가?
- [ ] 최상위 저장 DTO에 버전 정보가 있는가?
- [ ] 필드 누락을 임의의 기본값으로 숨기지 않는가?

