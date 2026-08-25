# Day 01 TIL

- 이름: `<박성연>`
- 작성일: `<2026-08-24>`

## 1. 오늘 막힌 부분 또는 내린 판단

`<if문을 적절하게 사용하지 못한 것 같습니다.>`

## 2. 수정 전과 수정 후

### 수정 전

```csharp
//  public string name;
    public static int HP = MaxHP;
    public static int MP = MaxMP;
    public static int seconds;
    public Cleric(string name, int HP, int MP)
    {
        this.name = name;
    }

    public void SelfAid()
    {
        MP -= 5;
        HP = MaxHp;
    }
    
    public void Pray(int seconds)
    {
        Amounts = seconds + Random(0,);
        if ((MP + Amounts) >= MAXMP)
        {
            MP = MaxMp;
            
        }

        return Amounts;
    }
```

### 수정 후

```csharp
        public string Name { get; private set; }
        public int HP { get; private set; }
        public int MP { get; private set; }
        public const int MaxHP = 50;
        public const int MaxMP = 10;

        public Cleric(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("이름이 없는 성직자는 생성할 수 없습니다.");
            }

            Name = name;
            HP = MaxHP;
            MP = MaxMP;
        }

        /// <summary>
        /// MP 5를 소비하고 HP를 최대로 회복합니다.
        /// </summary>
        public void SelfAid()
        {
            if (MP >= 5)
            {
                MP -= 5;
                HP = MaxHP;
            }
        }

        /// <summary>
        /// 지정된 초(seconds)만큼 기도하여 MP를 회복합니다. (0~2 추가 회복)
        /// </summary>
        /// <param name="seconds">기도 시간</param>
        /// <returns>실제로 회복된 MP 양</returns>
        public int Pray(int seconds)
        {
            Random rand = new Random();
            // 0~2 사이의 랜덤 값 생성
            int bonus = rand.Next(0, 3); 
            int potentialRecovery = seconds + bonus;
            
            int oldMP = MP;
            // MP가 MaxMP를 넘지 않도록 계산
            MP = Math.Min(MaxMP, MP + potentialRecovery);
            
            return MP - oldMP; // 실제로 회복된 양 반환
        }
```

`<C#에서 캡슐화와 랜덤함수도 잘 기억이 나지 않아 AI의 도움을 받았습니다..>`

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: `<조건에 따른 클래스코드와 이에 따른 테스트코드 작성>`
- 제안받은 내용: 
`1. 생성자 검증: string.IsNullOrWhiteSpace(name)를 사용하여 이름이 없거나 공백인 경우 ArgumentException을 던지도록 구현했습니다.
2. SelfAid(): MP >= 5 조건을 확인하여 MP를 차감하고 HP를 MaxHP로 즉시 설정합니다.

3. Pray(int seconds):
Random.Next(0, 3)를 사용하여 0, 1, 2 중 하나의 숫자를 생성합니다.
Math.Min을 사용하여 계산된 MP가 MaxM`
- 채택 또는 거절한 내용: `<AI가 작성한 코드를 채택했습니다.>`
- 판단한 이유: `<제가 아는 지식을 가지고 문장처럼 해석하였습니다.>`

AI 대화 전문을 붙이지 말고 질문, 판단, 검증 내용을 요약합니다.

## 4. 검증 결과

- 빌드: `<성공 / 실패>`
- 실행 결과: `<확인한 동작>`
- 추가로 확인한 내용: `<테스트 또는 예외 상황>`

## 5. 아직 궁금한 점

`<해결하지 못했거나 더 알아보고 싶은 내용>`

## 6. 다음에 적용할 것

`<다음 코딩에서 직접 적용할 한 가지>`