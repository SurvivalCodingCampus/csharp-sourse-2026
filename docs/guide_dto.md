# DTO 가이드

## 적용 범위

API JSON, DB 행, 파일 형식 등 외부 데이터의 모양을 표현하는 타입.

## 규칙

- DTO는 외부 필드명과 Nullable 가능성을 반영한다. 외부 계약이 바뀌면 DTO와 Mapper를 함께 점검한다.
- DTO에 레벨 해금, 점수, 보상 같은 게임 규칙을 넣지 않는다.
- 선택 필드의 기본값과 필수 필드 누락을 구분한다. 필수 식별자가 없는데 빈 ID로 대체하지 않는다.
- API 응답을 도메인 모델로 직접 역직렬화하지 않는다. DTO를 받고 Mapper를 거친다.
- DTO는 DataSource/Mapper/Repository 내부에서만 사용하고 Service와 UI의 계약으로 노출하지 않는다.

## Good 예시 코드

```csharp
public sealed class PlayerProfileDto
{
    [JsonPropertyName("player_id")]
    public Guid? PlayerId { get; init; }

    [JsonPropertyName("nickname")]
    public string? Nickname { get; init; }

    [JsonPropertyName("avatar_id")]
    public string? AvatarId { get; init; }
}

var dto = JsonSerializer.Deserialize<PlayerProfileDto>(json);
```

필수 여부는 실제 API 스키마를 확인한 뒤 Mapper에서 검사한다. 위 필드명은 설계 예시다.

## Bad 예시 코드

```csharp
var profile = JsonSerializer.Deserialize<PlayerProfile>(json);
```

외부 응답 형식이 도메인 모델 생성 방식과 결합된다. API의 `null`이나 필드명 변경이 도메인 모델에 바로 전파된다.

## 기존 코드에서 확인한 교훈

`Day08_DTO_Mapper/TIL.md`에는 `JsonConvert.DeserializeObject<Pokemon>(json)`로 직접 읽던 방식을 `PokemonDto`와 Mapper로 고친 경험이 기록돼 있다. Island 게임에도 같은 경계를 유지한다.
