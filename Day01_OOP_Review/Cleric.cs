namespace Day01_OOP_Review;

public class Cleric
{
    public string Name { get; set;}
    public int Hp { get; set; }
    public int Mp { get; set; }
    public const int MaxHp=50;
    public const int MaxMp = 10;

    public Cleric(string name)
    {
        Name = name;
        Hp = MaxHp;
        Mp = MaxMp;
    }
    public void SelfAid()
    {
        Mp -= 5;
        Hp = MaxHp;
    }
    public int Pray(int second)
    {
        int beforeMp = Mp;
        Random random = new Random();
        
        int plusMp = random.Next(0, 3);
        Mp += second+plusMp;
        
        if (Mp >= MaxMp) Mp = MaxMp;
        return Mp - beforeMp;
    }
}
