# Repository 가이드

## 적용 범위

도메인 모델 단위의 조회·저장 계약. Island 게임의 `IPlayerRepository`, `ITaskRepository` 등에 적용한다.

## 규칙

- Repository 인터페이스를 Service에 주입한다. 구현은 DataSource와 Mapper에 의존한다.
- Repository는 DTO를 밖으로 노출하지 않는다. DataSource 응답을 해석해 도메인 모델 또는 `null`/목록을 반환한다.
- 조회 결과 없음과 실패를 구분한다. 예를 들어 단일 조회의 `404`는 `null`, `200`인데 필수 본문이 비었으면 데이터 형식 오류다.
- 과제 3 보강안에서는 Repository가 `Task<T>`/`Task<T?>`를 반환하고, Service가 최종 `Result<T, GameError>`를 만든다. 한 기능에서 서로 다른 정책을 섞지 않는다.
- 레벨 해금, 보상, 자원 부족, 작업 완료 같은 게임 규칙은 Service에 둔다. 여러 저장 작업의 원자성은 실제 저장 기술을 정한 뒤 설계한다.

## Good 예시 코드

```csharp
public async Task<PlayerProfile?> GetByIdAsync(
    Guid playerId, CancellationToken cancellationToken)
{
    var response = await _dataSource.GetProfileAsync(
        playerId, cancellationToken);

    if (response.StatusCode == 404)
        return null;

    if (response.StatusCode != 200)
        throw new HttpRequestException(
            $"Profile request failed: {response.StatusCode}");

    if (response.Body is null)
        throw new InvalidDataException("Profile response is invalid.");

    return response.Body.ToModel();
}
```

실제 구현에서는 `CancellationToken`을 인터페이스부터 일관되게 전달한다. 예시는 데이터 경계만 보여준다.

## Bad 예시 코드

```csharp
public async Task<bool> UnlockLevelAsync(Guid playerId, int levelId)
{
    var profile = await GetByIdAsync(playerId);
    if (profile is null || profile.Stars < 20)
        return false;

    await _dataSource.SaveUnlockedLevelAsync(playerId, levelId);
    return true;
}
```

별 요구량과 해금 판정이 Repository에 들어가면 저장 방식 변경과 게임 규칙 변경이 한 클래스에 모인다. `20`도 `.fig`에서 확인되지 않은 임의 규칙이다.

## 기존 코드에서 확인한 교훈

`Day06_Model_Repository/Data/Repositories/InventoryRepository.cs`의 `AddItemAsync`는 슬롯·스택 상한을 직접 판정한다. 과제 2·3에서는 이러한 비즈니스 판정을 Service로 옮기고 Repository는 조회·저장으로 좁히는 방향을 택했다.
