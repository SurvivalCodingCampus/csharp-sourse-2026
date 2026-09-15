namespace Day07_Pokemon;

public class Pokemon
{
    public int  Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double HeightMeter => HeightDecimeter / 10.0;
    public double WeightKg => WeightHectogram / 10.0;
    public int HeightDecimeter { get; init; } //객체 초기화 블록 { ... } 허용 + 이후 수정 불가
    public int WeightHectogram { get; init; }
    public string? ImageUrl { get; init; }
    public IReadOnlyList<string> Types { get; init; } = Array.Empty<string>(); //Types가 null이 되지 않도록 빈 배열로 초기화
}