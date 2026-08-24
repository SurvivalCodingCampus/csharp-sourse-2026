# Day 01 TIL

## 학습 주제

`Java에서 익힌 객체지향 개념을 C# 문법과 관례로 표현하는 방법`을 복습했다. 클래스, 상속, 다형성의 의미는 유지하면서 키워드, 명명법, 프로퍼티, 표준 API가 어떻게 달라지는지 확인했다.

## 학습 목적

Java로 작성했던 객체지향 코드를 같은 동작의 C# 코드로 옮기기 위해 공부했다. 명시적인 `Program.Main()` 구조를 사용하고, Java식 사고를 C#의 프로퍼티와 접근 제한자, 컬렉션 문법으로 표현하는 데 목적을 두었다.

## 핵심 개념

| Java | C# | 핵심 차이 |
| --- | --- | --- |
| `package`, `import` | `namespace`, `using` | 소속과 타입 사용 범위를 표현한다. |
| `String`, `boolean` | `string`, `bool` | C# 기본 타입 별칭을 사용한다. |
| getter/setter 메서드 | 프로퍼티 | `get`, `set` 접근자로 상태 공개 범위를 제어한다. |
| `extends`, `implements` | `:` | 클래스 상속과 인터페이스 구현에 같은 기호를 사용한다. |
| `super` | `base` | 부모 생성자나 부모 멤버에 접근한다. |
| `final` | `const`, `readonly`, `sealed` | 상수, 생성 후 불변 필드, 상속 제한을 구분한다. |
| `ArrayList`, `HashMap` | `List<T>`, `Dictionary<TKey, TValue>` | 제네릭 컬렉션과 PascalCase API를 사용한다. |

C# 메서드는 기본적으로 재정의할 수 없다. 부모 클래스에서 `virtual`로 허용하고 자식 클래스에서 `override`로 재정의를 명시해야 한다.

`var`는 타입이 없는 변수가 아니라 컴파일러가 초기값으로 타입을 추론하는 문법이다. `string?`의 `?`는 해당 참조에 `null`이 들어갈 수 있음을 나타낸다.

### 접근 제한자 비교

| 제한자 | 접근 범위 | 사용 기준 |
| --- | --- | --- |
| `private` | 선언한 타입 내부 | 외부에서 직접 바꾸면 안 되는 상태와 구현 세부 사항에 사용한다. |
| `public` | 모든 접근 가능한 코드 | 외부에 제공할 생성자, 동작, 조회용 프로퍼티에 사용한다. |
| `protected` | 선언한 타입과 파생 타입 | 상속받은 클래스에서 사용할 멤버에 적용한다. |
| `internal` | 같은 어셈블리 | 어셈블리 내부에서만 공유할 타입이나 멤버에 적용한다. |

클래스 멤버의 기본 접근 수준은 `private`이고, 최상위 클래스의 기본 접근 수준은 `internal`이다. 접근 범위를 코드에 명시하면 객체가 외부에 제공하는 기능과 내부에서 보호할 상태를 구분하기 쉽다.

## 실습 또는 예제

Java로 만들었던 `Cleric`을 C#의 프로퍼티와 접근 제한자를 사용해 옮기는 예제를 작성했다.

```csharp
using System;

public class Cleric
{
    private const int MaxHp = 50;
    private const int MaxMp = 10;
    private const int SelfAidMpCost = 5;

    public string Name { get; }
    public int Hp { get; private set; }
    public int Mp { get; private set; }

    public Cleric(string name)
    {
        Name = name;
        Hp = MaxHp;
        Mp = MaxMp;
    }

    public void SelfAid()
    {
        if (Mp < SelfAidMpCost)
        {
            Console.WriteLine("MP가 부족합니다.");
            return;
        }

        Mp -= SelfAidMpCost;
        Hp = MaxHp;
    }

    public int Pray(int seconds)
    {
        if (seconds < 0)
        {
            Console.WriteLine("기도 시간은 0초 이상이어야 합니다.");
            return 0;
        }

        int recoveryAmount = seconds + Random.Shared.Next(0, 3);
        int previousMp = Mp;

        Mp = Math.Min(Mp + recoveryAmount, MaxMp);

        return Mp - previousMp;
    }
}
```

`Name`은 생성 시에만 정하고, `Hp`와 `Mp`는 외부에서 읽을 수 있지만 직접 변경할 수 없도록 `private set`을 적용했다. `SelfAid()`와 `Pray()`만 상태를 변경하게 하여 최대 HP와 최대 MP 규칙을 클래스 내부에서 보호했다.

## 이해한 내용

객체지향의 클래스, 인스턴스, 상속, 다형성 개념은 Java와 C#에서 공통으로 사용할 수 있다. 실제 변환에서는 개념 자체보다 C#의 키워드와 명명 관례, 프로퍼티 사용 방식에 주의해야 한다.

특히 공개 setter로 상태를 열어 두는 대신 `public int Mp { get; private set; }`처럼 변경 범위를 제한하고 의미 있는 메서드가 상태를 바꾸게 할 수 있다. 이를 통해 객체가 지켜야 하는 최대값이나 소비 조건을 클래스 내부에 둘 수 있다.

또한 C#에서는 공개 메서드와 프로퍼티에 PascalCase를 사용한다. Java 코드가 문법적으로 변환되더라도 `pray()`나 `getMp()`를 그대로 유지하기보다 `Pray()`와 `Mp`로 표현하는 편이 C# 관례에 맞다.

## 헷갈렸던 부분

`public`과 `private`을 타입 자체의 공개 여부와 멤버의 공개 여부로 나누어 판단하는 부분이 헷갈렸다.

`public class`는 다른 코드에서 해당 타입을 사용할 수 있게 하고, `public` 멤버는 객체 외부에 제공할 기능을 나타낸다. 반면 `private` 필드나 setter는 객체 내부에서만 상태를 변경하도록 제한한다. 모든 멤버를 `public`으로 두기보다 외부에서 반드시 사용해야 하는 기능만 공개하는 기준이 중요하다.

## 다시 볼 포인트

- 부모 메서드를 재정의하려면 `virtual`과 `override`를 함께 확인한다.
- `const`, `readonly`, `static readonly`, `sealed`가 각각 어떤 불변성과 제한을 나타내는지 구분한다.
- `public`은 외부에 제공할 기능에, `private`은 보호할 상태와 구현 세부 사항에 사용한다.
- 프로퍼티의 `private set`을 사용하면 외부 조회와 내부 변경을 분리할 수 있다.
- `Console.ReadLine()`의 결과가 `string?`이라는 점과 null 처리 방법을 다시 확인한다.
- `List<T>`, `HashSet<T>`, `Dictionary<TKey, TValue>`의 추가, 개수 확인, 안전한 조회 API를 비교한다.