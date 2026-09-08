using Day05_DataSource.Models;

namespace Day05_DataSource.Interfaces;

public interface IDataSource
{
    Task<List<People>> GetPeopleAsync();
    Task SavePeopleAsync(List<People> people);
}