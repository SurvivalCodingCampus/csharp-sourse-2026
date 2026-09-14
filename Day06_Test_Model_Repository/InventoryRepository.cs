using System.Diagnostics.CodeAnalysis;
using System.Linq;
namespace Day06_Model_Repository;

public class InventoryRepository : IInventoryRepository
{
    private readonly IItemDataSource _dataSource;
    private readonly int _maxSlot;
    private readonly int _maxStack;

    //InventoryRepository 클래스 구현
    //    생성자로 부터 IItemDataSource 와 int maxSlot (슬롯 갯수), int maxStack(아이템당 최대 갯수)를 주입받을 것
    //    AddItemAsync() 는 다음 규칙을 따름
    //인벤토리에 없는 아이템을 추가할 경우 maxSlot 수를 초과할 수 없음
    //하나의 아이템당 총 갯수(count)는 maxStack을 초과할 수 없음 (기존 아이템에 추가할 때도 적용)
    //위 조건이 모두 충족될 때만 아이템을 추가하며, 성공 시 true, 실패 시 false를 반환

    public InventoryRepository(IItemDataSource dataSource, int  maxSlot, int maxStack)
    {
        //ai 추천: 잘못 설정된 값은 생성 시점에 거절됨.
        ArgumentNullException.ThrowIfNull(dataSource);

        if (maxSlot <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxSlot));

        if (maxStack <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxStack));

        _dataSource = dataSource;
        _maxStack = maxStack;
        _maxSlot = maxSlot;
    }

    public Task<List<Item>> GetItemsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Item?> GetItemByldAsync(int ItemId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddItemAsync(Item item)
    {
        
        if (item is null || item.Count <= 0)
            return false;

        var items = await _dataSource.LoadAllItemsAsync();
        //FirstOrDefault() : 
        //조건에 맞는 첫 번째 요소를 반환하고 조건에 맞는 요소가 없으면 기본값(Default)을 반환 (메모용 주석)
        var existingItem = items.FirstOrDefault(
            savedItem => savedItem.Id == item.Id);

        if (existingItem is null)
        {
            // 새 아이템이므로 슬롯과 수량을 모두 검사한다.
            if (items.Count >= _maxSlot)
                return false;

            if (item.Count > _maxStack)
                return false;

            items.Add(item);
        }
        else
        {
            // 기존 아이템이므로 합산된 수량만 검사한다.
            if (existingItem.Count + item.Count > _maxStack)
                return false;

            existingItem.IncreaseCount(item.Count);
        }

        await _dataSource.SaveAllItemsAsync(items);
        return true;

    }
}