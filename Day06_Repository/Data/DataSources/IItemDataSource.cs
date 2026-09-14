using Day06_Repository.Models;

namespace Day06_Repository.Data.DataSources;

public interface IItemDataSource
{
    Task<List<Item>> LoadAllItemsAsync();
    Task SaveAllItemsAsync(List<Item> items);
}