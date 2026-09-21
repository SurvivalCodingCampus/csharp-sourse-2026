namespace Day08_DTO_Mapper;

public class Pokemon(int id, string name, List<string> types, Dictionary<string, int> stats, string officialArtworkUrl)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public List<string> Types { get; set; } = types;
    public Dictionary<string, int> Stats { get; set; } =  stats;
    public string? OfficialArtworkUrl { get; set; } = officialArtworkUrl;

    public override bool Equals(object? obj)
    {
        if (obj is not Pokemon other) return false;
        return Id == other.Id && Name == other.Name;
    }

    public override int GetHashCode() => HashCode.Combine(Id, Name);

    public override string ToString() => $"[#{Id}] {Name} (Types: {string.Join(", ", Types)})";
}