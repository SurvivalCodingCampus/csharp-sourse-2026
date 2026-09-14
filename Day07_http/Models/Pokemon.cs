using System.Text.Json.Serialization;

namespace Day07_http.Models;

public class Pokemon
{
    [JsonProperty("name")] //어트리뷰트 변경
    public string? Name { get; set; }
    
    [JsonPropertyName("sprites")] // 어트리뷰트 변경
    public OtherSprites? Sprites { get; set; }
}

public class JsonPropertyAttribute : Attribute
{
    public JsonPropertyAttribute(string name)
    {
        throw new NotImplementedException();
    }
}