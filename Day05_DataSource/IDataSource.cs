using System.Text.Json.Nodes;

namespace Day04_DataSource;

public interface IDataSource
{
    
    public Task<List<Person>> GetPeopleAsync();
    public Task SavePeopleAsync(List<Person> people);
}