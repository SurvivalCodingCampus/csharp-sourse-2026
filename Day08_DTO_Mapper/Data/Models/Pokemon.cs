namespace Day08_DTO_Mapper.Data.Models;

public class Pokemon
{
    public string Name { get; }
    public List<string> Types { get; }

    public double Height { get; }
    public double Weight { get; }

    public int Attack { get; }
    public int Defense { get; }
    public int Speed { get; }
    public int SpecialAttack { get; }
    public int SpecialDefense { get; }

    public Pokemon(
        string name,
        List<string> types,
        double height,
        double weight,
        int attack,
        int defense,
        int speed,
        int specialAttack,
        int specialDefense)
    {
        Name = name;
        Types = types;
        Height = height;
        Weight = weight;
        Attack = attack;
        Defense = defense;
        Speed = speed;
        SpecialAttack = specialAttack;
        SpecialDefense = specialDefense;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Pokemon other)
        {
            return false;
        }

        return Name == other.Name &&
               Types.SequenceEqual(other.Types) &&
               Height == other.Height &&
               Weight == other.Weight &&
               Attack == other.Attack &&
               Defense == other.Defense &&
               Speed == other.Speed &&
               SpecialAttack == other.SpecialAttack &&
               SpecialDefense == other.SpecialDefense;
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(Name);
        hash.Add(Height);
        hash.Add(Weight);
        hash.Add(Attack);
        hash.Add(Defense);
        hash.Add(Speed);
        hash.Add(SpecialAttack);
        hash.Add(SpecialDefense);

        foreach (var type in Types)
        {
            hash.Add(type);
        }

        return hash.ToHashCode();
    }

    public override string ToString()
    {
        return
            $"Name: {Name}\n" +
            $"Types: {string.Join(", ", Types)}\n" +
            $"Height: {Height}m\n" +
            $"Weight: {Weight}kg\n" +
            $"Attack: {Attack}\n" +
            $"Defense: {Defense}\n" +
            $"Speed: {Speed}\n" +
            $"Special Attack: {SpecialAttack}\n" +
            $"Special Defense: {SpecialDefense}";
    }
}