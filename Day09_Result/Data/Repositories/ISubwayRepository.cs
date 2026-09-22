using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.Models;

namespace Day09_Result.Data.Repositories;

public interface ISubwayRepository
{
    Task<Result<List<Subway>, SubwayError>> GetSubwayByNameAsync(string statnName);
}