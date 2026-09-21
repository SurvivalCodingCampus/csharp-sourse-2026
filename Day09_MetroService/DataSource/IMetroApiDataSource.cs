using Day09_MetroService.DTO;
namespace Day09_MetroService.DataSource;
public interface IMetroApiDataSource
{
    Task<Response<MetroDTO>> GetByNameAsync(string stationName);
}
