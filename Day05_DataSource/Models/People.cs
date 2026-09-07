namespace Day05_DataSource.Models;

public class People
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    
    public People(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public People()
    {
        
    }

    protected bool Equals(People other)
    {
        return Name == other.Name && Age == other.Age;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((People)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }
}