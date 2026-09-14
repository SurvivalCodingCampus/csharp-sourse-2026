using Day06_Model_Repository.DataSources;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly IItemDataSource _dataSource;
    private readonly int _maxSlot;
    private readonly int _maxStack;

    public InventoryRepository(
        IItemDataSource dataSource,
        int maxSlot,
        int maxStack)
    {
        _dataSource = dataSource;
        _maxSlot = maxSlot;
        _maxStack = maxStack;
    }

    public Task<List<Item>> GetItemsAsync()
    {
        return _dataSource.LoadAllItemsAsync();
    }

    public async Task<Item?> GetItemByIdAsync(int itemId)
    {
        var allItems = await _dataSource.LoadAllItemsAsync();

        return allItems.FirstOrDefault(item => item.Id == itemId);
    }

    public async Task<bool> AddItemAsync(Item item)
    {
        var allItems = await _dataSource.LoadAllItemsAsync();

        var existingItem =
            allItems.FirstOrDefault(i => i.Id == item.Id);

        if (existingItem == null)
        {
            if (allItems.Count >= _maxSlot)
            {
                return false;
            }

            if (item.Count > _maxStack)
            {
                return false;
            }

            allItems.Add(item);
        }
        else
        {
            if (existingItem.Count + item.Count > _maxStack)
            {
                return false;
            }

            existingItem.Count += item.Count;
        }

        await _dataSource.SaveAllItemsAsync(allItems);

        return true;
    }
}