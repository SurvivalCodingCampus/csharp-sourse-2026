namespace Day06_Repository;

public interface IInventoryRepository
{
    Task<List<Item>> GetItemsAsync();
    Task<Item?> GetItemByIdAsync(string itemId);
    Task<bool> AddItemAsync(Item item);
}
