namespace Day08_DTO_Mapper.Data.Models;

public class Pokemon
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public double HeightMeter => HeightDecimeter / 10.0;
    public double WeightKg => WeightHectogram / 10.0;
    public int HeightDecimeter { get; init; }
    public int WeightHectogram { get; init; }
    public string? ImageUrl { get; init; }
    public IReadOnlyList<string> Types { get; init; } = Array.Empty<string>();
}