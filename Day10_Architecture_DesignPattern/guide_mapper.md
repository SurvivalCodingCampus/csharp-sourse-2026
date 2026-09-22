# Mapper 설계 가이드

> 대상 환경: Unity / C# 모바일 게임  
> 목적: DTO와 Domain Model 사이의 변환 및 외부 데이터 검증을 한곳에 모은다.

## [규칙]

- Mapper는 DTO와 Domain Model의 변환만 담당한다.
- DTO에서 Domain으로 변환할 때 외부 데이터의 유효성을 검증한다.
- DTO → Domain 변환은 실패할 수 있으므로 `Result<T>`를 반환한다.
- 유효한 Domain → DTO 변환은 일반적으로 직접 DTO를 반환한다.
- 파일, API, DB를 직접 조회하지 않는다.
- Service나 Repository를 직접 호출하지 않는다.
- 알 수 없는 enum 문자열을 임의의 기본 enum으로 바꾸지 않는다.
- 누락 필드나 손상 값을 조용히 보정하지 않고 `INVALID_DATA`로 반환한다.
- 양방향 변환에서 저장 대상 필드가 손실되지 않아야 한다.

## [Good 예시 코드]

```csharp
public static class EntityMapper
{
    public static Result<Entity> ToDomain(EntityDto dto)
    {
        if (dto.EntityId <= 0 || dto.ZoneId <= 0)
            return Result<Entity>.Failure(ErrorType.InvalidData);

        if (dto.LevelOfBuilding < 0)
            return Result<Entity>.Failure(ErrorType.InvalidData);

        if (!Enum.TryParse(dto.EntityType, ignoreCase: true, out EntityType type))
            return Result<Entity>.Failure(ErrorType.InvalidData);

        var entity = new Entity(
            dto.EntityId,
            dto.ZoneId,
            type,
            dto.LevelOfBuilding);

        return Result<Entity>.Success(entity);
    }

    public static EntityDto ToDto(Entity entity)
    {
        return new EntityDto
        {
            EntityId = entity.EntityId,
            ZoneId = entity.ZoneId,
            EntityType = entity.EntityType.ToString(),
            LevelOfBuilding = entity.LevelOfBuilding
        };
    }
}
```

좋은 이유:

- 외부 데이터의 잘못된 ID, 레벨, enum을 명시적으로 거부한다.
- I/O나 게임 진행 로직이 없다.
- 변환 실패 이유를 Repository에 전달한다.

## [Bad 예시 코드 — 과거 설계에서 피해야 할 실수]

```csharp
public static Entity ToDomain(EntityDto dto)
{
    EntityType type = Enum.TryParse(dto.EntityType, out EntityType parsed)
        ? parsed
        : EntityType.Lamp;

    int level = Math.Max(0, dto.LevelOfBuilding);
    return new Entity(dto.EntityId, dto.ZoneId, type, level);
}
```

문제점:

- 알 수 없는 EntityType을 `Lamp`로 바꾸어 데이터 손상을 숨긴다.
- 음수 레벨을 0으로 수정하여 오류 원인을 찾기 어렵게 만든다.
- 실패를 표현할 수 없다.

```csharp
public static GameState ToDomain(string savePath)
{
    string json = File.ReadAllText(savePath);
    GameSaveDto dto = JsonSerializer.Deserialize<GameSaveDto>(json);
    return Convert(dto);
}
```

문제점:

- Mapper가 DataSource의 파일 읽기 책임까지 수행한다.
- 변환 테스트를 위해 실제 파일이 필요해진다.

## [필수 검증 항목]

- ID가 유효한 범위인지 확인한다.
- 코인과 열쇠가 음수가 아닌지 확인한다.
- 현재 진행도가 최대 진행도를 넘지 않는지 확인한다.
- Task가 참조하는 Entity와 Zone이 존재하는지 확인한다.
- Session이 참조하는 Level이 존재하는지 확인한다.
- enum 문자열이 정의된 값인지 확인한다.
- `saveVersion`이 지원 범위인지 확인한다.

## [검토 체크리스트]

- [ ] Mapper가 I/O를 수행하지 않는가?
- [ ] DTO → Domain 실패가 `Result<T>`로 표현되는가?
- [ ] 잘못된 enum을 기본값으로 대체하지 않는가?
- [ ] 손상된 수치를 조용히 보정하지 않는가?
- [ ] Domain → DTO → Domain 왕복 시 값이 보존되는가?
- [ ] 모든 저장 대상 필드가 매핑되는가?

