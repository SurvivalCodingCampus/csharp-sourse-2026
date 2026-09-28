# Test 가이드

## 적용 범위

DataSource, Mapper, Repository, Service의 경계와 규칙을 검증하는 NUnit 테스트.

## 규칙

- 테스트 이름은 `대상_조건_기대결과`로 쓰고, 성공·없음·형식 오류·통신 실패·취소를 구분한다.
- Repository와 Service 단위 테스트에는 가짜 DataSource/Repository를 주입한다. 실 API와 네트워크에 의존하지 않는다.
- 반환 타입만 확인하지 말고 데이터 값, 오류 종류, 저장 호출 여부와 횟수를 검증한다.
- Mapper 테스트는 `null`, 필수 필드 누락, 음수 값처럼 계약의 경계에서 차이가 나는 경우를 검증한다.
- 레벨 완료·보상·작업 완료처럼 상태가 바뀌는 경우 실패 시 부분 저장, 중복 호출도 확인한다. 원자성 보장은 실제 저장 방식에 따라 별도 통합 테스트가 필요하다.

## Good 예시 코드

아래는 향후 `TaskService`가 구현되면 작성할 테스트의 형태다. `FakeTaskRepository`는 테스트용 구현으로 별도로 만든다.

```csharp
[Test]
public async Task CompleteTaskAsync_없는작업이면_TaskUnavailable을반환()
{
    var repository = new FakeTaskRepository(availableTasks: []);
    var service = new TaskService(repository);

    var result = await service.CompleteTaskAsync(
        Guid.NewGuid(), "lamp");

    Assert.That(result,
        Is.TypeOf<Result<TaskProgress, GameError>.Error>());
    var error = (Result<TaskProgress, GameError>.Error)result;
    Assert.That(error.error, Is.EqualTo(GameError.TaskUnavailable));
    Assert.That(repository.SaveCount, Is.Zero);
}
```

실제 생성자와 오류 종류가 확정되면 그 계약에 맞춰 테스트 코드를 조정한다.

## Bad 예시 코드

```csharp
[Test]
public async Task CompleteTaskAsync_테스트()
{
    var result = await service.CompleteTaskAsync(playerId, "lamp");
    Assert.That(result, Is.Not.Null);
}
```

`Result`가 성공이든 오류든 통과한다. 외부 API에 직접 붙어 실행 결과가 네트워크 상태에 따라 달라지는 테스트도 단위 테스트로 사용하지 않는다.

## 기존 코드에서 확인한 교훈

`Day09_Result_Pattern_Test/PokemonTests.cs`에는 시간 초과와 JSON 오류 테스트가 있다. 이는 좋은 출발점이지만 해당 파일만으로 정상 응답, `404`, 본문 누락을 검증할 수는 없다. Island 게임 테스트에서는 성공 경로와 실패 경로를 함께 작성한다.
