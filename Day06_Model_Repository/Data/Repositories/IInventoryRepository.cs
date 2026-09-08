using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.Data.Repositories;

public interface IInventoryRepository
{
    Task<List<Item>> GetItemsAsync();
    Task<Item?> GetItemByIdAsync(int itemId);
    Task<bool> AddItemAsync(Item item);
}