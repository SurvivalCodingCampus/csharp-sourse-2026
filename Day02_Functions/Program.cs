namespace Day2_Fuctions;

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
        // 1. 2011년에 일어난 모든 트랜잭션을 찾아 가격 기준 오름차순으로 정리하여 이름을 나열하시오
        transactions.Where(e => e.Year == 2011)
            .OrderBy(e => e.Value)
            .Select(e => e.Trader.Name)
            .ToList()
            .ForEach(Console.WriteLine);
        // 2. 거래자가 근무하는 모든 도시를 중복 없이 나열하시오
        transactions.Where(e => e.Trader.Name != null && !string.IsNullOrEmpty(e.Trader.City))
            .Select(e => e.Trader.City)
            .ToHashSet()
            .ToList()
            .ForEach(Console.WriteLine);

        // 3. 케임브리지에서 근무하는 모든 거래자를 찾아서 이름순으로 정렬하여 나열하시오
        transactions.Where(e => e.Trader.City == "Cambridge" && !string.IsNullOrEmpty(e.Trader.Name))
            .Select(e => e.Trader.Name)
            .ToHashSet()
            .OrderBy(name => name)
            .ToList()
            .ForEach(Console.WriteLine);

        // 4. 모든 거래자의 이름을 알파벳순으로 정렬하여 나열하시오
        transactions.Where(e => e.Trader.City != null && !string.IsNullOrEmpty(e.Trader.Name))
            .Select(e => e.Trader.Name)
            .ToHashSet()
            .OrderBy(name => name)
            .ToList()
            .ForEach(Console.WriteLine);
        // 5. 밀라노에 거래자가 있는가?
        bool result = transactions.Any(e => e.Trader.City == "Milan");
        Console.WriteLine(result);
        // 6. 케임브리지에 거주하는 거래자의 모든 트랙잭션값을 출력하시오
        transactions.Where(e => e.Trader.City == "Cambridge")
            .Select(e =>  e.Value)
            .ToList()
            .ForEach(Console.WriteLine);
        // 7. 전체 트랜잭션 중 최대값은 얼마인가?
        int maxResult = transactions
            .Select(e => e.Value)
            .Aggregate((max, next) => Math.Max(max, next));
        
        Console.WriteLine(maxResult);
        
        // 8. 전체 트랜잭션 중 최소값은 얼마인가?
        int minResult = transactions
            .Select(e => e.Value)
            .Aggregate((min, next) => Math.Min(min, next));
        
        Console.WriteLine(minResult);
    }

}

