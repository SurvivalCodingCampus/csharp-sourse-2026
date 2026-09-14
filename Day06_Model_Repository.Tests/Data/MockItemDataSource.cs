using Day06_Model_Repository.Models;
using Day06_Model_Repository.DataSources;

namespace Day06_Model_Repository.Tests.Data;

public class MockItemDataSource : IItemDataSource
{
    public List<Item> Items { get; set; } = new List<Item>();

    public Task<List<Item>> LoadAllItemsAsync()
    {
        return Task.FromResult(Items);
    }

    public Task SaveAllItemsAsync(List<Item> items)
    {
        Items = items;
        return Task.CompletedTask;
    }
}