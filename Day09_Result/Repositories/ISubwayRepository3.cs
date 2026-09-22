using Day09_Result.Common;
using Day09_Result.Error;
using Day09_Result.Models;

namespace Day09_Result.Repositories;

public interface ISubwayRepository3 {
    Task<Result3<List<Subway3>, Error3>> GetArrivalsAsync(string stationName);
}