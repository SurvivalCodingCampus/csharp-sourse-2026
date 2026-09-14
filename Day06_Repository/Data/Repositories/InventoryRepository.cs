namespace Day06_Repository;

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

    public async Task<Item?> GetItemByIdAsync(string itemId)
    {
        var items = await _dataSource.LoadAllItemsAsync();
        return items.FirstOrDefault(item => item.Id == itemId);
    }

    public async Task<bool> AddItemAsync(Item item)
    {
        var items = await _dataSource.LoadAllItemsAsync();
        var existing = items.FirstOrDefault(i => i.Id == item.Id);

        if (existing != null)
        {
            if (existing.Count + item.Count > _maxStack)
            {
                return false;
            }

            existing.Count += item.Count;
        }
        else
        {
            if (items.Count >= _maxSlot)
            {
                return false;
            }

            if (item.Count > _maxStack)
            {
                return false;
            }

            items.Add(item);
        }

        await _dataSource.SaveAllItemsAsync(items);
        return true;
    }
}
