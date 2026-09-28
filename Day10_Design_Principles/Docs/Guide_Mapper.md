# Mapper 가이드라인

[규칙]
- Mapper는 Dto ↔ 도메인 모델 간 순수 변환 함수만 담당한다. 입력이 같으면 항상 같은 출력을 내며, 부수효과(I/O, 상태 변경, 로그)가 없다.
- Mapper는 static 클래스/메서드로 작성하며, 인스턴스 상태를 갖지 않는다.
- Mapper는 검증(validation)이나 비즈니스 판정을 포함하지 않는다. 필드 존재 여부, null 체크 등 "변환 가능하게 만드는" 최소한의 방어 코드만 허용한다.
- 양방향 변환(`ToDomain`, `ToDto`)을 모두 제공하여 Repository가 조회/저장 양쪽에서 일관되게 사용할 수 있도록 한다.
- Mapper는 다른 계층(DataSource, Repository, Service)을 호출하지 않는다.

[Good 예시 코드]
```csharp
// PlayerMapper.cs
public static class PlayerMapper
{
    public static Player ToDomain(PlayerDto dto)
    {
        return new Player
        {
            playerId = dto.player_id,
            nickname = dto.nick_name,
            countryFlag = dto.country_flag,
            profileImageUrl = dto.profile_image_url,
            coins = dto.coin_balance,
            gems = dto.gem_balance,
            goldenKeys = dto.golden_key_count
        };
    }

    public static PlayerDto ToDto(Player domain)
    {
        return new PlayerDto
        {
            player_id = domain.playerId,
            nick_name = domain.nickname,
            country_flag = domain.countryFlag,
            profile_image_url = domain.profileImageUrl,
            coin_balance = domain.coins,
            gem_balance = domain.gems,
            golden_key_count = domain.goldenKeys
        };
    }
}
```
필드명 차이(`nick_name` ↔ `nickname`)를 흡수하는 것이 Mapper의 유일한 책임이다.

[Bad 예시 코드] (본인이 실수한 경험 남기기)
```csharp
// 실수했던 코드: Mapper 안에서 검증과 기본값 판정을 수행
public static class PlayerMapper
{
    public static Player ToDomain(PlayerDto dto)
    {
        // ❌ Mapper가 비즈니스 규칙(음수 방지, 기본 닉네임)을 판정
        if (dto.coin_balance < 0)
        {
            throw new InvalidOperationException("코인은 음수일 수 없습니다.");
        }

        return new Player
        {
            playerId = dto.player_id,
            nickname = string.IsNullOrEmpty(dto.nick_name) ? "Guest" + UnityEngine.Random.Range(1000, 9999) : dto.nick_name, // ❌ 부수효과(랜덤) 포함, 순수 함수 아님
            coins = dto.coin_balance
        };
    }
}
```
**문제점**: `Random.Range`가 Mapper 안에 있어서 같은 Dto를 두 번 변환해도 다른 `Player`가 나오는 비결정적(non-deterministic) 함수가 되어버렸고, 이 때문에 단위 테스트에서 `Assert.AreEqual` 검증이 간헐적으로 실패했다. 또한 "코인 음수 방지" 같은 검증은 Mapper가 아니라 `PlayerService`가 담당해야 할 판정이었는데, Mapper에 있다 보니 Repository를 거치는 모든 조회에서 예외가 발생할 위험을 안고 있었다. 이후 Mapper는 순수 필드 매핑만 하도록 되돌리고, 기본 닉네임 규칙은 `PlayerService.getProfile()`로 옮겼다.
