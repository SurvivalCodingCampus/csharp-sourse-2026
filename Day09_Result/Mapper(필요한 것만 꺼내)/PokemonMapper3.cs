using Day09_Result_Pattern.Data.DTO;
using Day09_Result_Pattern.Data.Models;

namespace Day09_Result.Mapper_필요한_것만_꺼내_;

public static class PokemonMapper {
    public static Pokemon3 ToModel(PokemonDto3 dto3) {
        return new Pokemon3(dto3.Id ?? 0, dto3.Name ?? "", dto3.Height ?? 0, dto3.Weight ?? 0);
    }
}