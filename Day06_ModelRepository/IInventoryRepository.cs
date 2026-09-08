namespace Day06_ModelRepository;

public interface IInventoryRepository
{
    Task<List<Item>> GetItemsAsync();
    Task<Item?> GetItemByIdAsync(int itemId);
    Task<bool> AddItemAsync(Item item);
}