using Day06_Model_Repository.Data.DataSources;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.Data.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private int MaxSlot { get; set; }
    private int MaxStack { get; set; }
    private readonly IItemDataSource _itemDataSource;

    public InventoryRepository(IItemDataSource dataSource, int maxSlot, int maxStack)
    {
        _itemDataSource = dataSource;
        MaxSlot = maxSlot;
        MaxStack = maxStack;
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        var items = await _itemDataSource.LoadAllItemsAsync();
        return items;
    }

    public async Task<Item?> GetItemByIdAsync(int itemId)
    {
        var items = await GetItemsAsync();
        var idCheck = items.FirstOrDefault(e => e.Id == itemId);
        return idCheck;
    }

    public async Task<bool> AddItemAsync(Item item)
    {
        var items = await GetItemsAsync();
        var sameCheck = await GetItemByIdAsync(item.Id);
        
        if (sameCheck != null)
        {
            if (sameCheck.Count >= MaxStack)
            {
                Console.WriteLine($"{sameCheck.Name} 해당 아이템은 99개로 가득 찼습니다.");
                return false;
            }
            sameCheck.Count += item.Count;
        }
        else
        {
            if (items.Count >= MaxSlot)
            {
                Console.WriteLine($"가방이 가득 차 {item.Name}은 인벤토리에 추가되지 않았습니다.");
                return false;
            }
            items.Add(item);
        }
        await _itemDataSource.SaveAllItemsAsync(items);
        return true;
    }
}