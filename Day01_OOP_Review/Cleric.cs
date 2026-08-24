namespace Day01_OOP_Review;

class Cleric
{
    private const int MaxHp = 50;
    private const int MaxMp = 10;

    public string Name { get; private set; }
    private int currentHp;
    private int currentMp;

    public Cleric(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("성직자는 이름을 가지고 생성되어야 합니다.");
        }
        this.Name = name;
        this.currentHp = MaxHp; 
        this.currentMp = MaxMp; 
    }

    public void SelfAid()
    {
        if (currentMp >= 5)
        {
            currentMp -= 5;
            currentHp = MaxHp; 
        }
        else
        {
            Console.WriteLine($"{Name}은(는) SelfAid를 할 MP가 부족합니다.");
        }
    }

    public int Pray(int seconds)
    {
        
        int mpToRestore = seconds + (seconds % 3); 
        if (currentMp + mpToRestore > MaxMp)
        {
            mpToRestore = MaxMp - currentMp; 
        }

        currentMp += mpToRestore;
        return mpToRestore;
    }
    
    public int Pray()
    {
        return Pray(1); 
    }


    public void DisplayStatus()
    {
        Console.WriteLine($"이름: {Name}, HP: {currentHp}/{MaxHp}, MP: {currentMp}/{MaxMp}");
    }
}