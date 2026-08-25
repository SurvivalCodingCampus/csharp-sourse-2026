namespace Day02_Funtions;

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
        Console.WriteLine("1. =============");
        transactions.Where((e => e.Year == 2011))
            .OrderBy(e => e.Value)
            .ToList()
            .ForEach(e => Console.WriteLine(e.Trader.Name));
        
        Console.WriteLine("2. =============");
        transactions.Select(e => e.Trader.City)
            .Distinct()
            .ToList()
            .ForEach(e => Console.WriteLine(e));
        
        Console.WriteLine("3. =============");
        transactions.Where(e => e.Trader.City == "Cambridge")
            .Select(e => e.Trader.Name)
            .OrderBy(e => e)
            .Distinct()
            .ToList()
            .ForEach(e => Console.WriteLine(e));
        
        Console.WriteLine("4 =============");
        transactions.Select(e => e.Trader.Name)
            .OrderBy(e => e)
            .Distinct()
            .ToList()
            .ForEach(e => Console.WriteLine(e));
        
        Console.WriteLine("5 =============");
        Console.WriteLine(transactions.Any(e => e.Trader.City == "Milan"));
        
        Console.WriteLine("6 =============");
        transactions.Where(e=>e.Trader.City=="Cambridge")
            .Select(e=>e.Value)
            .ToList()
            .ForEach(e => Console.WriteLine(e));
        
        Console.WriteLine("7 =============");
        int MaxValue = transactions.Select(e => e.Value)
            .Aggregate((e, value) => Math.Max(e, value));
            Console.WriteLine(MaxValue);
            
            Console.WriteLine("8 =============");
            int MinValue = transactions.Select(e => e.Value)
                .Aggregate((e, value) => Math.Min(e, value));
            Console.WriteLine(MinValue);
        
    }
}
