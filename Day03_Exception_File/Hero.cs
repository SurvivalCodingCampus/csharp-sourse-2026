namespace Day03_Exception_File;

public class Hero
{
    public string Name { get; }
    public int Hp { get; }
    
    public Hero(string name, int hp)
    {
        Name = name;
        Hp = hp;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Hp)}: {Hp}";
    }
}