using Day09_MetroService.Common;
using Day09_MetroService.Common.Error;
using Day09_MetroService.Model;
namespace Day09_MetroService.Reposetory;
public interface IMetroRepository
{
    Task<Result<IReadOnlyList<Metro>, MetroError>> GetByStationNameAsync(string stationName);
}
