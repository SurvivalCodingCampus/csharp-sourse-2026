using Day05_DataSource.Models;

namespace Day05_DataSource.Interfaces;

public interface IDataSource
{
    Task<List<Person>> GetPeopleAsync();
    Task SavePeopleAsync(List<Person> people);
}