---

### `docs/guide_mapper.md`

```markdown
# Mapper 작성 가이드

## [규칙]
- Mapper는 DTO를 Domain Model(Entity)로 변환하거나, Domain Model을 DTO로 변환하는 변환 책임만을 가진다.
- 확장 메서드(Extension Method) 형태로 정의하여 가독성을 높인다.
- 잘못된 포맷이나 Nullable 데이터에 대한 안전한 파싱(Safe Parse)을 보장하며, 비즈니스 의사결정은 포함하지 않는다.

---

## [Good 예시 코드]
```csharp
public static class StageProgressMapper
{
    public static UserLevelProgress ToDomain(this StageProgressDto dto)
    {
        return new UserLevelProgress(
            userId: dto.UserId,
            levelNumber: dto.LevelNumber,
            status: Enum.TryParse<LevelStatus>(dto.Status, true, out var status) ? status : LevelStatus.LOCKED,
            bestScore: dto.BestScore,
            stars: dto.Stars
        );
    }

    public static StageProgressDto ToDto(this UserLevelProgress domain)
    {
        return new StageProgressDto(
            domain.UserId,
            domain.LevelNumber,
            domain.Status.ToString(),
            domain.BestScore,
            domain.Stars
        );
    }
}
```

## [BAD 예시 코드]
```csharp
// 실수 사례: Mapper 안에서 로직 판단 및 외부 서비스 호출
public static class StageProgressMapper
{
public static UserLevelProgress ToDomain(StageProgressDto dto, IWalletRepository walletRepo)
{
// ❌ 금지: Mapper가 다른 Repository를 참조하거나 비즈니스 판단 수행
if (dto.Stars == 0)
{
walletRepo.DeductPenalty();
}

        return new UserLevelProgress(dto.UserId, dto.LevelNumber, dto.Stars);
    }
}
```