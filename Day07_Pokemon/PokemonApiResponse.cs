using System.Text.Json.Serialization;

namespace Day07_Pokemon;

public class PokemonApiResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")] 
    public string Name { get; set; } = string.Empty;
    //string.Empty : 빈 문자열("")을 나타내는 정적 읽기 전용 필드
    
    [JsonPropertyName("height")]
    public int Height { get; set; }
    
    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("sprites")] 
    public PokemonSprites Sprites { get; set; } = new();
    
    [JsonPropertyName("types")]
    public List<PokemonTypeSlot> Types { get; set; } = new();
}

public class PokemonSprites
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
}

public class PokemonTypeSlot
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")] 
    public NamedApiResource Type { get; set; } = new();
}

public class NamedApiResource
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}