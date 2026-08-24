namespace Day01_OOP_Review;

public class Cleric
{
    public string Name;
    public int Hp;
    public const int MaxHp = 50;
    public int Mp;
    public const int MaxMp = 10;

    public Cleric(string name, int hp, int mp)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("You cannot create nameless 'Cleric' class");
        }

        Name = name;
        Hp = hp;
        Mp = mp;
    }

    public void SelfAid()
    {
        Mp -= 5;
        Hp = MaxHp;
    }

    public int Pray(int seconds)
    {
        Random rand = new Random();
        int recoverMp = seconds + rand.Next(3);

        if (Mp + recoverMp > MaxMp)
        {
            recoverMp = MaxMp - Mp;
        }

        Mp += recoverMp;
        return recoverMp;
    }
}