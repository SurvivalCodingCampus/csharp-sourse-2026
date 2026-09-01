namespace Day02_lambda;

class Program
{
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
            Console.WriteLine("1.========");
            transactions.Where(e => e.Year == 2011)
                .OrderBy(e => e.Value)
                .ToList()
                .ForEach(e => Console.WriteLine(e.Trader.Name));

            Console.WriteLine("2.========");
            transactions.Select(e => e.Trader.City)
                .Distinct()
                .ToList()
                .ForEach(city => Console.WriteLine(city));

            Console.WriteLine("3.========");
            transactions.Select(e => e.Trader)
                .Where(trader => trader.City == "Cambridge")
                .DistinctBy(trader => trader.Name)
                .ToList()
                .ForEach(trader => Console.WriteLine(trader.Name));

            Console.WriteLine("4.========");
            transactions.Select(e => e.Trader.Name)
                .Distinct()
                .OrderBy(name => name)
                .ToList()
                .ForEach(name => Console.WriteLine(name));

            Console.WriteLine("5.========");
            bool hasMilanTrader = transactions.Any(e => e.Trader.City == "Milan");
            Console.WriteLine(hasMilanTrader ? "밀라노에 거래자 있음" : "밀라노에 거래자 없음");

            Console.WriteLine("6.========");
            transactions.Where(e => e.Trader.City == "Cambridge")
                .Select(e => e.Value)
                .ToList()
                .ForEach(val => Console.WriteLine(val));

            Console.WriteLine("7.========");
            int maxValue = transactions.Max(e => e.Value);
            Console.WriteLine($"최댓값:{maxValue}");

            Console.WriteLine("8.========");
            int minValue = transactions.Min(e => e.Value);
            Console.WriteLine($"최솟값 : {minValue}");
        }
    }
}