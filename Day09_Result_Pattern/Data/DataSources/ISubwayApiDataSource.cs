using Day09_Result_Pattern.Data.DTO;

namespace Day09_Result_Pattern.Data.DataSources;

public interface ISubwayApiDataSource
{
    Task<Response<SubwayResponseDto>> GetArrivalsAsync(
        string stationName
    );
}