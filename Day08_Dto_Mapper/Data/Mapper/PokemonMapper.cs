

using Day08_Dto_Mapper.Data.DTOs;
using Day08_Dto_Mapper.Data.Models;

public static class PokemonMapper{
    public static Pokemon2 ToModel(this PokemonDto dto) {
        //3. 게임이 어떠한 경우에도 중단되지 않도록 하시오
        if (dto == null) {
            dto.Name = "이름_영원히안끝나";
            dto.Sprites!.FrontDefault = "영원히안_끝나는_게임이미지";
        }


        //2. 유효하지 않은 데이터는 기본값으로 처리하시오
        try {
            //1. name이 비어있지 않은지 검사 
            if (dto.Name == null) {
                dto.Name = "이름_영원히안끝나";
            }

            return new Pokemon2(
                Name: dto.Name!,
                ImageUrl: dto.Sprites!.FrontDefault!);
        }
        catch (Exception) {
            dto.Name = "이름_영원히안끝나";
            dto.Sprites!.FrontDefault = "영원히안_끝나는_게임이미지";
            return default;
        }
    }
}



/*


try-catch안에는 
    return값을 넣는 이유: 예외 처리를 위함
    throw new Exception();을 해도 된다

*/