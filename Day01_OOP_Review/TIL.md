# Day 01 TIL

여기에 템플릿 복사해서 활용할 것

Day 01 TIL

이름: 서인호

작성일: <2026-08-24>

1. 오늘 막힌 부분 또는 내린 판단
    Test.cs 작성, private 설정때려서 외부에서 불러오기가 안되고잇음


2. 수정 전과 수정 후
   수정 전
   class Cleric
   {
   private const int MaxHp = 50;
   private const int MaxMp = 10;

   public string Name { get; private set; }
   private int currentHp;
   private int currentMp;

   public Cleric(string name)
   {
   if (string.IsNullOrWhiteSpace(name))
   {
   throw new ArgumentException("성직자는 이름을 가지고 생성되어야 합니다.");
   }
   this.Name = name;
   this.currentHp = MaxHp; 
   this.currentMp = MaxMp;
   }

  
   public void SelfAid()
   {
   if (currentMp >= 5)
   {
   currentMp -= 5;
   currentHp = MaxHp; 
   // Console.WriteLine($"{Name}이(가) SelfAid를 사용하여 HP를 회복했습니다."); 
   }
   else
   {
   Console.WriteLine($"{Name}은(는) SelfAid를 할 MP가 부족합니다.");
   }
   }

   public int Pray(int seconds)
   {
  
   int mpToRestore = seconds + (seconds % 3); // seconds + (0, 1, 또는 2)

        
        if (currentMp + mpToRestore > MaxMp)
        {
            mpToRestore = MaxMp - currentMp; 
        }

        currentMp += mpToRestore;

        return mpToRestore;
   }

   
   public int Pray()
   {
   return Pray(1);
   }

   public void DisplayStatus()
   {
   Console.WriteLine($"이름: {Name}, HP: {currentHp}/{MaxHp}, MP: {currentMp}/{MaxMp}");
   }
   }


   수정 후

  class Cleric
  {
  private const int MaxHp = 50;
  private const int MaxMp = 10;

    public string Name { get; private set; }
    private int currentHp;
    private int currentMp;

    public Cleric(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("성직자는 이름을 가지고 생성되어야 합니다.");
        }
        this.Name = name;
        this.currentHp = MaxHp; 
        this.currentMp = MaxMp; 
    }



3. AI 사용 여부와 채택, 거절한 이유

   AI 사용 여부: 사용함

   질문: currentMp currentHp, MaxHp 등등의 심볼이 해결되지 않았대 이게 무슨 의미야

   제안받은 내용: 변수들이 Cleric 클래스 내부에 선언되지 않았기 때문에 발생함

   채택 또는 거절한 내용: 클래스 내부에 변수들을 다시 선언함

   판단한 이유: 
  

4. 검증 결과
   빌드: 성공 / 실패
   실행 결과: X
   추가로 확인한 내용:



5. 아직 궁금한 점
   <해결하지 못했거나 더 알아보고 싶은 내용>


6. 다음에 적용할 것
   <다음 코딩에서 직접 적용할 한 가지>