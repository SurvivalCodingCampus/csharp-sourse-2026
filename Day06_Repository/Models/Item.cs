namespace Day06_Repository.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }

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

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Count)}: {Count}";
    }
}