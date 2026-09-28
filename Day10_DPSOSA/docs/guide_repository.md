---

### `docs/guide_repository.md`

```markdown
# Repository 구현 가이드

## [규칙]
- DataSource를 호출해 DTO를 취득한 뒤, Mapper를 거쳐 **Domain Model(Entity)**로 변환하여 상위 계층에 전달한다.
- Repository의 메서드는 **오직 순수 `Task<T>` 또는 `Task`만 반환**해야 한다.
- 비즈니스 판정(예: 잔액 충분 여부 확인, 별점 클리어 여부 확인)을 절대 수행하지 않는다. 영속성 I/O 및 데이터 저장/조회에만 집중한다.

---

## [Good 예시 코드]
```csharp
public interface IWalletRepository
{
    Task<UserWallet> GetWalletAsync(string userId);
    Task SaveWalletAsync(UserWallet wallet);
}

public class WalletRepository : IWalletRepository
{
    private readonly IWalletRemoteDataSource _dataSource;

    public WalletRepository(IWalletRemoteDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<UserWallet> GetWalletAsync(string userId)
    {
        WalletDto dto = await _dataSource.FetchWalletAsync(userId);
        return dto.ToDomain(); // Mapper 사용
    }

    public async Task SaveWalletAsync(UserWallet wallet)
    {
        WalletDto dto = wallet.ToDto();
        await _dataSource.UpsertWalletAsync(dto);
    }
}
```

## [BAD 예시 코드]
```csharp
// 실수 사례: Repository에서 Result 패턴을 남용하거나 비즈니스 검증을 수행함
public class WalletRepository : IWalletRepository
{
    // ❌ 금지: Repository가 비즈니스 실패 결과(Result)를 직접 판단하여 반환
    public async Task<Result<UserWallet, GameErrorCode>> DeductCoinsAsync(string userId, int amount)
    {
        var wallet = await _dataSource.GetWalletAsync(userId);
        
        // ❌ 금지: 비즈니스 판정은 Service 계층의 책임임
        if (wallet.Coins < amount)
        {
            return Result.Failure(GameErrorCode.INSUFFICIENT_COINS);
        }

        wallet.Coins -= amount;
        await _dataSource.SaveAsync(wallet);
        return Result.Success(wallet.ToDomain());
    }
}
```