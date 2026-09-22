# Dto 가이드라인

[규칙]
- Dto는 외부 API/DB의 데이터 구조를 그대로 표현한다. 필드명과 타입은 외부 스펙(JSON 응답 등)을 따르며, 도메인 모델의 필드명과 다를 수 있다.
- Dto는 순수 데이터 컨테이너이다. 계산, 검증, 판정 메서드를 갖지 않는다.
- Dto는 DataSource와 Mapper에서만 사용된다. Service, UI, 도메인 로직에서 Dto를 직접 참조하지 않는다.
- Dto ↔ 도메인 모델 간 변환은 오직 Mapper만 담당한다.
- 외부 API 스펙이 바뀌면 Dto만 수정하고, 도메인 모델과 Service는 Mapper 수정만으로 영향을 흡수해야 한다.

[Good 예시 코드]
```csharp
// PlayerDto.cs — 서버 API 응답 구조를 그대로 반영 (snake_case, nullable 허용)
[Serializable]
public class PlayerDto
{
    public string player_id;
    public string nick_name;      // 도메인 모델의 nickname과 이름이 다름
    public string country_flag;
    public string profile_image_url;
    public int coin_balance;      // 도메인 모델의 coins와 이름이 다름
    public int gem_balance;
    public int golden_key_count;
}
```
Dto는 서버가 내려주는 필드 구조를 그대로 따르며, 도메인 모델(`Player.coins`)과 필드명이 달라도 문제되지 않는다 — 그 차이를 흡수하는 것이 Mapper의 역할이다.

[Bad 예시 코드] (본인이 실수한 경험 남기기)
```csharp
// 실수했던 코드: Dto에 로직과 도메인 필드명을 그대로 사용
[Serializable]
public class PlayerDto
{
    public string playerId;
    public string nickname;
    public int coins;
    public int gems;

    // ❌ Dto에 판정 로직 포함
    public bool CanAfford(int amount) => coins >= amount;

    // ❌ Dto에 가공 로직 포함
    public string GetDisplayName() => string.IsNullOrEmpty(nickname) ? "Guest" : nickname;
}

// ❌ UI가 Dto를 직접 사용
public class ProfilePanel : MonoBehaviour
{
    public void ShowPlayer(PlayerDto dto) // ❌ 도메인 모델이 아니라 Dto를 그대로 UI까지 전달
    {
        nameText.text = dto.GetDisplayName();
    }
}
```
**문제점**: 서버 API 응답 필드가 `nickname`에서 `nick_name`으로 바뀌자, Dto뿐 아니라 이 Dto를 직접 참조하던 UI 코드(`ProfilePanel`)까지 전부 수정해야 했다. Dto에 `CanAfford()` 같은 판정 로직까지 있어서, 같은 판정이 Service에도 중복 구현되어 두 곳의 결과가 어긋난 적도 있었다. 이후 Dto는 필드만 남기고, UI는 항상 도메인 모델(`Player`)만 참조하도록 강제했다.
