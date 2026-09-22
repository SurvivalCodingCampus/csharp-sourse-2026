# Repository 가이드라인

[규칙]
- Repository는 DataSource로부터 Dto를 받아 Mapper를 통해 도메인 모델로 변환한 뒤 반환한다.
- Repository는 `get`/`save`/`delete` 수준의 순수 영속성 책임만 가진다. 판정(조건 분기, 계산, 상태 결정)을 포함하지 않는다.
- Repository는 여러 DataSource를 조합할 수 있지만, 조합의 "의미"(비즈니스 규칙)는 Service가 결정한다. Repository는 단순 취합만 한다.
- Repository는 `Result<T, E>`를 사용하지 않는다. 실패는 인프라 예외로 그대로 전파한다.
- 반환 타입은 조회 시 `Task<TDomain>`, 저장/삭제 시 `Task`로 통일한다.

[Good 예시 코드]
```csharp
// IPlayerRepository.cs
public interface IPlayerRepository
{
    Task<Player> GetPlayerAsync(string playerId);
    Task SavePlayerAsync(Player player);
}

public class PlayerRepository : IPlayerRepository
{
    private readonly IPlayerDataSource _dataSource;

    public async Task<Player> GetPlayerAsync(string playerId)
    {
        var dto = await _dataSource.GetPlayerAsync(playerId);
        return PlayerMapper.ToDomain(dto); // 변환은 Mapper에 위임
    }

    public async Task SavePlayerAsync(Player player)
    {
        var dto = PlayerMapper.ToDto(player);
        await _dataSource.SavePlayerAsync(dto);
    }
}
```
Repository는 "무엇을 저장할지" 결정하지 않고, 이미 결정되어 전달된 `Player` 객체를 그대로 저장할 뿐이다.

[Bad 예시 코드] (본인이 실수한 경험 남기기)
```csharp
// 실수했던 코드: Repository가 판정 로직을 포함함
public class TaskRepository : ITaskRepository
{
    public async Task CompleteTaskAsync(string taskId) // ❌ 이름부터 판정 결과를 지시
    {
        var dto = await _dataSource.GetTaskAsync(taskId);

        // ❌ Repository 안에서 완료 조건을 직접 판단
        if (dto.currentProgress < dto.targetProgress)
        {
            throw new InvalidOperationException("아직 완료할 수 없습니다."); // ❌ 예외로 비즈니스 규칙 표현
        }

        dto.isCompleted = true;
        await _dataSource.SaveTaskAsync(dto);

        // ❌ Repository가 다른 Repository(PlayerRepository)를 직접 호출 (보상 지급)
        var playerDto = await _playerDataSource.GetPlayerAsync(dto.ownerId);
        playerDto.coins += dto.rewardAmount;
        await _playerDataSource.SavePlayerAsync(playerDto);
    }
}
```
**문제점**: "완료 가능 여부 판정"과 "보상 지급"이라는 두 가지 비즈니스 규칙이 Repository 안에 섞여 있었다. 이후 "이미 완료된 태스크는 재완료 불가"라는 규칙을 추가할 때, `TaskRepository`뿐 아니라 이 메서드를 호출하는 모든 곳의 예외 처리 코드를 함께 수정해야 했다. `TaskService.claimReward()`로 판정과 조합 로직을 옮기고, Repository는 `GetTaskAsync`/`SaveTaskAsync`만 남기도록 리팩터링했다.