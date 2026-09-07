using Day05_DataSource.Model;

namespace Day05_DataSource.Interface;

//구현없는 인터페이스 메서드 선언
public interface IDataSource {
    public Task<List<Person>> GetPeopleAsync();
    public Task SavePeopleAsync(List<Person> people);
}