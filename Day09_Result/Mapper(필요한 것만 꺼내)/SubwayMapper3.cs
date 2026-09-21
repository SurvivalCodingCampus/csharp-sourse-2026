using Day09_Result.Models;

namespace Day09_Result.Mapper_필요한_것만_꺼내_;

public static class SubwayMapper3 {
    
    public static List<Subway3> ToModels(SubwayDto3 dto) {
        var list = new List<Subway3>();
        foreach (var item in dto.realtimeArrivalList ?? new List<RealtimeArrivalDto>()) {
            list.Add(new Subway3(item.statnNm, item.updnLine, item.trainLineNm, item.arvlMsg2));
        }
        return list;
    }
}
