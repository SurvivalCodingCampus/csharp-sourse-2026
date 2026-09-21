using Day09_Result_패턴.Data.DTOs;
using Day09_Result_패턴.Data.Models;

namespace Day09_Result_패턴.Data.Mapper;

public static class PokemonMapper
{
     public static Pokemon ToModel(this PokemonDto dto)
     {
          return new Pokemon(
               Name: dto.Name ?? "NO NAME",
               ImageUrl: dto.Sprites?.FrontDefault ?? "");
     }
     
     //AI: dto == null일 때 상황 보완
     // public static Pokemon ToModel(this PokemonDto dto)
     // {
     //      if (dto == null)
     //      {
     //           return new Pokemon(
     //                "NO NAME",
     //                ""
     //           );
     //      }
     //
     //      return new Pokemon(
     //           string.IsNullOrWhiteSpace(dto.Name)
     //                ? "NO NAME"
     //                : dto.Name.Trim(),
     //
     //           string.IsNullOrWhiteSpace(dto.Sprites?.FrontDefault)
     //                ? ""
     //                : dto.Sprites.FrontDefault
     //      );
     // }
}