---

### `docs/guide_test.md`

```markdown
# 테스트(Test) 작성 가이드

## [규칙]
- Service 계층의 비즈니스 로직 단위 테스트는 Mock Repository를 주입하여 실제 DB/네트워크 없이 독립적으로 검증한다.
- 테스트 메서드 명칭은 `[메서드명]_[시나리오]_[기대결과]` 컨벤션을 준수한다.
- `Result.IsSuccess` 및 반환된 에러 코드(`Result.Error`)를 명확하게 단언(Assert)한다.

---

## [Good 예시 코드]
```csharp
[TestClass]
public class RestorationServiceTests
{
    private Mock<IRestorationTaskRepository> _mockTaskRepo = null!;
    private Mock<IWalletRepository> _mockWalletRepo = null!;
    private RestorationService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockTaskRepo = new Mock<IRestorationTaskRepository>();
        _mockWalletRepo = new Mock<IWalletRepository>();
        _service = new RestorationService(_mockTaskRepo.Object, _mockWalletRepo.Object);
    }

    [TestMethod]
    public async Task ExecuteTaskAsync_InsufficientKeys_ReturnsInsufficientKeysError()
    {
        // Arrange
        const string userId = "user1";
        const string taskId = "task_lamp";
        var task = new RestorationTask(taskId, zoneId: 1, requiredKeys: 3, status: TaskStatus.PENDING);
        var wallet = new UserWallet(userId, coins: 100, keys: 1); // 열쇠 1개 보유 (3개 필요)

        _mockTaskRepo.Setup(r => r.GetTaskAsync(taskId)).ReturnsAsync(task);
        _mockWalletRepo.Setup(r => r.GetWalletAsync(userId)).ReturnsAsync(wallet);

        // Act
        var result = await _service.ExecuteTaskAsync(userId, taskId);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(GameErrorCode.INSUFFICIENT_KEYS, result.Error);
        _mockWalletRepo.Verify(r => r.SaveWalletAsync(It.IsAny<UserWallet>()), Times.Never);
    }
}
```

## [BAD 예시 코드]
```csharp
[TestClass]
public class RestorationServiceTests
{
    [TestMethod]
    public async Task TestRestoration()
    {
        // ❌ 금지: 실제 네트워크/로컬 DB 파일에 직접 접근하는 통합 환경 의존
        var realRepo = new LocalFileRestorationTaskRepository("C:/save.json");
        var service = new RestorationService(realRepo, ...);

        var result = await service.ExecuteTaskAsync("user1", "task1");

        // ❌ 금지: Result의 상태나 에러 코드를 검증하지 않고 뭉뚱그려 확인
        Assert.IsNotNull(result);
    }
}
```