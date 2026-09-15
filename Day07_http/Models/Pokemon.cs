using System.Text.Json.Serialization;

namespace Day07_http.Models;

public class Pokemon
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name {get; set;}
    [JsonPropertyName("sprites")]
    public OtherSprites? Sprites {get; set;}
}