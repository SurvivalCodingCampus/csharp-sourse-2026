using Newtonsoft.Json;

namespace Day07_Network.Models;

public class Pokemon
{
    [JsonProperty("name")] 
    public string? Name { get; set; }

    [JsonProperty("id")] 
    public int Id { get; set; }

    [JsonProperty("sprites")]
    public PokemonSprites? Sprites { get; set; }
    
    [JsonProperty("types")] 
    public Types[]? Types { get; set; }
    
    
}

public class PokemonSprites
{
    [JsonProperty("other")]
    public OtherSprites? Other { get; set; }
}

public class OtherSprites
{
    [JsonProperty("official-artwork")]
    public OfficialArtwork? OfficialArtwork { get; set; }
}

public class OfficialArtwork
{
    [JsonProperty("front_default")]
    public string? FrontDefault { get; set; }
}

public class Types
{
    [JsonProperty("type")]
    public Type type { get; set; }
}

public class Type
{
    [JsonProperty("name")]
    public string? TypeName { get; set; }
}