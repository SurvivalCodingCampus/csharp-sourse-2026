# Day 01 TIL

- 이름: `<장종민>`
- 작성일: `<2026-08-25>`

## 1. 오늘 막힌 부분 또는 내린 판단

`<LINQ에서 Select와 Where, OrderBy 들은 DB의 쿼리문과 많이 유사하다고 느껴서 과제 풀이를 함>`

## 2. 수정 전과 수정 후

### 수정 전

```csharp
namespace Day02_Functions;

public class Trader
{
    public string Name { get; set; }
    public string City { get; set; }

    public Trader(string name, string city)
    {
        Name = name;
        City = city;
    }
}

public class Transaction
{
    public Trader Trader { get; set; }
    public int Year { get; set; }
    public int Value { get; set; }

    public Transaction(Trader trader, int year, int value)
    {
        Trader = trader;
        Year = year;
        Value = value;
    }
}

public class MainClass
{
    public static List<Transaction> transactions = new List<Transaction>
    {
        new Transaction(new Trader("Brian", "Cambridge"), 2011, 300),
        new Transaction(new Trader("Raoul", "Cambridge"), 2012, 1000),
        new Transaction(new Trader("Raoul", "Cambridge"), 2011, 400),
        new Transaction(new Trader("Mario", "Milan"), 2012, 710),
        new Transaction(new Trader("Mario", "Milan"), 2012, 700),
        new Transaction(new Trader("Alan", "Cambridge"), 2012, 950)
    };

    public static void Main(string[] args)
    {
        
    }
}

```

### 수정 후

```csharp
namespace Day02_Functions;

public class Trader
{
    public string Name { get; set; }
    public string City { get; set; }

    public Trader(string name, string city)
    {
        Name = name;
        City = city;
    }
}

public class Transaction
{
    public Trader Trader { get; set; }
    public int Year { get; set; }
    public int Value { get; set; }

    public Transaction(Trader trader, int year, int value)
    {
        Trader = trader;
        Year = year;
        Value = value;
    }
}

public class MainClass
{
    public static List<Transaction> transactions = new List<Transaction>
    {
        new Transaction(new Trader("Brian", "Cambridge"), 2011, 300),
        new Transaction(new Trader("Raoul", "Cambridge"), 2012, 1000),
        new Transaction(new Trader("Raoul", "Cambridge"), 2011, 400),
        new Transaction(new Trader("Mario", "Milan"), 2012, 710),
        new Transaction(new Trader("Mario", "Milan"), 2012, 700),
        new Transaction(new Trader("Alan", "Cambridge"), 2012, 950)
    };

    public static void Main(string[] args)
    {
        Console.WriteLine("1. 2011년에 일어난 모든 트랜잭션을 찾아 가격 기준 오름차순으로 정리하여 이름을 나열하시오.");
        transactions.Where(e => e.Year == 2011)
            .OrderBy(e => e.Value)
            .ToList()
            .ForEach(e => Console.WriteLine(e.Trader.Name));
        Console.WriteLine();
        
        Console.WriteLine("2. 거래자가 근무하는 모든 도시를 중복 없이 나열하시오");
        transactions.Select(e => e.Trader.City)
            .ToHashSet()
            .ToList()
            .ForEach(e => Console.WriteLine(e));
        Console.WriteLine();
        
        Console.WriteLine("3. 케임브리지에서 근무하는 모든 거래자를 찾아서 이름순으로 정렬하여 나열하시오");
        transactions.Where(e => e.Trader.City == "Cambridge")
            .Select(e => e.Trader.Name)
            .OrderBy(e => e.ToString())
            .ToHashSet()
            .ToList()
            .ForEach(e => Console.WriteLine(e));
        Console.WriteLine();
        
        Console.WriteLine("4. 모든 거래자의 이름을 알파벳순으로 정렬하여 나열하시오");
        transactions.Select(e=>e.Trader.Name)
            .OrderBy(e=>e.ToString())
            .ToHashSet()
            .ToList()
            .ForEach(e => Console.WriteLine(e));
        Console.WriteLine();
        
        Console.WriteLine("5. 밀라노에 거래자가 있는가?");
        transactions.Where(e => e.Trader.City == "Milan")
            .Select(e => e.Trader.Name)
            .ToHashSet()
            .ToList()
            .ForEach(e => Console.WriteLine(e));
        Console.WriteLine();
        
        Console.WriteLine("6. 케임브리지에 거주하는 거래자의 모든 트랙잭션값을 출력하시오");
        transactions
            .Where(e => e.Trader.City == "Cambridge")
            .ToList()
            .ForEach(e =>
                Console.WriteLine(
                    $"Name: {e.Trader.Name}, City: {e.Trader.City}, Year: {e.Year}, Price: {e.Value}"
                ));
        Console.WriteLine();
        
        Console.WriteLine("7. 전체 트랜잭션 중 최대값은 얼마인가?");
        int maxResult = transactions
            .Select(e => e.Value)
            .Aggregate((e, v) => Math.Max(e, v));

        Console.WriteLine(maxResult);

    }
}

```

`<문제 풀이 코드를 추가함>`

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용하지 않음>`
- 채택 또는 거절한 내용: `<AI 사용 거절>`
- 판단한 이유: `<DB 쿼리문의 작성할 수 있는 지식과 강의 자료를 통해 풀 수 있었다고 판단함>`
- 
## 4. 아직 궁금한 점

`<OrderBy 요구하지 않은 문제들도 기본적으로 OrderBy(이름)을 써야하는지 모름>`

## 5. 다음에 적용할 것

`<4번에 있는 조건을 해야한 다면 모든 문제 답을 OrderBy 를 넣어 수정>`
