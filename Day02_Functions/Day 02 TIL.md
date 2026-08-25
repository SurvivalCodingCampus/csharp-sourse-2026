# Day 02 TIL

- 이름: `<문재원>`
- 작성일: `<2026-08-25>`

## 1. 오늘 막힌 부분 또는 내린 판단

전체 트랙잭션 중 최대값, 최소값을 구하는 문제에서 막혔으나, <br>
Console.WriteLine() 문 안에 넣어서 해보니 출력이 되는 것을 확인하여 해결하였습니다.

## 2. 수정 전과 수정 후

### 수정 전

```csharp
처음 작성한 문장
// transactions.Aggregate((e, v) => Math.Max(e, v));>
두 번째 작성한 문장
// transactions.MaxBy(e => e.Value);
```

### 수정 후

```csharp
최종 수정코드
// Console.WriteLine(transactions.Max(e => e.Value));
```

- 처음 작성한 문장에서는 Math.Max는 int, double 등을 비교하는 것인데, e, v 는 Transaction 오류가 나서 안되는 것으로 간주하였습니다.
- 두번째 작성한 것은 디버그로 Max가 구해지는 것을 확인은 하였으나, 출력까지 한 줄에 되지 않았습니다.
- 결론 : 출력까지 해보기 위해 Console.WriteLine() 문을 사용하여 안에 Max,Min 을 구하는 람다식을 작성하였습니다.

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: `<Aggregate((e, v) => Math.Max(e, v))를 사용할 수 없는 이유 // HashSet 말고 중복제거 함수는?>`
- 제안받은 내용: `<Math는 숫자를 비교하는 것인데 e,v 는 객체이다. // Distinct(), DistinctBy()>`
- 채택 또는 거절한 내용: `<채택 : DistinctBy()>`
- 판단한 이유: `HashSet 으로는 되지 않아 또 다른 방법이 있나 질문 후, Distinct와 DinstinctBy() 둘 다 사용해 본 후 되는 것으로 사용`



## 4. 검증 결과

- 빌드: `<성공>`
- 실행 결과: `<출력값으로 실행결과 확인>`
- 추가로 확인한 내용: ``

## 5. 아직 궁금한 점

`<람다식, Delegate를 더 공부해야 할 듯하다.>`

## 6. 다음에 적용할 것

`<Not yet>`
