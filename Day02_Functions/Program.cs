namespace Day02_Functions;

class Program
{
    // delegate 함수를 저장할 타입을 선언 + 타입 Safety
    public delegate int MyDelegate(int a, int b);
    
    // static void Main(string[] args)
    // {
    //
    //     var list = new List<int> { 1, 2, 3, 4, 5 };
    //     // 함수형 프로그래밍 : 실제 수행하는 일에 집중
    //     list.ForEach(e =>
    //         {
    //             Console.Write(" ");
    //             Console.WriteLine(e);
    //         }
    //         );
    //     // list.ForEach(Console.WriteLine);
    //     
    //     List<int>list2 = list.Where(e => e % 2 == 0).ToList();
    //     list.Where(e => e % 2 == 0).Select(e => $"숫자 {e}").ToList().ForEach(Console.WriteLine);
    //     
    //     Console.WriteLine("Hello, World!");
    //
    //     // 함수 전달할 인자 1, 인자 2 , 반환타입
    //     Func<int, int, int> addFunc = (a, b) => a + b;
    //     
    //     int result = addFunc(1, 2);
    //     Console.WriteLine(result);
    //
    //     MyDelegate myDelegate = (a, b) => a + b;
    //     Console.WriteLine(myDelegate(1, 2));
    // }
}

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
        // 여기에 푸세요
        Console.WriteLine("===========1==========");
        // 1. 2011년에 일어난 모든 트랜잭션을 찾아 가격 기준 오름차순으로 정리하여 이름을 나열하시오
        transactions.Where(e => e.Year == 2011).
            OrderBy(e => e.Value).
            ToList().
            ForEach(e => Console.WriteLine(e.Trader.Name));
        
        Console.WriteLine("===========2==========");
        
        // 2. 거래자가 근무하는 모든 도시를 중복 없이 나열하시오
        transactions.Select(e => e.Trader.City).ToHashSet().ToList().ForEach(e => Console.WriteLine(e));
        Console.WriteLine("===========3==========");
        
        // 3. 케임브리지에서 근무하는 모든 거래자를 찾아서 이름순으로 정렬하여 나열하시오
        transactions.Where(e => e.Trader.City == "Cambridge").
            OrderBy(e => e.Trader.Name).
            Select(e => e.Trader.Name).
            ToHashSet().
            ToList().
            ForEach(e => Console.WriteLine(e));
        Console.WriteLine("===========4==========");
        
        // 4. 모든 거래자의 이름을 알파벳순으로 정렬하여 나열하시오
        transactions.Select(e => e.Trader.Name).
            OrderBy(e => e).
            ToHashSet().
            ToList().
            ForEach(e => Console.WriteLine(e));
        Console.WriteLine("===========5==========");
        
        // 5. 밀라노에 거래자가 있는가?
        Console.WriteLine(transactions.Where(e => e.Trader.City == "Milan").ToList().Count > 0);
        
        Console.WriteLine("===========6==========");
        
        // 6. 케임브리지에 거주하는 거래자의 모든 트랙잭션값을 출력하시오
        transactions.Where(e => e.Trader.City == "Cambridge").
            ToList().
            ForEach(
                e => Console.WriteLine(
                    "Name : " + e.Trader.Name + 
                    " | City : " + e.Trader.City + 
                    " | Year : " + e.Year + 
                    " | Value : " + e.Value)
                );
        Console.WriteLine("===========7==========");
        
        // 7. 전체 트랜잭션 중 최대값은 얼마인가?
        Console.WriteLine(transactions.Select(e => e.Value).Aggregate((e, v) => Math.Max(e, v)));
        Console.WriteLine("===========8==========");

        // 8. 전체 트랜잭션 중 최소값은 얼마인가?
        Console.WriteLine(transactions.Select(e => e.Value).Aggregate((e, v) => Math.Min(e, v)));
        Console.WriteLine("=====================");

    }
}



