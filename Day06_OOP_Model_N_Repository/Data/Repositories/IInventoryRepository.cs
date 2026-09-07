namespace Day06_OOP_Model_N_Repository.Data.Repositories;

public interface IInventoryRepository {
    Task<List<Item>> GetItemsAsync();
    Task<Item?> GetItemByIdAsync(int itemId);
    Task<bool> AddItemAsync(Item item); //
}