namespace Day01_OOP_Review;

public class Cleric
{
    private const int MaxHp = 50;
    private const int MaxMp = 10;
    private const int SelfAidMp = 5;
 
    public string Name { get; }
    public int Hp { get; set; } = MaxHp;
    public int Mp { get; set; } = MaxMp;
    
    public Cleric(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("이름은 필수입니다.");
        }

        Name = name;
    }
        
    public void SelfAid()
    {
        if (Mp < SelfAidMp)
        {
            Console.WriteLine("Mp 부족");
            return;
        }
        Mp -= SelfAidMp;
        Hp = MaxHp;
    }
    
    public int Pray(int seconds)
    {
        int beforeMp = Mp;
        int prayAmount = seconds + Random.Shared.Next(0, 3);

        Mp += prayAmount;
        if (Mp > MaxMp)
        {
            Mp = MaxMp;
        }

        return Mp - beforeMp;
    }
}