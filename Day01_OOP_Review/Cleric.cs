using System.Runtime.InteropServices.Swift;

namespace Day01_OOP_Review;

public class Cleric
{
    public string name = "Cleric";
    private static int maxHp = 50;
    public int Hp;
    private static int maxMp = 10;
    public int Mp;
    

    

    public void selfAid()
    {
        Mp -= 5;
        Hp = maxHp;
    }

    public int pray(int seconds)
    {
        Hp += (seconds + Math.Max(0,2));
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