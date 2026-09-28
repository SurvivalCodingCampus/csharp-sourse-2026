# Mapper 가이드

## 적용 범위

DTO와 도메인 모델 사이의 변환. 외부 데이터의 형식 검증과 단위 변환을 이 경계에서 수행한다.

## 규칙

- Mapper는 순수 함수에 가깝게 만든다. 같은 입력에 같은 결과를 내고, 네트워크·DB·파일에 접근하지 않는다.
- API 단위와 도메인 단위가 다르면 명시적으로 변환한다. 예: 센티미터를 미터로 변경.
- 선택 필드는 의미가 분명한 기본값을 쓸 수 있다. 필수 ID나 핵심 상태가 없으면 임의의 정상 모델을 만들지 않는다.
- 변환 불가능한 입력은 명시적으로 실패시킨다. Repository/Service가 이를 데이터 오류로 처리할 수 있게 한다.
- 점수·해금·보상 규칙은 Mapper에 넣지 않는다.

## Good 예시 코드

아래 `PlayerProfile` 생성자는 향후 도메인 모델의 실제 생성 방식에 맞춰 조정한다.

```csharp
public static PlayerProfile ToModel(this PlayerProfileDto? dto)
{
    if (dto?.PlayerId is null ||
        string.IsNullOrWhiteSpace(dto.Nickname))
    {
        throw new InvalidDataException(
            "Required profile fields are missing.");
    }

    return new PlayerProfile(
        dto.PlayerId.Value,
        dto.Nickname.Trim(),
        dto.AvatarId ?? "default");
}
```

`"default"` 사용도 실제 아바타 규칙에서 허용할 때만 적용한다.

## Bad 예시 코드

```csharp
if (dto is null)
    return new PlayerProfile(Guid.Empty, "Unknown", "default");
```

프로필이 없거나 응답이 깨졌는데도 정상 프로필처럼 보이게 한다. 이 상태로 저장하면 잘못된 사용자에게 데이터가 연결될 수 있다.

## 기존 코드에서 확인한 교훈

`Day09_Result_Pattern/Data/Mapper/PokemonMapper.cs`는 `null` DTO를 기본 `Pokemon`으로 만든다. 학습용 표시 데이터에서는 한 선택일 수 있지만 필수 식별자가 있는 Island 게임의 프로필·진행도에는 그대로 적용하지 않는다.
