using Day06_Model_Repository.Models;
using Day06_Model_Repository.DataSources;

namespace Day06_Model_Repository.Data;

public class InventoryRepository : IInventoryRepository
{
    private readonly IItemDataSource _dataSource;
    private readonly int _maxSlot;
    private readonly int _maxStack;

    public InventoryRepository(IItemDataSource dataSource, int maxSlot, int maxStack)
    {
        _dataSource = dataSource;
        _maxSlot = maxSlot;
        _maxStack = maxStack;
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        return await _dataSource.LoadAllItemsAsync();
    }

    public async Task<Item?> GetItemByIdAsync(int itemId)
    {
        List<Item> currentItems = await _dataSource.LoadAllItemsAsync();
        return currentItems.FirstOrDefault(i => i.Id == itemId);
    }

    public async Task<bool> AddItemAsync(Item item)
    {
        List<Item> currentItems = await _dataSource.LoadAllItemsAsync();
        
        Item? existingItem = currentItems.FirstOrDefault(i => i.Id == item.Id);

        if (existingItem != null)
        {
            int newCount = existingItem.Count + item.Count;

            if (newCount > _maxStack)
            {
                return false;  
            }

            existingItem.Count = newCount;
        }
        else
        {
            if (currentItems.Count >= _maxSlot)
            {
                return false;  
            }

            if (item.Count > _maxStack)
            {
                return false;  
            }

            currentItems.Add(item);
        }
        
        await _dataSource.SaveAllItemsAsync(currentItems);
        return true;
    }
}