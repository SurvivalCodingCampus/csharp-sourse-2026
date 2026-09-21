using System;

public class Pokemon : IEquatable<Pokemon>
{
    public int Id { get; }
    public string Name { get; }
    public int Height { get; }
    public int Weight { get; }
    public string FrontDefaultImageUrl { get; }

    public Pokemon(int id, string name, int height, int weight, string frontDefaultImageUrl)
    {
        Id = id;
        Name = name;
        Height = height;
        Weight = weight;
        FrontDefaultImageUrl = frontDefaultImageUrl;
    }

    public bool Equals(Pokemon? other)
    {
        if (other is null) return false;
        return Id == other.Id && Name == other.Name;
    }

    public override bool Equals(object? obj) => Equals(obj as Pokemon);

    public override int GetHashCode() => HashCode.Combine(Id, Name);

    public override string ToString()
    {
        return $"Pokemon {{ Id = {Id}, Name = {Name}, Height = {Height}, Weight = {Weight} }}";
    }
}