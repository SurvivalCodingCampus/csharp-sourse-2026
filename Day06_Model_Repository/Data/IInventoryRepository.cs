using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.Data;

public interface IInventoryRepository
{
    public Task<List<Item>> GetItemsAsync();
    public Task<Item?> GetItemByIdAsync(int itemId);
    public Task<bool> AddItemAsync(Item item);
}