# Day 06 TIL

- 이름: <이혁>
- 작성일: <2026-09-08>

## 1. 오늘 막힌 부분 또는 내린 판단

<과제 1과 과제 2의 InventoryRepository 클래스 구현은 수월하게 작성했지만, AddItemAsync()와 과제 3은 스스로 코드를 작성해 보려고 많은 노력을 하였으나 한계를 많이 느껴 AI의 도움을 받았습니다.>

## 2. 수정 전과 수정 후

### 수정 전

```csharp
public class InventoryRepository
{
    public IItemDataSource DataSource { get; }  

    public InventoryRepository(IItemDataSource dataSource)
    {
        DataSource = dataSource;
    }
}
```

### 수정 후

```csharp
public class InventoryRepository
{
    private readonly IItemDataSource _dataSource; 

    public InventoryRepository(IItemDataSource dataSource)
    {
        _dataSource = dataSource;
    }
```

<실무에서는 프로퍼티 대신 private readonly 필드로 저장하는 경우가 더 흔하다고 하여서 수정하였습니다.>

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: <사용함>
- 질문: <생성자로부터 값을 주입받는다는 것은, 객체가 처음 만들어질 때 한 번 값이 정해지고 그 이후로는 안 바뀌어야 된다는 뜻이야?>
- 제안받은 내용: <실무에서는 프로퍼티 대신 private readonly 필드로 저장하는 경우가 더 흔합니다.>
- 채택 또는 거절한 내용: <private readonly를 선택하여 작성했습니다.>
- 판단한 이유: <private는 클래스 외부에서 접근이 불가하여 프로퍼티보다 더 강하게 숨긴다는 것을 설명해 주었고, readonly는 생성자에서 한 번 값을 대입한 후로는 재대입이 불가능하고 밑줄(_)로 시작하는 이름은 private 필드라는 걸 나타내는 C# 관례라는 설명을 듣고 기존에 있던 배경지식과 동일하여 적용하였습니다.>

AI 대화 전문을 붙이지 말고 질문, 판단, 검증 내용을 요약합니다.

## 4. 검증 결과

- 빌드: <성공>
- 실행 결과: <Unit Tests 창으로 테스트 결과 확인 후, 테스트 성공이 나와 Git Hub에 사진으로 첨부하였습니다.>
- 추가로 확인한 내용: 

## 5. 아직 궁금한 점

<JsonFile에 코드를 작성할 때, 시작을 어떻게 해야 할지 많이 막막했는데 JsonFile 작성하는 법을 찾아봐야겠습니다.>

## 6. 다음에 적용할 것

<JsonFile 작성하는 법을 잘 익혀서 다음 과제에 적용해보면 좋을 것 같습니다.>