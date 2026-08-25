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
    public static List<Transaction> Transactions = new List<Transaction>
    {
        new Transaction(new Trader("Brian", "Cambridge"), 2011, 300),
        new Transaction(new Trader("Raoul", "Cambridge"), 2012, 1000),
        new Transaction(new Trader("Raoul", "Cambridge"), 2011, 400),
        new Transaction(new Trader("Mario", "Milan"), 2012, 710),
        new Transaction(new Trader("Mario", "Milan"), 2012, 700),
        new Transaction(new Trader("Alan", "Cambridge"), 2012, 950),

    };

    public static void Main(string[] args)
    {
        Console.WriteLine("1. 2011년 거래 가격기준 asc");
        var query1 = Transactions
            .Where(t => t.Year == 2011)
            .OrderBy(t => t.Value)
            .Select(t => t.Trader.Name);
        foreach (var name in query1)
            Console.WriteLine(name);
        
        Console.WriteLine("\n2. 도시 목록 distinct");
        var query2 = Transactions
            .Select(t => t.Trader.City)
            .Distinct();
        foreach (var city in query2)
            Console.WriteLine(city);
        
        Console.WriteLine("\n3. 케임브리지 거래자 이름순 asc");
        var query3 = Transactions
            .Where(t => t.Trader.City == "Cambridge")
            .Select(t => t.Trader.Name)
            .Distinct()
            .OrderBy(name => name);
        foreach (var name in query3)
            Console.WriteLine(name);
        
        Console.WriteLine("\n4. 전체 거래자 이름 asc");
        var query4 = Transactions
            .Select(t => t.Trader.Name)
            .Distinct()
            .OrderBy(name => name);
        foreach (var name in query4)
            Console.WriteLine(name);
        
        Console.WriteLine("\n5. 밀라노 거래자 유무");
        bool query5 = Transactions.Any(t => t.Trader.City == "Milan");
        Console.WriteLine(query5);
        
        Console.WriteLine("\n6. 케임브리지 거래자의 트랜잭션 값");
        var query6 = Transactions
            .Where(t => t.Trader.City == "Cambridge")
            .Select(t => t.Value);
        foreach (var value in query6)
            Console.WriteLine(value);
        
        Console.WriteLine("\n7. 트랜잭션 최대값");
        int query7 = Transactions.Max(t => t.Value);
        Console.WriteLine(query7);

        Console.WriteLine("\n8. 트랜잭션 최소값");
        int query8 = Transactions.Min(t => t.Value);
        Console.WriteLine(query8);

    }

}