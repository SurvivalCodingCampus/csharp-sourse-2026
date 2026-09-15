public static class PokemonMapper
{
    public static Pokemon ToModel(this PokemonDto? dto)
    {
        // DTO 자체가 통째로 null일 경우 방어
        if (dto == null)
        {
            return new Pokemon(0, "Unknown", 0, 0, string.Empty);
        }

        // 이름이 비어있거나 유효하지 않은 경우 기본값 처리
        string validName = string.IsNullOrWhiteSpace(dto.Name) ? "Unknown" : dto.Name;
        int validId = dto.Id ?? 0;
        int validHeight = dto.Height ?? 0;
        int validWeight = dto.Weight ?? 0;
        string validImageUrl = dto.Sprites?.FrontDefault ?? string.Empty;

        return new Pokemon(
            id: validId,
            name: validName,
            height: validHeight,
            weight: validWeight,
            frontDefaultImageUrl: validImageUrl
        );
    }
}