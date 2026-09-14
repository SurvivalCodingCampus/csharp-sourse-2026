using Day06_Model_Repository.Interfaces;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository.DataSources;

public class InventoryRepository(IItemDataSource source, int maxSlot, int maxStack) : IInventoryRepository
{
    public IItemDataSource Source { get; set; } = source;
    public int MaxSlot { get; private set; } = maxSlot;
    public int MaxStack { get; private set; } = maxStack;

    private List<Item> _cureentList; 


    // 모든 아이템 목록을 비동기로 가져옴
    public async Task<List<Item>> GetItemsAsync()
    {
        return _cureentList = await Task.FromResult(await Source.LoadAllItemsAsync());
    }

    // 특정 아이템을 비동기적으로 검색
    public Task<Item?> GetItemByIdAsync(int itemId)
    {
        if (_cureentList is null)
        {
            _cureentList = GetItemsAsync().Result;
        }
        return Task.FromResult(_cureentList.Find(n => n.ItemId == itemId));
    }

    // 아이템을 인벤토리에 추가하는 메서드 성공시 True, 실패시 False 반환
    public async Task<bool> AddItemAsync(Item item)
    {
        // 인벤토리 전체 데이터 가져오기
        List<Item> itemList = await Source.LoadAllItemsAsync();
        _cureentList = itemList;
        
        var findItem = itemList.Find(n => n.ItemId == item.ItemId);
        
        // 이미 아이템이 있다면
        if (findItem != null && findItem.Count + item.Count <= MaxStack)
        {
            findItem.Count += item.Count;
            await Source.SaveAllItemsAsync(itemList);
            
            return true;
        }
        // 아이템이 없고, 현재 슬롯 수 보다 maxSlot이 작으며, item의 count와 인벤토리 내의 count의 합이 maxStack보다 작은 경우 인벤토리에 추가
        else if (findItem == null && itemList.Count < maxSlot && MaxStack - item.Count >= 0)
        {
            itemList.Add(item);
            await Source.SaveAllItemsAsync(itemList);
            return true;
        }
        return false;
    }
}