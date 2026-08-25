namespace Day01_OOP_Review;

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
        Console.WriteLine("1. 2011년에 일어난 모든 트랜잭션을 찾아 가격 기준 오름차순으로 정리하여 이름을 나열");
        transactions.Where(e=> e.Year == 2011)
            .OrderBy(e=>e.Value)
            .ToList()
            .ForEach(e=>Console.WriteLine(e.Trader.Name));
        Console.WriteLine(" ");
        
        
        Console.WriteLine("2. 거래자가 근무하는 모든 도시를 중복 없이 나열");
        transactions.Select(e => e.Trader.City)
            .ToHashSet()
            .ToList()
            .ForEach(e=>Console.WriteLine(e));
        Console.WriteLine(" ");
        
        
        Console.WriteLine("3. 케임브리지에서 근무하는 모든 거래자를 찾아서 이름순으로 정렬하여 나열");
        transactions.Where(e => e.Trader.City == "Cambridge")
            .OrderBy(e=>e.Trader.Name)
            .Select(e => e.Trader.Name)
            .ToHashSet() //💣⭐select와 세트(Distinct | ToHashSet)이므로 같이 사용 
            .ToList() //ToList(e => e % 2 ==0).ToList()
            .ForEach(e =>Console.WriteLine(e));
        Console.WriteLine(" ");
        
        
        Console.WriteLine("4. 모든 거래자의 이름을 알파벳순으로 정렬하여 나열");
        transactions.OrderBy(e => e.Trader.Name)
            .DistinctBy(e => e.Trader.Name)//select가 있을때 ToHashSet, Distinct를 씀
            .ToList() //중복없는 리스트업 
            .ForEach(e => Console.WriteLine(e.Trader.Name)); //for문
        Console.WriteLine(" ");
        
        
        Console.WriteLine("5. 밀라노에 거래자가 있는가?");
        Console.WriteLine(transactions.Any(e => e.Trader.City == "Milan"));
        Console.WriteLine(" ");
            
        
        Console.WriteLine("6. 케임브리지에 거주하는 거래자의 모든 트랙잭션값을 출력");
        transactions.Where(e=>e.Trader.City == "Cambridge")
            .Select(e => e.Value)
            .ToList()
            .ForEach(e=>Console.WriteLine(e));
        Console.WriteLine(" ");
        
        
        Console.WriteLine("7. 전체 트랜잭션 중 최대값은 얼마?");
        transactions.MaxBy(e => e.Value);
        Console.WriteLine(transactions.Max(e => e.Value));
        Console.WriteLine(" ");
        
        
        
        Console.WriteLine("8. 전체 트랜잭션 중 최소값은 얼마?");
        transactions.MinBy(e => e.Value);
        Console.WriteLine(transactions.Min(e => e.Value));
        Console.WriteLine(" ");

    }
}
