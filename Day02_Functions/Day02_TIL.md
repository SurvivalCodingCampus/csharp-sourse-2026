# Day 02 TIL

- 이름: `<이혁>`
- 작성일: `<2026-08-25>`

## 1. 오늘 막힌 부분 또는 내린 판단

`<특정 기준으로 중복을 제거하는 DistinctBy와 최대값과 최소값을 구하는 것을 몰라서 찾아보고 적용하였습니다.>`

## 2. 수정 전과 수정 후

### 수정 전

```csharp
// < transactions.Where(e => e.Trader.City == "Cambridge")
            .OrderBy(e => e.Trader.Name)
            .ToList()
            .ForEach(e => Console.WriteLine(e.Trader.Name));>
```

### 수정 후

```csharp
// < transactions.Where(e => e.Trader.City == "Cambridge")
            .OrderBy(e => e.Trader.Name)
            .DistinctBy(e => e.Trader.Name)
            .ToList()
            .ForEach(e => Console.WriteLine(e.Trader.Name));>
```

`<특정 기준으로 중복을 제거하는 DistinctBy를 몰랐는데, 디버깅 과정에서 알게 되어 찾아보고 수정하였습니다.>`

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: `<수정 전의 코드의 문제점이 무엇인가?, DistinctBy가 무엇인가?, 최대값과 최소값을 어떻게 나타내는가?>`
- 제안받은 내용: `<수정 전의 코드의 문제점이 DistinctBy가 누락된 것이라고 알려주었습니다.>`
- 채택 또는 거절한 내용: `<DistinctBy를 선택했습니다.>`
- 판단한 이유: `<DistinctBy에 대해서 자세한 설명을 들은 후, 적절한 것 같아 선택하였습니다.>`

AI 대화 전문을 붙이지 말고 질문, 판단, 검증 내용을 요약합니다.

## 4. 검증 결과

- 빌드: `<성공>`
- 실행 결과: `<출력값으로 실행결과 확인하였습니다.>`
- 추가로 확인한 내용: ``

## 5. 아직 궁금한 점

`<람다식과 델리게이트에 대해서 더 공부하고 익숙해져야 할 것 같습니다.>`

## 6. 다음에 적용할 것

`<DistinctBy와 최대값 그리고 최소값을 적용해야 할 때 잘 적용해야겠습니다.>`