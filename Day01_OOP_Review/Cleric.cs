namespace Day01_OOP_Review;

public class Cleric
{
<<<<<<< HEAD
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
=======
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
>>>>>>> aad6bf3de8b77354ca700abfb18af1e52b980af9
