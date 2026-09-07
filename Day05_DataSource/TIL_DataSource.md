# Day 01 TIL

- 이름: `<박성연>`
- 작성일: `<2026-09-07>`

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



데이터와 로직을 분리하는 이유: 1. 유지보수 2. 협업의 효율성 - 수정 편함
DataSource: 앱이 사용하는 원천 데이터. 
- 역할: 대부분 외부 데이터 저장소와 직접 통신. Raw 데이터 수신 및 처리. CRUD(Create, Read, Update, Delete) 작업 수행
- 종류: Test(.txt 등), File(로컬 파일), JSON(웹 API에서 자주 사용), XML, CSV (엑셀 같은 형식), RDBMS (MySQL, PostgreSQL 등 관계형 DB), NoSQL(MongoDB, Firebase Firestore 등)
<br>소스마다 장단점 다름. 상황에 따라 적절히 선택
- e.g. 아이템 기존 정보를 외부 파일에 저장하고 게임 실행 시 로드하고 사용. 게임 플레이 중 발생한 모든 상태 변화를 저장하는 외부 파일

데이터 모델: 데이터 구조를 정의. 저장할 데이터 모양을 클래스로 작성.
<br>
<br>데이터 소스: 데이터 소스에 대한 추상화를 담당하는 인터페이스 정의. 기본적인 기능을 제공, 실제 데이터 저장 장소 상관 X
  <br> 유연한 설계를 위해 이름 짓기, 인터페이스를 사용
  <br> - 이름 짓기: 저장소 위치 기준 e.g. LocalUser-,RemoteUser-, CachedUser-, 기술 스택 기준: RoomUser-, RetrofitUser-, SharedPrefsUser-.
  <br> - 인터페이스 사용: I접두사를 사용한 인터페이스명, 인터페이스와 구현체 구분 용이, 많은 기업/프로젝트에서 채택


디렉토리 구조
![img.png](img.png)


AI정리
![img_1.png](img_1.png)  ![img_3.png](img_3.png) ![img_2.png](img_2.png)
## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: `<조건에 따른 클래스코드와 이에 따른 테스트코드 작성>`
- 제안받은 내용:




## 4. 검증 결과

- 빌드: `<성공 / 실패>`
- 실행 결과: `<확인한 동작>`
- 추가로 확인한 내용: `<테스트 또는 예외 상황>`

## 5. 아직 궁금한 점

`<해결하지 못했거나 더 알아보고 싶은 내용>`

## 6. 다음에 적용할 것

`<다음 코딩에서 직접 적용할 한 가지>`