# DataSource 구현 가이드

## [규칙]
- DataSource는 외부 데이터(REST API, 로컬 SQLite, SharedPreferences/PlayerPrefs, 원격 DB 등)와의 통신/I/O만 전담한다.
- 비즈니스 가공이나 도메인 모델 변환을 절대 수행하지 않으며, 직렬화/역직렬화된 순수 **DTO(Data Transfer Object)** 형태의 날것 데이터를 반환한다.
- 모든 I/O 작업은 비동기(`Task<T>`)로 처리하며, 실패 시 네트워크/DB 레벨 예외(Exception)를 그대로 던지거나 I/O 응답 DTO를 반환한다.

---

## [Good 예시 코드]
```csharp
// DTO 정의: 외부 API 명세와 1:1 매핑
public record UserProfileDto(string Id, string Nickname, string AvatarUrl, int SoundVol, int MusicVol);

public interface IUserRemoteDataSource
{
    Task<UserProfileDto> FetchUserProfileAsync(string userId);
}

public class UserRemoteDataSource : IUserRemoteDataSource
{
    private readonly HttpClient _httpClient;

    public UserRemoteDataSource(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<UserProfileDto> FetchUserProfileAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"/api/v1/users/{userId}");
        response.EnsureSuccessStatusCode();
        
        var dto = await response.Content.ReadFromJsonAsync<UserProfileDto>();
        return dto!;
    }
}
```

## [BAD 예시 코드]
```csharp
// 실수 사례: DataSource에서 Entity로 직접 변환하고 비즈니스 판정(기본값 보정)을 수행함
public class UserRemoteDataSource
{
    public async Task<UserProfile> GetUserAsync(string userId) // DTO가 아닌 Domain Model 반환
    {
        var json = await _client.GetStringAsync($"/api/v1/users/{userId}");
        var rawData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

        // ❌ 금지: 볼륨 기본값 보정 등 비즈니스 규칙이 DataSource 계층에 침투함
        float sound = rawData.ContainsKey("sound") ? Convert.ToSingle(rawData["sound"]) : 1.0f;

        // ❌ 금지: 도메인 객체 인스턴스화
        return new UserProfile(userId, rawData["name"].ToString(), sound);
    }
}
```