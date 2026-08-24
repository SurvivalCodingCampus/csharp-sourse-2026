namespace Day01_OOP_Review;

public class Cleric
{
    private string Name { get; set; }
    private int HP { get; set; }
    private int MP { get; set; }
    
    public const int MaxHp = 50;
    public const int MaxMp = 10;
    public const int SelfAidCost = 5;

    public Cleric(string name, int hp, int mp)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.HP = hp >= MaxHp ? MaxHp : hp > 0 ? hp : 0;
        this.MP = mp >= MaxMp ? MaxMp : mp > 0 ? mp : 0;
    }

    public Cleric(string name, int hp)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.HP = hp >= MaxHp ? MaxHp : hp > 0 ? hp : 0;
        this.MP = MaxMp;
    }

    public Cleric(string name)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.HP = MaxHp;
        this.MP = MaxMp;
    }

    public int GetHp()
    {
        return this.HP;
    }

    public int GetMp()
    {
        return this.MP;
    }

    public void SelfAid()
    {
        if (this.MP < SelfAidCost)
        {
            return;
        }

        this.HP = MaxHp;
        this.MP -= SelfAidCost;
    }

    public int Pray(int seconds)
    {
        Random rnd = new Random();
        int restoreMp = rnd.Next(3) + seconds;
        
        if (this.MP + restoreMp > MaxMp)
        {
            restoreMp = MaxMp - this.MP;
            this.MP = MaxMp;
            return  restoreMp;
        }
        else
        {
            this.MP += restoreMp; 
            return restoreMp;
        }
    }
}