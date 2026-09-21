# Day 09 TIL

- 이름: <이혁>
- 작성일: <2026-09-21>

## 1. 오늘 막힌 부분 또는 내린 판단

<디렉토리를 세분화한 구조의 뜻은 이해하였으나, 각각의 구조에 맞는 코드를 작성하기에 어려움이 많아 AI의 도움을 많이 받았습니다.>

## 2. 수정 전과 수정 후

### 수정 전

```csharp
public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto? dto)
    {
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
    }
}
```

### 수정 후

```csharp
public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto dto)
    {
        string safeName = string.IsNullOrWhiteSpace(dto.Name) ? "Unknown" : dto.Name;

        string safeImageUrl = dto.Sprites?.Other?.OfficialArtwork?.FrontDefault
                              ?? dto.Sprites?.FrontDefault
                              ?? "default_img.png";

        return new Pokemon(Name: safeName, ImageUrl: safeImageUrl);
    }
}
```

<if (dto == null) 분기는 사실상 의미없는 분기입니다. this PokemonDto? dto로 확장 메서드를 만들어도, 호출부에서 pokemonDto?.ToModel()처럼 ?.로 호출하면 dto가 null인 순간 컴파일러가 ToModel 자체를 호출하지 않고 바로 null을 반환합니다. 그래서 null 분기 안쪽 코드는 절대 실행되지 않습니다.
또한 try/catch도 실질적으로 무의미합니다. 이 메서드 안에서 하는 일은 전부 ?.와 ??로 안전하게 null을 처리하는 것뿐이라 던질 예외 자체가 없습니다. "혹시 몰라서" 잡아두는 catch는 진짜 버그가 났을 때 그걸 조용히 삼켜버려서 디버깅만 어렵게 만들어서 코드를 수정했습니다.>

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: <사용함>
- 질문: <위의 코드에서 try/catch가 실용적이야?>
- 제안받은 내용: <위의 코드에서는 try/catch가 무의미하고 버그가 났을 때 디버깅이 힘들어질 수 있습니다.>
- 채택 또는 거절한 내용: <try/catch를 지우고 코드를 수정하였습니다.>
- 판단한 이유: <AI의 설명을 듣고 이해하니, AI의 설명이 맞아 코드를 실용적으로 수정하였습니다.>

AI 대화 전문을 붙이지 말고 질문, 판단, 검증 내용을 요약합니다.

## 4. 검증 결과

- 빌드: <성공>
- 실행 결과: <Unit Tests 창으로 테스트 결과 확인 후, 테스트 성공이 나와 Git Hub에 사진으로 첨부하였습니다.>
- 추가로 확인한 내용: 

## 5. 아직 궁금한 점

<디렉토리 구조에 있는 코드들을 완전히 내 것으로 이해하고 정리하지는 못했습니다.>

## 6. 다음에 적용할 것

<디렉토리 구조에 있는 코드들을 이해하고, 새로 배운 코드들을 적용하고 싶습니다.>