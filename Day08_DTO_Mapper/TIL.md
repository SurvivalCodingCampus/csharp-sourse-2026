# Day 08 TIL

- 이름: 장종민
- 작성일: 2026-09-21

## 1. 오늘 막힌 부분 또는 내린 판단

DTO와 Model을 왜 분리해야 하는지 이해하는 부분이 헷갈렸다.

JSON 데이터에는 null이나 잘못된 값이 들어올 수 있기 때문에  
DTO는 Nullable로 받고, Mapper에서 기본값으로 처리한 뒤  
Model로 변환하는 구조로 작성하였다.

## 2. 수정 전과 수정 후

### 수정 전

```csharp
var pokemon =
    JsonConvert.DeserializeObject<Pokemon>(json);
```

### 수정 후

```csharp
var dto =
    JsonConvert.DeserializeObject<PokemonDto>(json);

return dto.ToModel();
```

JSON을 바로 Model로 받지 않고  
DTO로 받은 뒤 Mapper를 통해 Model로 변환하도록 수정하였다.

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용
- 질문: 과제 1~4에서 각각 무엇을 구현해야 하는지와 DTO, Model,   
Mapper, DataSource, Repository의 역할
- 제안받은 내용: JSON 데이터를 DTO로 받고, Mapper를 통해 Model로 변환하며,    
DataSource는 데이터 통신, Repository는 비즈니스 로직을 담당하는 구조라고 설명받았다.
- 채택 또는 거절한 내용: 과제의 전체적인 구조와 각 클래스의 역할에 대한 설명을 참고
- 판단한 이유: 과제에서 요구하는 데이터 흐름과 각 클래스의 역할을 이해한 뒤 직접 코드에 적용하기 위해 사용

## 4. 검증 결과

- 빌드: 성공
- 실행 결과:  
Name: jigglypuff  
Types: normal, fairy  
Height: 0.5m  
Weight: 5.5kg  
Attack: 45  
Defense: 20  
Speed: 20  
Special Attack: 45  
Special Defense: 25  
- 추가로 확인한 내용: null이나 잘못된 값이 들어와도 Mapper에서 기본값으로 변환되는 것을 테스트로 확인

## 5. 아직 궁금한 점

DTO와 Model을 어느 기준으로 나누는지 더 알아보고 싶다.  
데이터가 잘못 들어왔을 때 기본값 처리와 예외 처리를 어떻게 구분하는지도 궁금하다.

## 6. 다음에 적용할 것

API 데이터를 사용할 때 DTO와 Model의 역할을 먼저 구분해서 작성할 것이다.  
Mapper를 사용해 잘못된 데이터가 직접 Model에 들어가지 않도록 처리할 것이다.  