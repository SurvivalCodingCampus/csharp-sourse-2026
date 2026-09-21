using Day08_DTO_Mapper.Data.DTOs;

namespace Day08_DTO_Mapper.Data.Models;

public record Pokemon(int? Id, string Name, string ImageUrl, List<string> Types)
{
    public override string ToString()
    {
        return $"Id = {Id}, Name = {Name}, Sprites = {ImageUrl}, Types = {string.Join(", ", Types)}";
    }
}