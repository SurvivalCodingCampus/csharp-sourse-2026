using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day06_Repository.Data.DataSources;
using Day06_Repository.Data.Repositories;
using Day06_Repository.Models;
using Day06_Repository.Tests.Data.DataSources;
using NUnit.Framework;

namespace Day06_Repository.Tests.Data.Repositories;

[TestFixture]
[TestOf(typeof(InventoryRepository))]
public class InventoryRepositoryTest
{

    [Test]
    public async Task 인벤토리_초기화_및_로드()
    {
        // given
        IItemDataSource dataSource = new MockItemDataSource();
        IInventoryRepository repository = new InventoryRepository(dataSource);
        
        // when
        List<Item> items = await repository.GetItemsAsync();
        

        // then
        Assert.IsTrue(items.Any(item => item.Name == "Sword"));
        Assert.IsTrue(items.Any(item => item.Name == "Shield"));
    }
}