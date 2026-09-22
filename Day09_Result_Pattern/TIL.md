# Day 09 TIL

- 이름: 장종민
- 작성일: 2026-09-21

## 1. 오늘 막힌 부분 또는 내린 판단

기존 포켓몬 과제에서 작성한 `Result`, `Response` 등의 코드를  
지하철 기능에서도 어떻게 재사용할지 구조를 잡는 부분이 헷갈렸다.

## 2. 수정 전과 수정 후

### 수정 전

```csharp
var subwayList =
    response.Body.RealtimeArrivalList
        .Select(dto => dto.ToModel())
        .ToList();
```

### 수정 후

```csharp
if (response.Body.RealtimeArrivalList == null ||
    response.Body.RealtimeArrivalList.Count == 0)
{
    return new Result<List<Subway>, SubwayError>.Error(
        SubwayError.StationNotFound
    );
}

var subwayList =
    response.Body.RealtimeArrivalList
        .Select(dto => dto.ToModel())
        .ToList();
```

잘못된 역을 검색했을 때 빈 데이터를 그대로 처리하지 않고  
`StationNotFound` 에러를 반환하도록 수정

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용
- 질문: 포켓몬 과제에서 작성한 `Result`, `Response` 등의 코드를   
지하철 과제에서도 재사용할 수 있는지와 `Program.cs`에서 두 기능을 함께 실행하는 방법을 질문  
- 제안받은 내용: `Result`와 `Response`는 공용으로 재사용하고,   
지하철에 필요한 `DTO`, `Mapper`, `Model`, `Repository`만 추가한 뒤   
`Program.cs`에서 `switch`문으로 포켓몬과 지하철 기능을 선택하도록 구성하는 방법을 설명받음
- 채택 또는 거절한 내용: 기존 공용 코드를 재사용하고  
`Program.cs`에서 `switch`문으로 기능을 나누는 방식을 채택
- 판단한 이유: 같은 코드를 중복해서 작성하지 않고 기존 기능도 유지하면서   
새로운 기능을 추가할 수 있어 관리하기 쉽다고 판단

## 4. 검증 결과

- 빌드: 성공
- 실행 결과: 
1. 포켓몬 정보
2. 지하철 도착 정보  
선택: 2  
역 이름: 서울
         
역 이름: 서울  
방향: 상행  
열차 정보: 양주행 - 시청방면  
종착역: 양주  
도착 정보: 서울 도착  
현재 위치: 서울  
도착까지 남은 시간: 0초  
도착 상태 코드: 1  

- 추가로 확인한 내용: `TimeoutException`, `JsonSerializationException`,   
지하철 역 존재 여부를 `MockDataSource`로 테스트하였고 모든 테스트가 성공

## 5. 아직 궁금한 점

`Mock`과 실제 API 테스트를 어떤 기준으로 나누어 작성하는지 더 알아보고 싶다.   
또한 에러 종류가 많아질 경우 `Result`의 에러 타입을 어떻게 관리하는지도 궁금하다.

## 6. 다음에 적용할 것

외부 API를 사용할 때 정상 데이터뿐만 아니라  
빈 데이터나 예외 상황도 `Result`와 `Mock`을 이용해 함께 검증할 것이다.