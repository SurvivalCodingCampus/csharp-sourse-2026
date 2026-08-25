namespace Day01_OOP_Review;

public class Cleric
{
    public const int MaxHp = 50;
    public const int MaxMp = 10;
    
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    
    private static readonly Random random = new Random();
    
    public Cleric(string name, int hp = MaxHp, int mp = MaxMp)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("이름이 없는 성직자는 생성할 수 없습니다.");
        }

        Name = name;
        Hp = Math.Min(hp, MaxHp);
        Mp = Math.Min(mp, MaxMp);
    }
    
    public void SelfAid()
    {
        if (Mp < 5)
        {
            Console.WriteLine("MP가 부족하여 SelfAid를 사용할 수 없습니다.");
            return;
        }

        Mp -= 5;
        Hp = MaxHp;
    }
    
    public int Pray(int seconds)
    {
        if (seconds <= 0) return 0;

        int recoverAmount = seconds + random.Next(0, 3); // 0, 1, 2 중 랜덤
        int actualRecovered = Math.Min(recoverAmount, MaxMp - Mp);

        Mp += actualRecovered;

        return actualRecovered;
    }
}