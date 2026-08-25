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
        // 1. 2011년에 일어난 모든 트랜잭션을 찾아 가격 기준 오름차순으로 정리하여 이름을 나열하시오
        
        Console.WriteLine("1. =======================\n");
        transactions.Where(e => e.Year == 2011)
            .OrderBy(e => e.Value)
            .ToList()
            .ForEach(e => Console.WriteLine(e.Trader.Name));
        
        Console.WriteLine("2. =======================\n");
        transactions.Select(e => e.Trader.City)
            .Distinct()
            .ToList()
            .ForEach(city => Console.WriteLine(city));
        
        Console.WriteLine("3. =======================\n");
        transactions.Where(e => e.Trader.City == "Cambridge")
            .Select(e => e.Trader.Name)
            .Distinct()
            .OrderBy(name => name)
            .ToList()
            .ForEach(Console.WriteLine);
        
        Console.WriteLine("4. =======================\n");
        transactions.Select(e => e.Trader.Name)
            .Distinct()
            .OrderBy(name => name)
            .ToList()
            .ForEach(Console.WriteLine);
        
        Console.WriteLine("5. =======================\n");
        if (transactions.Any(e => e.Trader.City == "Milan"))
        {
            Console.WriteLine("존재한다");
        }
        else
        {
            Console.WriteLine("존재하지 않는다");
        }
        
        Console.WriteLine("6. =======================\n");
        transactions.Where(e => e.Trader.City == "Cambridge")
            .OrderBy(e => e.Value)
            .ToList()
            .ForEach(e => Console.WriteLine(e.Value));
        
        Console.WriteLine("7. =======================\n");
        int maxValue = transactions.Select(e => e.Value)
                        .Aggregate((e, value) => Math.Max(e, value));
        
        Console.WriteLine(maxValue);
        
        Console.WriteLine("8. =======================\n");
        int minValue = transactions.Select(e => e.Value)
                        .Aggregate((e, value) => Math.Min(e, value));
        
        Console.WriteLine(minValue);
        
    }
}



class Program
{
    // delegate 함수를 저장할 타입을 선언 + 타입 Safety
    public delegate int MyDelegate(int a, int b);
    
    // static void Main(string[] args)
    // {
    //     var list = new List<int> {1, 2, 3, 4, 5};
    //
    //     List<int> list2 = list.Where(e => e % 2 == 0).ToList();
    //
    //     // 전통적인 뺑뺑이
    //     for (int i = 0; i < list.Count; i++)
    //     {
    //         Console.WriteLine(list[i]);
    //     }
    //
    //     foreach (var item in list)
    //     {
    //         Console.WriteLine(item);
    //     }
    //     
    //     // 함수형 프로그래밍 : 실제 수행하는 일에 집중
    //     list.ForEach(e =>
    //     {
    //         Console.Write(" ");
    //         Console.WriteLine(e);
    //     });
    //     list.ForEach(Console.WriteLine);
    //     
    //     Console.WriteLine("Hello, World!");
    //     
    //     // 함수에 전달할 인자1, 인자2, 리턴타입
    //     Func<int, int, int> addFunc = (a, b) => a + b;
    //     
    //     int result = addFunc(1, 2);
    //     Console.WriteLine(result);
    //
    //     MyDelegate myDelegate = (a, b) => a + b;
    //     Console.WriteLine(myDelegate(1, 2));
    // }

    int Add(int a, int b) => a + b;
    
}