using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.DataSources;

public interface IItemDataSource
{
    Task<List<Item>> LoadAllItemsAsync();
    Task SaveAllItemsAsync(List<Item> items);
}