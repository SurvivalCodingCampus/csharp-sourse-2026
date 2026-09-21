namespace Day07_http_WithAI.Models;

using Newtonsoft.Json;

//과제2

// public class Pokemon
// {
//     [JsonProperty("name")]
//     public string? Name { get; set; }
//
//     [JsonProperty("sprites")]
//     public PokemonSprites? Sprites { get; set; }
// }
public record Pokemon(string Name, string ImageUrl);