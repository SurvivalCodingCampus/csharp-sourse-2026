namespace Day01_OOP_Review;

public class Cleric
{
    public string Name = "";
    public int Hp;
    public int Mp;
    private const int MaxHp = 50;
    private const int MaxMp = 10;
    
    public Cleric(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        Name = name;
        Hp = MaxHp;
        Mp = MaxMp;
    }

    public Cleric(string name, int hp)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        Name = name;
        Hp = hp;
        Mp = MaxMp;
    }
    
    public Cleric(string name, int hp, int mp)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        Name = name;
        Hp = hp;
        Mp = mp;
    }

    public void SelfAid()
    {
        if (Mp < 5)
        {
            Console.WriteLine("MP가 부족합니다");
            return;
        }
        Mp -= 5;
        Hp = MaxHp;
    }

    public int Pray(int seconds)
    {
        Random random = new Random();
        int praySec = seconds + random.Next(3);
        int previousMp = Mp;
        
        Mp += praySec;
        
        if (Mp > MaxMp)
        {
            Mp = MaxMp;
            Console.WriteLine("MP가 가득찼습니다");
        }
        
        int finalMp = Mp - previousMp;
        
        return finalMp;
    }
}