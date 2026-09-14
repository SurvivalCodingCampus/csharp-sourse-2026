namespace Day06_Model_Repository.Models;

public class Item
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Count { get; set; } = 0;

    public Item(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Item(int id, string name, int count)
    {
        Id = id;
        Name = name;
        Count = count;
    }

    protected bool Equals(Item other)
    {
        return Id == other.Id && Name == other.Name && Count == other.Count;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Item)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Count);
    }
}