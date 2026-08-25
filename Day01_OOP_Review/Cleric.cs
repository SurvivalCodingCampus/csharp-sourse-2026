using System.Runtime.InteropServices.Swift;

namespace Day01_OOP_Review;

public class Cleric
{
    public string name = "Cleric";
    private const int maxHp = 50;
    public int Hp;
    private const int maxMp = 10;
    public int Mp;
    

    

    public void selfAid()
    {
        Mp -= 5;
        Hp = maxHp;
    }

    public int pray(int seconds)
    {
        int revoveryMp = (seconds + Random.Shared.Next(3));
        Mp += revoveryMp;
        if (Mp < 1)
        {
            return 0;
        }
        if (Mp >= maxMp)
        {
            Mp = maxMp;
        }
        return Mp;
    }

    public Cleric(string name, int hp, int mp)
    {
        this.name = name;
        this.Hp = hp;
        this.Mp = mp;
    }
}