# Result 패턴 가이드

> 대상 환경: Unity / C# 모바일 게임  
> 목적: 예상 가능한 실패를 예외나 모호한 Boolean 대신 명시적으로 전달한다.

## [규칙]

- 성공과 실패를 동시에 가질 수 없도록 생성자를 제한한다.
- 성공 Result에는 오류가 없어야 한다.
- 실패 Result에는 반드시 `ErrorType`이 있어야 한다.
- `Result<T>`가 성공하면 값이 존재해야 한다.
- 실패한 `Result<T>`의 값 접근을 허용하지 않는다.
- 외부에서는 `Success()`와 `Failure()` 팩토리 메서드만 사용한다.
- Service, Repository, Mapper, 실패 가능한 Domain 행동에 적용한다.
- DataSource의 기술 예외는 Repository가 Result로 변환한다.
- 정상적인 게임 실패는 Result로, 프로그래밍 오류는 예외 또는 사전조건 위반으로 구분한다.
- 호출자는 반환된 Result를 무시하지 않는다.

## [Good 예시 코드]

```csharp
public sealed class Result
{
    public bool IsSuccess { get; }
    public ErrorType? Error { get; }

    private Result(bool isSuccess, ErrorType? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
        => new(true, null);

    public static Result Failure(ErrorType error)
        => new(false, error);
}

public sealed class Result<T>
{
    private readonly T? _value;

    public bool IsSuccess { get; }
    public ErrorType? Error { get; }

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Failure result has no value.");

    private Result(bool isSuccess, T? value, ErrorType? error)
    {
        IsSuccess = isSuccess;
        _value = value;
        Error = error;
    }

    public static Result<T> Success(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new Result<T>(true, value, null);
    }

    public static Result<T> Failure(ErrorType error)
        => new(false, default, error);
}
```

호출자는 모든 결과를 확인한다.

```csharp
Result keyResult = player.ReduceGoldenKeys(task.RequiredKeys);

if (!keyResult.IsSuccess)
    return Result<GameState>.Failure(keyResult.Error!.Value);
```

## [Bad 예시 코드 — 과거 설계에서 피해야 할 실수]

```csharp
public sealed class Result<T>
{
    public bool IsSuccess { get; set; }
    public T Value { get; set; }
    public ErrorType Error { get; set; }
}
```

문제점:

- 외부에서 `IsSuccess = true`와 오류를 동시에 지정할 수 있다.
- 실패 상태인데도 Value를 사용할 수 있다.
- Result의 불변조건이 보호되지 않는다.

```csharp
entity.Upgrade();
zone.AdvanceProgress(1);
repository.Save(state);
```

문제점:

- Model의 Result와 Repository의 저장 결과를 무시한다.
- 중간 실패를 성공으로 처리할 수 있다.

```csharp
public bool ReduceGoldenKeys(int amount)
```

문제점:

- `false`만으로는 잘못된 입력과 열쇠 부족을 구분할 수 없다.

## [Result 적용 범위]

| 계층 | 반환 예시 |
| --- | --- |
| Domain | `Result ReduceGoldenKeys(int amount)` |
| Mapper | `Result<GameState> ToDomain(GameSaveDto dto)` |
| Repository | `Result<GameState> Load(string playerId)` |
| Repository | `Result Save(GameState state)` |
| Service | `Result<GameState> CompleteTask(...)` |
| DataSource | 기술 예외 발생, Repository에서 Result로 변환 |

## [검토 체크리스트]

- [ ] 성공과 실패 상태가 생성 단계에서 분리되는가?
- [ ] 실패 Result에 오류가 반드시 존재하는가?
- [ ] 실패 값 접근이 차단되는가?
- [ ] Boolean만으로 오류 원인을 뭉개지 않는가?
- [ ] 호출자가 모든 Result를 확인하는가?
- [ ] 같은 오류가 여러 계층에서 이중 포장되지 않는가?

