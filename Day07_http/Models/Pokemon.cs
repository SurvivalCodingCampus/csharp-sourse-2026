namespace Day07_http.Models;

public class Pokemon
{
    public string? Name { get; set; }

    public Sprites? Sprites { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public List<PokemonType>? Types { get; set; }
}
