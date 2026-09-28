# DataSource 가이드라인

[규칙]
- DataSource는 외부 데이터(API/DB/로컬 파일)와의 통신만 담당한다.
- DataSource는 항상 Dto 형태로 원본(raw) 데이터를 반환해야 하며, 도메인 모델로 변환하지 않는다.
- DataSource는 비즈니스 판정(유효성 검사, 조건 분기, 계산)을 포함하지 않는다.
- DataSource는 Repository에서만 호출된다. Service나 UI가 DataSource를 직접 호출하지 않는다.
- DataSource는 `Result<T, E>`를 사용하지 않는다. 실패는 인프라 예외(네트워크 오류, 파일 없음 등)로 그대로 던진다.
- 반환 타입은 `Task<TDto>` (비동기)로 통일한다.

[Good 예시 코드]
```csharp
// PlayerDataSource.cs
public interface IPlayerDataSource
{
    Task<PlayerDto> GetPlayerAsync(string playerId);
    Task SavePlayerAsync(PlayerDto dto);
}

public class RemotePlayerDataSource : IPlayerDataSource
{
    private readonly HttpClient _httpClient;

    public async Task<PlayerDto> GetPlayerAsync(string playerId)
    {
        var response = await _httpClient.GetAsync($"/api/players/{playerId}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonUtility.FromJson<PlayerDto>(json); // 원본 Dto만 반환
    }

    public async Task SavePlayerAsync(PlayerDto dto)
    {
        var json = JsonUtility.ToJson(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"/api/players/{dto.playerId}", content);
        response.EnsureSuccessStatusCode();
    }
}
```
DataSource는 통신 결과를 그대로 Dto로 전달할 뿐, `coins < 0`인지 검증하거나 다른 데이터와 조합하는 일을 하지 않는다.

[Bad 예시 코드] (본인이 실수한 경험 남기기)
```csharp
// 실수했던 코드: DataSource가 도메인 모델을 반환하고, 검증까지 수행함
public class RemotePlayerDataSource
{
    public async Task<Player> GetPlayerAsync(string playerId) // ❌ 도메인 모델 반환
    {
        var response = await _httpClient.GetAsync($"/api/players/{playerId}");
        var json = await response.Content.ReadAsStringAsync();
        var dto = JsonUtility.FromJson<PlayerDto>(json);

        // ❌ DataSource 안에서 비즈니스 판정 수행
        if (dto.coins < 0)
        {
            dto.coins = 0;
        }

        // ❌ DataSource가 직접 도메인 모델로 변환 (Mapper의 책임 침범)
        return new Player
        {
            playerId = dto.playerId,
            coins = dto.coins,
            nickname = string.IsNullOrEmpty(dto.nickname) ? "Guest" : dto.nickname // ❌ 기본값 판정 로직
        };
    }
}
```
**문제점**: 나중에 "게스트 기본 닉네임" 규칙이 바뀌자 DataSource, Repository, Service 세 곳에 비슷한 검증 코드가 중복되어 있었고, 어디서 최종 검증이 이루어지는지 추적하는 데 오래 걸렸다. 이후 DataSource는 절대 Dto 이외의 타입을 반환하지 않도록 강제했다.