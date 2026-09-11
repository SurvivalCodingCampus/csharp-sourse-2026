using System.Runtime.InteropServices.Marshalling;
using Day06_OOP_Model_N_Repository.DataSources;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

// using Day06_OOP_Model_N_Repository.Model;        
namespace Day06_OOP_Model_N_Repository.Data.Repositories;
public class InventoryRepository : IInventoryRepository {
    public int maxSloat;
    public int maxStack;
    public IItemDataSource itemInfo { get; }  //I타입 I변수

    public InventoryRepository(int maxSloatCopy, int maxStackCopy, IItemDataSource itemInfo) {
        this.maxSloat = maxSloatCopy;
        this.maxStack = maxStackCopy;
        this.itemInfo = itemInfo;
    }




    //2. 
    public Task<List<Item>> GetItemsAsync() {
        throw new System.NotImplementedException();
    }
    public Task<Item?> GetItemByIdAsync(int itemId) {
        throw new System.NotImplementedException();
    }
    
    public async Task<bool> AddItemAsync(Item item) {
        // ItemDataSource 인벤토리에 어떤 아이템들이 있는지 불러와 items에 담기
        // await쓴 이유: 아이템 목록 불러올때까지 기다림, 실제 결과를 꺼내기 위함
        List<Item> items = await itemInfo.LoadAllItemsAsync();

        //  있는 ID에 내가 선언한 ID 有没有在前面 check
        var alreadyExistingItem = items.FirstOrDefault(e => e.Id == item.Id);
        int addCnt = item.Cnt;

        if (alreadyExistingItem != null) {
            int alreadyCnt = alreadyExistingItem.Cnt;

            if (alreadyCnt + addCnt > maxStack) {
                return false;
            }

            alreadyExistingItem.Cnt = (alreadyCnt + addCnt);
            return true;
        }

        //
        if (items.Count < maxSloat) {
            return false;
        }

        if (addCnt > maxStack) {
            return false;
        }


        items.Add(item);
        return true;
    }
}
