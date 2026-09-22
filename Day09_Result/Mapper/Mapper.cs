
namespace Day08_DTO_Mapper.Mapper;

public static class Mapper
{
    //DTO에서 Model로 데이터 넘기기
    //name이 비어있지 않은지 검사
    //유효하지 않은 데이터는 기본값으로 처리하시오
    //게임이 어떠한 경우에도 중단되지 않도록 하시오
    
    //공부용 주석 처리
    //static은 객체를 만들지 않고 사용(DTO에서 Model로 넘기는 게 주 역할)
    public static Pokemon? ToModel(this PokemonDTO? dto) //this로 메서드가 있는 척 호출
    {//?는 null을 가질수 있음을 표시
        
            if (dto is null) return null;
            //DTO 없으면 즉, 반환할 데이터가 없으면 종료 처리
            
            int id = dto.Id ?? 0; //null이면 0을 사용
            
            string name = string.IsNullOrWhiteSpace(dto.Name) ? "unknown" : dto.Name.Trim();
            //Name이 null이거나 비어있거나 공백문자만 있거나를 판단 맞으면 "unknown", 아니면 Name값 사용(Trim으로 공백제거)
            var types = new List<string>();
            //이중타입을 가지는 포켓몬이 존재 , list로 담기
            if (dto.Types != null)
            {
                foreach (var slot in dto.Types)
                {
                    var typeName = slot?.Type?.Name;
                    if(!string.IsNullOrWhiteSpace(typeName))
                        types.Add(typeName.Trim());
                }
            }
            //종족값 출력을 위해 스탯들을 모두 List에 담기 (나중에 모두 더해서 출력)
            var stats = new Dictionary<string, int>();
            if (dto.Stats != null)
            {
                foreach (var slot in dto.Stats)
                {
                    var statName = slot?.Stat?.Name;
                    if (!string.IsNullOrWhiteSpace(statName))
                        stats[statName.Trim()] = Math.Max(0, slot?.BaseStat ?? 0);
                }
            }
            //포켓몬 이미지 추출
            string artworkUrl =
                dto.Sprites?.Other?.OfficialArtwork?.FrontDefault
                ?? dto.Sprites?.FrontDefault
                ?? string.Empty;
            
            return new Pokemon(id, name, types, stats, artworkUrl);
    }
}
