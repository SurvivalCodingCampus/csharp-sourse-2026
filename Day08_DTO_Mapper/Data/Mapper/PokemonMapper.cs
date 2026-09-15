using Day08_DTO_Mapper.Data.DTOs;
using Day08_DTO_Mapper.Data.Models;

namespace Day08_DTO_Mapper.Data.Mapper;

public static class PokemonMapper
{
    private const string DefaultName = "Unknown";
    private const int DefaultId = 0;
    private const int DefaultHeight = 0;
    private const int DefaultWeight = 0;

    /// <summary>
    /// DTO를 Domain Model(Pokemon)로 변환합니다.
    /// DTO가 null이거나 내부 데이터가 유효하지 않아도 기본값으로 처리하여 절대 예외를 던지지 않습니다.
    /// </summary>
    public static Pokemon ToDomain(this PokemonDto? dto)
    {
        // 1. DTO 객체 자체가 null인 경우 기본 모델 반환
        if (dto is null)
        {
            return CreateDefaultPokemon();
        }

        try
        {
            // 2. name 검사: null, 빈 문자열, 공백 문자열인 경우 "Unknown" 기본값 처리
            string validName = string.IsNullOrWhiteSpace(dto.Name)
                ? DefaultName
                : dto.Name.Trim();

            // 3. ID, Height, Weight: 음수 값이 들어온 경우 0으로 보정
            int validId = dto.Id > 0 ? dto.Id : DefaultId;
            int validHeight = dto.Height >= 0 ? dto.Height : DefaultHeight;
            int validWeight = dto.Weight >= 0 ? dto.Weight : DefaultWeight;

            // 4. Sprites 이미지 안전 추출
            string? validImageUrl = string.IsNullOrWhiteSpace(dto.Sprites?.FrontDefault)
                ? null
                : dto.Sprites.FrontDefault.Trim();

            // 5. Types 목록 안전 변환: null 슬롯이나 비어있는 이름 제외 후 리스트 생성
            var validTypes = dto.Types?
                .Where(slot => slot?.Type != null && !string.IsNullOrWhiteSpace(slot.Type.Name))
                .Select(slot => slot.Type.Name.Trim())
                .ToList() ?? new List<string>();

            return new Pokemon
            {
                Id = validId,
                Name = validName,
                HeightDecimeter = validHeight,
                WeightHectogram = validWeight,
                ImageUrl = validImageUrl,
                Types = validTypes
            };
        }
        catch
        {
            // 예기치 못한 런타임 오류 발생 시에도 게임 루프가 중단되지 않도록 기본 객체 반환
            return CreateDefaultPokemon();
        }
    }

    private static Pokemon CreateDefaultPokemon()
    {
        return new Pokemon
        {
            Id = DefaultId,
            Name = DefaultName,
            HeightDecimeter = DefaultHeight,
            WeightHectogram = DefaultWeight,
            ImageUrl = null,
            Types = Array.Empty<string>()
        };
    }
}