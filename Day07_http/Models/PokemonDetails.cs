namespace Day07_http.Models;

// ── sprites 관련 ──
public class Sprites
{
    public OtherSprites? Other { get; set; }
}

public class OtherSprites
{
    public PokemonSprites? OfficialArtwork { get; set; }
}

public class PokemonSprites
{
    public string? OfficialArtworkUrl { get; set; }
}


public class PokemonType
{
    public int Slot { get; set; }

    public PokemonTypeInfo? Type { get; set; }
}

public class PokemonTypeInfo
{
    public string? Name { get; set; }

    public string? Url { get; set; }
}
