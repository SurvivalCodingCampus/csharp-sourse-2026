namespace Day01_OOP_Review;

public class Cleric
{
    const int MaxHp = 50;
    const int MaxMp = 10;
    
    public string Name { get; private set; }
    public int Hp = MaxHp;
    public int Mp = MaxMp;

    public Cleric(string name, int hp, int mp)
    {
        Name = name;
        Hp = hp;
        Mp = mp;
    }

    public Cleric(string name, int hp)
    {
        Name = name;
        Hp = hp;
    }
    
    public Cleric(string name)
    {
        Name = name;
    }

    public int Pray(int seconds)
    {
        if (Mp < 1)
        {
            return 0;
        }
        // 0 ~ 2
        int recoveryMp = seconds + Random.Shared.Next(3);
        Mp = Math.Min(Mp + recoveryMp, MaxMp);
        
        return recoveryMp;
    }

    public void SelfAid()
    {
        if (Mp < 5)
        {
            throw new Exception("Not enough MP");
        }
        Mp -= 5;
        Hp = MaxHp;
    }
}