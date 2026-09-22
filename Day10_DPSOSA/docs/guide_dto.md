---

### `docs/guide_dto.md`

```markdown
# DTO (Data Transfer Object) 작성 가이드

## [규칙]
- DTO는 계층 간(특히 DataSource ↔ Repository) 데이터 전송만을 위한 단순한 데이터 컨테이너이다.
- 비즈니스 행위(메서드)나 도메인 규칙 판정 로직을 일절 포함하지 않는다.
- 불변성(Immutability) 유지를 위해 C#의 `record` 또는 `readonly struct` 사용을 지향한다.

---

## [Good 예시 코드]
```csharp
public record StageProgressDto(
    string UserId,
    int LevelNumber,
    string Status, // "LOCKED", "UNLOCKED", "COMPLETED"
    int BestScore,
    int Stars
);
```

## [BAD 예시 코드]
```csharp
public class StageProgressDto
{
public string UserId { get; set; }
public int LevelNumber { get; set; }
public int Stars { get; set; }

    // ❌ 금지: DTO 내부에 비즈니스 판정 메서드 포함
    public bool IsThreeStarClear()
    {
        return Stars >= 3;
    }
}
```