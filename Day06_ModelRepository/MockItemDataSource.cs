namespace Day06_ModelRepository;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MockItemDataSource : IItemDataSource
{
    // 가짜 DB 역할을 하는 메모리 리스트
    private List<Item> _storage;

    // 초기 데이터를 주입받거나, 없으면 빈 리스트로 시작
    public MockItemDataSource(List<Item>? initialData = null)
    {
        // 외부에서 원본을 직접 조작하지 못하도록 새 복사본 리스트를 생성
        _storage = initialData != null 
            ? initialData.Select(i => new Item(i.Id, i.Name, i.Count)).ToList() 
            : new List<Item>();
    }

    public Task<List<Item>> LoadAllItemsAsync()
    {
        // 원본 오염을 방지하기 위해 복사본을 반환
        var copy = _storage.Select(i => new Item(i.Id, i.Name, i.Count)).ToList();
        return Task.FromResult(copy);
    }

    public Task SaveAllItemsAsync(List<Item> items)
    {
        // 저장 시에도 깊은 복사본을 저장해 참조 문제를 방지
        _storage = items.Select(i => new Item(i.Id, i.Name, i.Count)).ToList();
        return Task.CompletedTask;
    }
}