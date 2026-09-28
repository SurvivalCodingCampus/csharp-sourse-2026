---

### `docs/guide_result_pattern.md`

```markdown
# Result 패턴 및 Service 가이드

## [규칙]
- Service 계층은 유효성 검사, 재화 차감, 목표 달성 여부 등 **핵심 비즈니스 판정 및 트랜잭션 오케스트레이션**을 전담한다.
- 예상 가능한 비즈니스 거절(예: 잔액 부족, 중복 완료, 조건 미달)에 대해 예외(`throw Exception`)를 던지지 않는다.
- 메서드의 최종 반환값은 반드시 **`Task<Result<T, TError>>`** 형태로 캡슐화하여 호출부에서 컴파일 타임에 안전하게 분기하도록 유도한다.

---

## [Good 예시 코드]
```csharp
public class Result<T, TE>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public TE Error { get; }

    private Result(T value) { IsSuccess = true; Value = value; Error = default!; }
    private Result(TE error) { IsSuccess = false; Value = default!; Error = error; }

    public static Result<T, TE> Success(T value) => new(value);
    public static Result<T, TE> Failure(TE error) => new(error);
}

public class RestorationService : IRestorationService
{
    private readonly IRestorationTaskRepository _taskRepo;
    private readonly IWalletRepository _walletRepo;

    public async Task<Result<RestorationTask, GameErrorCode>> ExecuteTaskAsync(string userId, string taskId)
    {
        var task = await _taskRepo.GetTaskAsync(taskId);
        var wallet = await _walletRepo.GetWalletAsync(userId);

        // 1. 비즈니스 판정
        if (task.Status == TaskStatus.COMPLETED)
            return Result<RestorationTask, GameErrorCode>.Failure(GameErrorCode.TASK_ALREADY_COMPLETED);

        if (!wallet.HasEnoughKeys(task.RequiredKeys))
            return Result<RestorationTask, GameErrorCode>.Failure(GameErrorCode.INSUFFICIENT_KEYS);

        // 2. 도메인 상태 변경
        wallet.DeductKeys(task.RequiredKeys);
        task.MarkAsCompleted();

        // 3. I/O 영속화 (Task 반환 대기)
        await _walletRepo.SaveWalletAsync(wallet);
        await _taskRepo.SaveTaskAsync(task);

        return Result<RestorationTask, GameErrorCode>.Success(task);
    }
}
```

## [BAD 예시 코드]
```csharp
// 실수 사례: 비즈니스 오류를 흐름 제어용 예외(Exception)로 던지거나 단순 bool 반환
public class RestorationService
{
    // ❌ 금지: 어떤 이유로 실패했는지 원인을 알 수 없음
    public async Task<bool> ExecuteTask(string userId, string taskId)
    {
        var task = await _taskRepo.GetTaskAsync(taskId);
        if (task.Status == TaskStatus.COMPLETED) return false;
        ...
    }

    // ❌ 금지: 일반적인 비즈니스 거절 상황에서 Exception throw
    public async Task ExecuteTaskWithException(string userId, string taskId)
    {
        var wallet = await _walletRepo.GetWalletAsync(userId);
        if (wallet.Keys < 1)
        {
            throw new InvalidOperationException("열쇠가 부족합니다."); // 성능 저하 및 크래시 유발
        }
    }
}
```