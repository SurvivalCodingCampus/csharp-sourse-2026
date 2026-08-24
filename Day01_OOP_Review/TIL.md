# Day 01 TIL

- 이름: `윤우상`
- 작성일: `2026-08-24`

## 1. 오늘 막힌 부분 또는 내린 판단

`Cleric 클래스 생성시 이름에 null값이 할당 될 경우 오류 발생 유도 방안을 Java 형식으로 사용했다가
쉽게 작성하는 방법을 IDE에서 수정 추천이 있어 해당 내용으로 수정함`

## 2. 수정 전과 수정 후

### 수정 전

```csharp
// <수정 전 코드를 작성하세요.>
public Cleric(string name, int hp, int mp)
{
    if (name is null)
    {
        throw new ArgumentNullException(nameof(name));
    }
    this.Name = name;
    this.HP = hp >= MaxHp ? MaxHp : hp > 0 ? hp : 0;
    this.MP = mp >= MaxMp ? MaxMp : mp > 0 ? mp : 0;
}
    
```

### 수정 후

```csharp
// <수정 후 코드를 작성하세요.>
public Cleric(string name, int hp, int mp)
{
    this.Name = name ?? throw new ArgumentNullException(nameof(name));
    this.HP = hp >= MaxHp ? MaxHp : hp > 0 ? hp : 0;
    this.MP = mp >= MaxMp ? MaxMp : mp > 0 ? mp : 0;
}

```

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `사용하지 않음`
- 질문: `<AI에게 한 질문>`
- 제안받은 내용: `<AI의 핵심 제안>`
- 채택 또는 거절한 내용: `<무엇을 선택했는지>`
- 판단한 이유: `<직접 검토한 근거>`

AI 대화 전문을 붙이지 말고 질문, 판단, 검증 내용을 요약합니다.

## 4. 검증 결과

- 빌드: `<성공>`
- 실행 결과: Cleric의 SelfAid() / Pray(int seconds 정상 작동 여부, 이름이 null값일 경우 예외처리 확인 성공, 최댓값  
- 추가로 확인한 내용: `<테스트 또는 예외 상황>`

## 5. 아직 궁금한 점

```
Assert 관련 함수에 대하여
JUnit과 NUnit의 차이점에 대하여
프로퍼티 사용 방법 등
```

## 6. 다음에 적용할 것

```
<다음 코딩에서 직접 적용할 한 가지>

프로퍼티를 사용하는 방법을 확인하여 getter/setter와 같은 구성 해보기
```

