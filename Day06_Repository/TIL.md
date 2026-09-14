# Day 01 TIL

- 이름: 김용운 
- 작성일: `<2026-08-24>`

## 1. 오늘 막힌 부분 또는 내린 판단

get; set;과 record의 차이점: 이전까지는 { get; set; }을 사용하여 프로퍼티를 열어두었으나, 
최신 C#에서는 데이터를 수정할 수 없도록 보장하는 불변성(Immutability) 관점에서 
record 사용이 권장됨을 이해함.

## 2. 수정 전과 수정 후

### 수정 전
public class PokemonDto
{
public int id { get; set; }
public string name { get; set; } = string.Empty;
}

### 수정 후
public record PokemonDto(
int Id,
string Name,
int Height,
int Weight,
SpritesDto? Sprites
);

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: record 불변성이 class와 무슨 차이인지, [property: JsonProperty(...)]와 ?는 왜 쓰는지
- 제안받은 내용: record를 통한 값 기반 비교 및 불변성 보장, DTO/Model 매핑 로직 및 Mocking 기법
- 채택 또는 거절한 내용: `<채택>`
- 판단한 이유: <class의 { get; set; }은 외부 변경 위험이 있지만 record는 생성 시점에 동결되어 안전함. 또한 API 속성과 C# 프로퍼티 이름을 짝지어주는 JsonProperty의 필요성을 명확히 이해하여 채택>


## 4. 검증 결과

- 빌드: `<성공>`
- 실행 결과: `조회할 포켓몬 이름을 입력하세요 (예: pikachu): pikachu

데이터 요청 중...
ID: 25
이름: pikachu
이미지 URL: https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/25.png
`

## 5. 아직 궁금한 점

세션관리 : controller로 요청을할떄마다 매번 api를 호출하면 효율이 안좋을거같다. 

## 6. 다음에 적용할 것

세션
