# __Day 01 TIL__

## 배운내용
### Java와 C#의 차이점
- Java와 C#은 거의 같지만 일부 세세한 차이가 있었다.
#### Java / C#

- 클래스, 상속, 다형성에서는 공통점이 있지만 명명법이나 키워드, 표준 api에선 차이가 있다.

```csharp
//java

public class Main{
    public stsic void main(String[] args){
        System.out.println("Hello");
    }
}

//C#
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello");
    }
}
```
#### 차이점 (코드와 함께 보기)
- 타입 이름에선 근소한 차이가 있다.
```csharp
//java > C#
//boolean > bool
//String > string
//Object > object
```
- C#은 var과 null 가능성을 따로 생각한다. null이 들어갈 가능성을 타입에 ?를 붙여 표현한다.
```csharp
//Java
var hero = new Hero();
String nickname = null;
//C# 
var hero = new Hero();
string? nickname = null;
string name = "아서스";

//var 는 타입이 없는 것이 아니다. ? 는 null이 들어갈 수 있음을 표시한다.
```
- 콘솔 입출력 api가 달라진다.
```csharp
//java
Scanner scanner = new Scanner(System.in);
System.out.print("이름: ");
String name = scanner.nextLine();
//C#
Console.Write("이름: ");
string name = Console.ReadLine() ?? "";

//Console.ReadLine() 의 결과는 string? 이므로 null 처리까지 생각한다.
```
- 문자열 메서드는 PascalCase다
```csharp
//Java
String text = "Hello Java";
text.length();
text.toLowerCase();
text.contains("Java");
//C# 
string text = "Hello C#";
text.Length;
text.ToLower();
text.Contains("C#");
string message = $"길이: {text.Length}";
//길이는 Length 프로퍼티다. 문자열 앞에 $ 를 붙이면 {표현식} 을 넣을 수 있다.
```
- override는 허용과 재정의를 모두 표시한다.
```csharp
class Hero
{
public virtual void Run() { }
}
class SuperHero : Hero
{
public override void Run() { }
}
//부모는 virtual, 자식은 override를 사용
```
- C#은 implements 없이 인터페이스를 구현한다.
```csharp
interface IDrawable
{
 void Draw();
}
class Hero : IDrawable
{
 public void Draw() { }
}
///관례상 I부터 시작
```
- 얕은 복사와 깊은 복사는 직접 의도를 드러낸다.
##### 기억할 점
###### (Java에서 이 표현은 C#에선 이렇게 한다.)
- package와 import는 C#에선 namespace와 using으로 바꿔 사용한다.
- getter/setter는 프로퍼티로 표현한다.(변경 범위도 프로퍼티에서 제어)
- 프로퍼티 안에서 값을 검증 할 수도 있다.
- extends를 C#에선 콜론(;)으로 사용한다.
- super는 base로 사용한다.
- Java의 instanceof는 is로 바꾼다.
- arrayList는 List<T>로 바꾼다.(중복 없는 집합은 HashSet<T>)
- HashMap은 Dictionary로 바꾼다.
- enum은 내부적으로 int 값들로 취급된다.

## 그외
### 어려웠던점

- 테스트코드 작성 법. 항상 어떻게 코드를 짜야하는 지 막막하다.

### 해결 방법

- 현재 해결 방법으로는 구글링과 ai를 활용 하였고, 그럼에도 틀린 부분이 생긴 상황에는 자동완성이 추천하는 코드로 수정 하였다.
### 다음에 더 공부 할 점


- 테스트 코드 작성법
