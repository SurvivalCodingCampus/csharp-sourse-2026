# Day 08 TIL

- 이름: `<문재원>`
- 작성일: `<2026-09-15>`

## 1. 오늘 막힌 부분 또는 내린 판단
- PokemonMapper에서 조건문으로 Name과 Sprites를 하였으나, 테스트시 오류가 발생.. 
- AI에게 뭐가 틀렸는지 검사 후, if문으로 알려달라고 하였으나 코드가 너무 용이하지 않아 '수정 후' 코드로 변경

## 2. 수정 전과 수정 후


### 수정 전

```
if (dto == null)
{
    return new Pokemon(Name: "Unknown", ImageUrl: "default_img.png");
}

try
{
    if (string.IsNullOrWhiteSpace(dto.Name))
    {
        dto.Name = "Unknown";
        dto.Sprites.Other.OfficialArtwork.FrontDefault = "default_img.png";
    }

    if (string.IsNullOrEmpty(dto.Sprites?.Other?.OfficialArtwork?.FrontDefault!))
    {
        dto.Sprites?.Other?.OfficialArtwork?.FrontDefault = "default_img.png";
    }

    return new Pokemon(
        Name: dto.Name!,
        ImageUrl: dto.Sprites?.Other?.OfficialArtwork?.FrontDefault!);
}
catch (Exception)
{
    return new Pokemon(Name: "Error", ImageUrl: "default_img.png");
}
```

### 수정 후

```
if (dto == null)
{
    return new Pokemon(Name: "Unknown", ImageUrl: "default_img.png");
}

try
{
    string safeName = string.IsNullOrWhiteSpace(dto.Name) ? "Unknown" : dto.Name;
    
    string safeImageUrl = dto.Sprites?.Other?.OfficialArtwork?.FrontDefault 
                          ?? dto.Sprites?.FrontDefault 
                          ?? "default_img.png";
    
    if (string.IsNullOrWhiteSpace(safeImageUrl))
    {
        safeImageUrl = "default_img.png";
    }
    
    return new Pokemon(
        Name: safeName,
        ImageUrl: safeImageUrl
    );
}
catch (Exception)
{
    return new Pokemon(Name: "Error", ImageUrl: "default_img.png");
}
```



## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `사용`
- 질문: `테스트 코드 시나리오를 알려줘 및 PokemonMapper 코드 제시 후 오류확인`
- 제안받은 내용: `시나리오1~4 내용과 입력값 예시, 및 PokemonMapper 오류 확인 후 코드 추천`
- 채택 또는 거절한 내용: `시나리오 채택, PokemonMapper는 골라서 사용`
- 판단한 이유: `이전 코드는 수업내용 바탕으로 작성했으며` <br>
`시나리오 하나씩 예시를 보며 테스트를 진행했습니다.` <br>
`PokemonMapper 코드 채택은 두 가지 코드를 제시 받았으며, 현 사용한 코드가 다르게 제시해 준 코드를 짧게 처리해준 코드인 것을 확인 후 현 코드를 사용`



## 4. 검증 결과

- 빌드: `<성공>`
- 실행 결과: `<출력값으로 실행결과 확인>`
- 추가로 확인한 내용: ``

## 5. 배운 것

- API 구조를 그대로 DTO로 먼저 옮기고, Mapper를 통해 필요한 데이터만 가지고 오는 방식을 기억하기
- IsNullOrWhiteSpace :: Null 이거나 "" 공백이라면에 쓰이는 것
- IsNullorEmpty와 차이는 " " 중간 공백이 아닌 띄어쓰기 공백이 있다면 Empty는 false로 인식
- record :: record를 사용하면 get,set,ToString 등 쓰지 않아도 가능하다.


