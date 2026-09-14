using Day06_Model_Repository.DataSources;
using Day06_Model_Repository.Models;

namespace Day06_Model_Repository_Test;

public class MockItemDataSource : IItemDataSource
{
    private List<Item> _items;

    public MockItemDataSource(List<Item> items)
    {
        _items = items;
    }

    public Task<List<Item>> LoadAllItemsAsync()
    {
        return Task.FromResult(_items);
    }

    public Task SaveAllItemsAsync(List<Item> items)
    {
        _items = items;

        return Task.CompletedTask;
    }
}