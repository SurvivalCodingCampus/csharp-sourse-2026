using Day06_Repository.Data.DataSources;
using Day06_Repository.Models;

namespace Day06_Repository.Data.Repositories;

public interface IInventoryRepository
{
    Task<List<Item>> GetItemsAsync();
    Task<Item?> GetItemByIdAsync(int itemId);
    Task<bool> AddItemAsync(Item item);
}