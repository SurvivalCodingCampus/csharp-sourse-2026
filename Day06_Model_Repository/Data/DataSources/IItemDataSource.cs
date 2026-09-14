using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.DataSources;

public interface IItemDataSource
{
    public Task<List<Item>> LoadAllItemsAsync();
    public Task SaveAllItemsAsync(List<Item> items);
}