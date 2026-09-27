# Result 패턴 가이드

## 적용 범위

사용 사례의 성공과 예상 가능한 실패를 호출자에게 명시적으로 전달하는 Service 반환 계약.

## 규칙

- 과제 3 보강안의 Service는 `Task<Result<TData, GameError>>`를 반환한다. Repository는 도메인 데이터 조회·저장 계약을 유지한다.
- `NotFound`, `LevelLocked`, `InsufficientResource`, `TargetNotMet`, `DataUnavailable`처럼 호출자가 처리할 수 있는 실패를 구체적으로 구분한다.
- `null` 조회 결과, 빈 목록, 형식 오류, 네트워크 오류를 같은 성공/실패로 뭉치지 않는다. 빈 목록이 정상인지 오류인지는 사용 사례별로 결정한다.
- `OperationCanceledException`은 다시 던진다. 예상하지 못한 프로그래밍 결함까지 `DataUnavailable`로 숨기지 않는다.
- 성공 값에 임의의 기본 도메인 객체를 넣어 실패를 감추지 않는다.

## Good 예시 코드

```csharp
public async Task<Result<Level, GameError>>
    GetPlayableLevelAsync(Guid playerId, int levelId)
{
    try
    {
        var level = await _levelRepository.GetByIdAsync(levelId);
        if (level is null)
            return new Result<Level, GameError>.Error(GameError.NotFound);

        var progress = await _progressRepository.GetForPlayerAsync(playerId);
        if (!IsPlayable(level, progress))
            return new Result<Level, GameError>.Error(GameError.LevelLocked);

        return new Result<Level, GameError>.Success(level);
    }
    catch (OperationCanceledException)
    {
        throw;
    }
    catch (HttpRequestException)
    {
        return new Result<Level, GameError>.Error(
            GameError.DataUnavailable);
    }
    catch (InvalidDataException)
    {
        return new Result<Level, GameError>.Error(
            GameError.DataUnavailable);
    }
}
```

`IsPlayable`의 해금 기준은 실제 게임 규칙이 정해진 뒤 구현한다. 저장 기술이 정해지면 해당 기술의 예상 가능한 예외만 추가로 변환한다.

## Bad 예시 코드

```csharp
catch (Exception)
{
    return new Result<Level, GameError>.Error(
        GameError.DataUnavailable);
}
```

취소와 코드 버그까지 데이터 오류로 위장한다. 성공 상태인데 `null` 본문이면 기본 프로필을 만들어 `Success`로 반환하는 것도 피한다.

## 기존 코드에서 확인한 교훈

`Day09_Result_Pattern/TIL.md`에는 지하철 응답 목록이 비었는데 성공처럼 처리하던 방식을 `StationNotFound`로 바꾼 경험이 있다. 반면 `Day09_Result_Pattern/Data/Repositories/SubwayRepository.cs`의 마지막 `catch`는 모든 예외를 `Unknown`으로 바꾼다. Island 게임에서는 오류를 구분하고 취소를 보존한다.
