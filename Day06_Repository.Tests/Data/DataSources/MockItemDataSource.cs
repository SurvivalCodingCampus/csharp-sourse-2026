using System.Collections.Generic;
using System.Threading.Tasks;
using Day06_Repository.Data.DataSources;
using Day06_Repository.Models;

namespace Day06_Repository.Tests.Data.DataSources;

public class MockItemDataSource: IItemDataSource
{
    private List<Item> _items = new List<Item>
    {
        new Item(1, "Sword", 10),
        new Item(2, "Shield", 5)
    };

    public async Task<List<Item>> LoadAllItemsAsync() => _items;

    public Task SaveAllItemsAsync(List<Item> items)
    {
        throw new System.NotImplementedException();
    }
}