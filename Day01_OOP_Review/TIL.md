# Day 01 TIL

- 이름: `<장종민>`
- 작성일: `<2026-08-24>`

## 1. 오늘 막힌 부분 또는 내린 판단

`<상수를 선언할 때 Java에선 대문자로 선언하지만, C#에서는 대소문자를 같이 사용>`\
`<Java와 C#의 Test 문법이 다름>`\
`<Pray의 0~2 회복량의 Random 함수의 사용이 다름>`

## 2. 수정 전과 수정 후

### 수정 전

```csharp
using Day01_OOP_Review;

namespace Day01_OOP_Review_Test;

public class Tests
{

    [Test]
    public void Cleric_생성()
    {
        Cleric cleric = new Cleric("성직자");

        Console.WriteLine($"이름: {cleric.Name}");
        Console.WriteLine($"HP: {cleric.Hp}");
        Console.WriteLine($"MP: {cleric.Mp}");
        
        Assert.That(cleric.Name, Is.EqualTo("성직자"));
        Assert.That(cleric.Hp, Is.EqualTo(50));
        Assert.That(cleric.Mp, Is.EqualTo(10));
    }

    [Test]
    public void Cleric_이름_체크()
    {
        ArgumentException? ex = Assert.Throws<ArgumentException>(() =>
        {
            new Cleric("");
        });
        
        Console.WriteLine(ex.Message);

        Assert.That(ex.Message, Is.EqualTo("이름은 필수입니다."));
    }

    [Test]
    public void SelfAid_사용시_Mp_5_감소_모든_Hp_회복()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.Hp -= 25;
        
        cleric.SelfAid();

        Assert.That(cleric.Mp, Is.EqualTo(5));
        Assert.That(cleric.Hp, Is.EqualTo(50));
    }

    [Test]
    public void Mp_부족_회복_불가()
    {
        Cleric cleric = new Cleric("성직자");
        
        cleric.Hp -= 25;
        cleric.SelfAid();
        
        cleric.Hp -= 25;
        cleric.SelfAid();
        
        cleric.Hp -= 25;
        cleric.SelfAid();

        Assert.That(cleric.Mp, Is.EqualTo(0));
    }

    [Test]
    public void Pray_3초시_3에서_5_만큼_MP_회복()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.Mp -= 10;
        
        int result = cleric.Pray(3);
        
        Assert.That(result, Is.InRange(3, 5));
        Assert.That(cleric.Mp, Is.InRange(3, 5));
    }

    [Test]
    public void Mp가_최대Mp_넘는지_체크()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.SelfAid();

        int seconds = 100;
        int result = cleric.Pray(seconds);

        Console.WriteLine($"Pray: {seconds}초");
        Console.WriteLine($"Mp 회복량: {result}");
        Console.WriteLine($"Mp: {cleric.Mp}");

        Assert.That(cleric.Mp, Is.EqualTo(10));
    }

    [Test]
    public void Pray_회복된_MP를_반환()
    {
        Cleric cleric = new Cleric("성직자");

        
        cleric.SelfAid();
        
        int result = cleric.Pray(2);
        
        Assert.That(result, Is.InRange(2, 4));
        Assert.That(cleric.Mp, Is.InRange(7, 9));
    }
}
```

### 수정 후

```csharp
using Day01_OOP_Review;

namespace Day01_OOP_Review_Test;

public class Tests
{

    [Test]
    public void Cleric_생성()
    {
        Cleric cleric = new Cleric("성직자");

        Console.WriteLine($"이름: {cleric.Name}");
        Console.WriteLine($"HP: {cleric.Hp}");
        Console.WriteLine($"MP: {cleric.Mp}");
        
        Assert.That(cleric.Name, Is.EqualTo("성직자"));
        Assert.That(cleric.Hp, Is.EqualTo(50));
        Assert.That(cleric.Mp, Is.EqualTo(10));
    }

    [Test]
    public void Cleric_이름_체크()
    {
        ArgumentException? ex = Assert.Throws<ArgumentException>(() =>
        {
            new Cleric("");
        });
        
        Console.WriteLine(ex.Message);

        Assert.That(ex.Message, Is.EqualTo("이름은 필수입니다."));
    }

    [Test]
    public void SelfAid_사용시_Mp_5_감소_모든_Hp_회복()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.Hp -= 25;
        Console.WriteLine($"SelfAid 사용 전 Hp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 사용 전 Mp: {cleric.Mp}");
        
        cleric.SelfAid();
        Console.WriteLine($"SelfAid 사용 후 Hp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 사용 후 Mp: {cleric.Mp}");
        Assert.That(cleric.Mp, Is.EqualTo(5));
        Assert.That(cleric.Hp, Is.EqualTo(50));
    }

    [Test]
    public void Mp_부족_회복_불가()
    {
        Cleric cleric = new Cleric("성직자");

        Console.WriteLine($"SelfAid 사용 전 Mp: {cleric.Mp}");
        cleric.Hp -= 25;
        cleric.SelfAid();
        Console.WriteLine($"SelfAid 첫번째 사용 후 Hp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 첫번째 사용 후 Mp: {cleric.Mp}");
        
        cleric.Hp -= 25;
        cleric.SelfAid();
        Console.WriteLine($"SelfAid 두번째 사용 후 Mp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 두번째 사용 후 Hp: {cleric.Mp}");
        
        cleric.Hp -= 25;
        cleric.SelfAid();
        Console.WriteLine($"SelfAid 세번째 사용 후 Hp: {cleric.Hp}");
        Console.WriteLine($"SelfAid 세번째 사용 후 Mp: {cleric.Mp}");

        Assert.That(cleric.Mp, Is.EqualTo(0));
    }

    [Test]
    public void Pray_3초시_3에서_5_만큼_MP_회복()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.Mp -= 10;

        Console.WriteLine($"Mp 0 확인: {cleric.Mp}");
        int result = cleric.Pray(3);

        Console.WriteLine($"Pray 사용 후 Mp: {cleric.Mp}");
        Assert.That(result, Is.InRange(3, 5));
        Assert.That(cleric.Mp, Is.InRange(3, 5));
    }

    [Test]
    public void Mp가_최대Mp_넘는지_체크()
    {
        Cleric cleric = new Cleric("성직자");

        cleric.SelfAid();
        Console.WriteLine($"Mp: {cleric.Mp}");

        int seconds = 100;
        int result = cleric.Pray(seconds);

        Console.WriteLine($"Pray: {seconds}초");
        Console.WriteLine($"Mp 회복량: {result}");
        Console.WriteLine($"Mp: {cleric.Mp}");

        Assert.That(cleric.Mp, Is.EqualTo(10));
    }

    [Test]
    public void Pray_회복된_MP를_반환()
    {
        Cleric cleric = new Cleric("성직자");

        
        cleric.SelfAid();
        Console.WriteLine($"Pray 사용 전 Mp: {cleric.Mp}");
        
        int result = cleric.Pray(2);
        Console.WriteLine($"Pray 사용 후 Mp: {cleric.Mp}");
        
        Assert.That(result, Is.InRange(2, 4));
        Assert.That(cleric.Mp, Is.InRange(7, 9));
    }
}
```

`<테스트를 하는 데, 진행되고 있는 값들을 확인할 수 없었기 때문에 Console.WriteLine 들을 추가>`

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: `<Java와 C#의 Test 작성 차이점>`
- 제안받은 내용: `<Java에서는 주로 JUnit을 사용하고, C#에서는 NUnit을 사용하여 테스트 코드를 작성한다. 
두 언어 모두 테스트할 메서드를 지정하고 예상한 결과와 실제 결과를 비교하는 방식은 비슷하지만 문법에서 차이가 있다.>`

`<Java의 JUnit에서는 `@Test`, `@BeforeEach`와 같은 Annotation을 사용하지만, 
C#의 NUnit에서는 `[Test]`, `[SetUp]`과 같은 Attribute를 사용한다. 
또한 Java에서는 `assertEquals()`나 `assertTrue()` 등을 이용해 결과를 검증하는 반면, 
C#에서는 `Assert.That()`과 `Is.EqualTo()`, `Is.InRange()` 등을 조합하여 결과를 검증할 수 있다.>`

`<객체의 값을 확인하는 방법에서도 차이가 있다. Java에서는 보통 `getHp()`와 같은 Getter를 통해 값을 가져오지만, 
C#에서는 Property를 사용하기 때문에 `cleric.Hp`처럼 직접 값을 확인할 수 있다.>`

`<예외를 테스트할 때는 Java에서 `assertThrows()`를 사용하고, C#에서는 `Assert.Throws()`를 사용한다. 
전체적인 테스트의 목적과 구조는 비슷하지만, Java와 C#에서 사용하는 문법과 테스트 라이브러리에 차이가 있다는 것을 알 수 있었다.>`

- 채택 또는 거절한 내용: `<모두 참고했다.>`
- 판단한 이유: `<단순히 Java와 C#의 Test 작성이 궁금했기 때문에>`

AI 대화 전문을 붙이지 말고 질문, 판단, 검증 내용을 요약합니다.

## 4. 검증 결과

- 빌드: `<성공>`
- 실행 결과: `<확인한 동작>`
- 추가로 확인한 내용: `<진행되고 있는 값들을 확인>`

## 5. 아직 궁금한 점

`<Java 와 C# 의 변수 선언 차이점>`

## 6. 다음에 적용할 것

`<변수 선언 시 대소문자 표기 방식의 차이>`
