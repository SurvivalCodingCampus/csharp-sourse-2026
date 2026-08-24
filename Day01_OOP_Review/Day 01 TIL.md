# Day 01 TIL

- 이름: `<문재원>`
- 작성일: `<2026-08-24>`

## 1. 오늘 막힌 부분 또는 내린 판단

1. Random함수에서 random.range로 하면 될 줄 알았는데, Unity에만 있는 함수였다.. <br>
=> random.Next()로 해야한다.

2. Test 하는 부분에서 어떤 용어를 써야할지 몰라 막혔다. <br>
=> AI에게 어떤 명령어를 써야하는지 도움요청

## 2. 수정 전과 수정 후

### 수정 전

```csharp
// private static Random rand = new Random();
```

### 수정 후

```csharp
/* public int Pray(int seconds)
    {
        Random random = new Random(); */
```

`<AI가 알려주고 그대로 사용을 해봤으나, 메서드 안에서도 잘 작동하는 것을 보고 메서드 안에 넣는 방법으로 수정>`

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: 
```
1. random.range()로 할 수 없나? 
2. Test 문에서 SetUp의 용도
3. Test 문 작성방법
```
- 제안받은 내용: 
```
1. random.range()는 Unity에 있는 것, random.Next()가 맞다, 메서드 안이 아닌 클래스 내부에서 써라.
2. Test 실행 직전 SetUp이 먼저 실행되는 것
3. Assert. 문
```
- 채택 또는 거절한 내용: `<채택 : Is.InRange 사용 // 거절 : Random을 클래스 안에서 사용하라고 한 것을 거절>`
- 판단한 이유: `<직접 코드를 리뷰를 하며, Random함수가 메서드 안에서 해도 된다는 것을 테스트로 확인하여 현재 쓰는 방식으로 변경>`



## 4. 검증 결과

- 빌드: `<성공>`
- 실행 결과: `<NameTest // SelfAidTest // PrayTest>`
- 추가로 확인한 내용: `<Is.EqualTo()안에 '+' 를 사용할 수 있다.>`

## 5. 아직 궁금한 점

`<역시나 Test문이 어렵다?>`

## 6. 다음에 적용할 것

`<Not yet>`

