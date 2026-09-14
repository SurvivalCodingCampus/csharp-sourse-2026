using Day06_Repository.Data.DataSources;
using Day06_Repository.Models;

namespace Day06_Repository.Data.Repositories;

public class InventoryRepository(
    IItemDataSource dataSource,
    int maxSlot = 4,
    int maxStack = 12) : IInventoryRepository
{
    public Task<List<Item>> GetItemsAsync()
    {
        return dataSource.LoadAllItemsAsync();
    }

    public async Task<Item?> GetItemByIdAsync(int itemId)
    {
        List<Item> items = await dataSource.LoadAllItemsAsync();
        return items.FirstOrDefault(item => item.Id == itemId);
    }

    public async Task<bool> AddItemAsync(Item item)
    {
        List<Item> items = await dataSource.LoadAllItemsAsync();
        
        if (items.Count > maxStack) return false;
        
        try
        {
            
            items.Add(item);
            await dataSource.SaveAllItemsAsync(items);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}