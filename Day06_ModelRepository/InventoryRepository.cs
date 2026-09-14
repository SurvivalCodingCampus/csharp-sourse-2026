namespace Day06_ModelRepository;

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
        var items = await _dataSource.LoadAllItemsAsync();
        return items.FirstOrDefault(i => i.Id == itemId);
    }

    public async Task<bool> AddItemAsync(Item item)
    {
        if (item == null || item.Count <= 0)
        {
            return false;
        }

        var items = await _dataSource.LoadAllItemsAsync();

        var existingItem = items.FirstOrDefault(i => i.Id == item.Id);

        if (existingItem != null)
        {
            if (existingItem.Count + item.Count > _maxStack)
            {
                return false;
            }

            existingItem.Count += item.Count;
        }
        else
        {
            if (item.Count >= _maxSlot)
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