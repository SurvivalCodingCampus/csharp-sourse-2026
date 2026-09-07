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

    protected bool Equals(Hero other)
    {
        return Name == other.Name && Hp == other.Hp;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Hero)obj);
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Hp);
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Hp)}: {Hp}";
    }
}