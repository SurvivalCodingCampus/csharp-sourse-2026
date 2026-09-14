# Day 06 TIL

- 이름: 장종민
- 작성일: 2026-09-13

## 1. 오늘 막힌 부분 또는 내린 판단

인벤토리에 아이템을 추가할 때 새로운 아이템과 기존에 존재하는 아이템을  
같은 방식으로 처리하면 안 된다는 점이 헷갈렸다.

새로운 아이템은 `maxSlot`을 확인해야 하고,  
기존 아이템은 새 슬롯을 사용하는 것이 아니라 기존 `Count`를 증가시켜야 한다.

또한 기존 아이템과 새로운 아이템 모두 최종 개수가 `maxStack`을 초과하지 않는지 확인해야 한다.

## 2. 수정 전과 수정 후

### 수정 전

```csharp
public async Task<bool> AddItemAsync(Item item)
{
    var allItems = await _dataSource.LoadAllItemsAsync();

    allItems.Add(item);

    await _dataSource.SaveAllItemsAsync(allItems);

    return true;
}
```

### 수정 후

```csharp
public async Task<bool> AddItemAsync(Item item)
{
    var allItems = await _dataSource.LoadAllItemsAsync();

    var existingItem =
        allItems.FirstOrDefault(i => i.Id == item.Id);

    if (existingItem == null)
    {
        if (allItems.Count >= _maxSlot)
        {
            return false;
        }

        if (item.Count > _maxStack)
        {
            return false;
        }

        allItems.Add(item);
    }
    else
    {
        if (existingItem.Count + item.Count > _maxStack)
        {
            return false;
        }

        existingItem.Count += item.Count;
    }

    await _dataSource.SaveAllItemsAsync(allItems);

    return true;
}
```

새로운 아이템과 기존 아이템을 구분해 `maxSlot`, `maxStack` 조건을 확인한 뒤   
추가하거나 개수를 증가시키도록 수정했다.

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용함
- 질문: MockItemDataSource를 사용해 NUnit 테스트 코드 작성 방법을 질문
- 제안받은 내용: MockItemDataSource를 만들고 5개의 테스트 케이스를 NUnit으로 작성
- 채택 또는 거절한 내용: NUnit 방식의 테스트 코드 구조를 채택
- 판단한 이유: 기존 테스트 프로젝트가 NUnit을 사용하고 있고,  
과제에서 요구한 5가지 테스트 조건과 일치하는지 확인 후 적용

## 4. 검증 결과

- 빌드: 성공
- 실행 결과: 기존_아이템_추가시_개수가_증가 Success  
새로운_아이템_추가시_정상적으로_추가 Success  
인벤토리_초기화_및_아이템_불러오기 Success  
최대_스택_초과시_아이템_개수가_증가하지_않음 Success    
최대_슬롯_초과시_새로운_아이템이_추가되지_않음 Success
- 추가로 확인한 내용: `maxSlot`, `maxStack`을 초과하면 아이템이 추가되지 않는지 확인

## 5. 아직 궁금한 점

나중에 파일이나 서버에서 데이터를 불러올 때도  
지금 만든 Repository를 비슷하게 사용할 수 있는지 궁금하다.

## 6. 다음에 적용할 것

다음에는 한 클래스에 기능을 다 넣지 않고 역할을 나눠서 작성해 볼 것이다.