# Result 패턴 가이드라인

[규칙]
- 비즈니스 판정이 포함된 모든 Service public 메서드는 `Task<Result<T, ServiceError>>`를 반환한다.
- `Result<T, E>`는 `isSuccess`, `value`, `error`를 갖고, `Result.Ok(value)` / `Result.Fail(error)` 정적 팩토리로만 생성한다. 생성자를 직접 노출하지 않는다.
- 실패 사유는 예외(exception)가 아니라 `ServiceError` enum 값으로 표현한다. 예외는 프로그래밍 오류(null 인자 등)나 인프라 오류(네트워크 끊김)에만 사용한다.
- Repository/DataSource는 `Result`를 사용하지 않는다 — 판정을 하지 않는 계층이므로 Result로 감쌀 실패 사유 자체가 없다.
- 부수효과 없이 항상 성공하는 순수 계산 함수(예: 별점 계산)는 `Result`로 감싸지 않고 값을 직접 반환해도 된다. 단, 이 경우 왜 예외로 두었는지 주석으로 남긴다.
- 호출부(UI)는 `result.isSuccess` 분기 없이 `result.value`에 접근하지 않는다.

[Good 예시 코드]
```csharp
// Result.cs
public class Result<T, E>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public E Error { get; }

    private Result(bool isSuccess, T value, E error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T, E> Ok(T value) => new Result<T, E>(true, value, default);
    public static Result<T, E> Fail(E error) => new Result<T, E>(false, default, error);
}

// PlayerService.cs
public async Task<Result<Player, ServiceError>> SpendCurrency(string playerId, int coins, int gems)
{
    var player = await _playerRepo.GetPlayerAsync(playerId);
    if (player == null)
        return Result<Player, ServiceError>.Fail(ServiceError.PLAYER_NOT_FOUND);

    if (player.coins < coins || player.gems < gems)
        return Result<Player, ServiceError>.Fail(ServiceError.INSUFFICIENT_CURRENCY);

    player.coins -= coins;
    player.gems -= gems;
    await _playerRepo.SavePlayerAsync(player);
    return Result<Player, ServiceError>.Ok(player);
}

// 호출부(UI)
var result = await playerService.SpendCurrency("p1", 100, 0);
if (result.IsSuccess)
{
    UpdateCoinUI(result.Value.coins);
}
else
{
    ShowError(result.Error); // ServiceError별로 메시지 분기 가능
}
```

[Bad 예시 코드] (본인이 실수한 경험 남기기)
```csharp
// 실수했던 코드: 실패를 null과 예외로 뒤섞어 표현
public async Task<Player> SpendCurrency(string playerId, int coins, int gems)
{
    var player = await _playerRepo.GetPlayerAsync(playerId);
    if (player == null)
        return null; // ❌ 실패 이유가 "플레이어 없음"인지 알 수 없음

    if (player.coins < coins)
        throw new InvalidOperationException("코인이 부족합니다."); // ❌ 정상적인 비즈니스 실패를 예외로 처리

    player.coins -= coins;
    await _playerRepo.SavePlayerAsync(player);
    return player;
}

// 호출부
try
{
    var player = await playerService.SpendCurrency("p1", 100, 0);
    if (player == null) // ❌ null 체크와 try-catch가 뒤섞임
    {
        ShowError("알 수 없는 오류");
    }
    UpdateCoinUI(player.coins); // ❌ null 체크를 깜빡하면 NullReferenceException
}
catch (InvalidOperationException ex)
{
    ShowError(ex.Message); // ❌ 문자열 비교로 에러 종류를 구분해야 함
}
```
**문제점**: "코인 부족"은 예외로, "플레이어 없음"은 null로 표현하다 보니 호출부마다 처리 방식이 제각각이었다. 어떤 화면에서는 `try-catch`를 빼먹어서 앱이 그대로 크래시됐고, 다른 화면에서는 `ex.Message` 문자열을 파싱해서 에러 종류를 구분하려다가 다국어 처리 시 깨졌다. 모든 실패를 `ServiceError` enum과 `Result`로 통일한 뒤로는, 호출부가 `switch(result.Error)`로 안전하게 분기할 수 있게 됐다.