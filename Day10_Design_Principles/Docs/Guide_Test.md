# Test 가이드라인

[규칙]
- Service 단위 테스트는 Repository를 Mock/Fake로 대체하여, 비즈니스 규칙만 격리해서 검증한다.
- 테스트 이름은 `메서드명_조건_기대결과` 패턴을 따른다. (예: `SpendCurrency_InsufficientCoins_ReturnsFail`)
- 성공 케이스뿐 아니라 모든 `ServiceError` 실패 케이스도 반드시 테스트한다.
- 실제 네트워크/DB/파일 I/O에 의존하는 테스트(DataSource, Repository 구현체 테스트)는 별도의 통합 테스트로 분리하고, Service 단위 테스트와 섞지 않는다.
- Mapper는 순수 함수이므로 입력→출력 매핑만 검증하면 충분하다 (Mock 불필요).

[Good 예시 코드]
```csharp
[Test]
public async Task SpendCurrency_InsufficientCoins_ReturnsFail()
{
    // Arrange
    var mockRepo = new Mock<IPlayerRepository>();
    mockRepo.Setup(r => r.GetPlayerAsync("p1"))
        .ReturnsAsync(new Player { playerId = "p1", coins = 10 });
    var service = new PlayerService(mockRepo.Object, Mock.Of<IPlayerSettingsRepository>());

    // Act
    var result = await service.SpendCurrency("p1", coins: 100, gems: 0);

    // Assert
    Assert.IsFalse(result.isSuccess);
    Assert.AreEqual(ServiceError.INSUFFICIENT_CURRENCY, result.error);
    mockRepo.Verify(r => r.SavePlayerAsync(It.IsAny<Player>()), Times.Never); // 저장 호출 안 됐는지 확인
}

[Test]
public async Task SpendCurrency_SufficientCoins_ReturnsSuccessAndSaves()
{
    var mockRepo = new Mock<IPlayerRepository>();
    mockRepo.Setup(r => r.GetPlayerAsync("p1"))
        .ReturnsAsync(new Player { playerId = "p1", coins = 100 });
    var service = new PlayerService(mockRepo.Object, Mock.Of<IPlayerSettingsRepository>());

    var result = await service.SpendCurrency("p1", coins: 50, gems: 0);

    Assert.IsTrue(result.isSuccess);
    Assert.AreEqual(50, result.value.coins);
    mockRepo.Verify(r => r.SavePlayerAsync(It.Is<Player>(p => p.coins == 50)), Times.Once);
}
```

[Bad 예시 코드] (본인이 실수한 경험 남기기)
```csharp
// 실수했던 코드: 실제 DataSource(네트워크)를 그대로 사용한 테스트
[Test]
public async Task SpendCurrency_Test()
{
    var realDataSource = new RemotePlayerDataSource(new HttpClient()); // ❌ 실제 네트워크 호출
    var realRepo = new PlayerRepository(realDataSource);
    var service = new PlayerService(realRepo, /* ... */);

    var result = await service.SpendCurrency("p1", coins: 50, gems: 0);

    Assert.IsTrue(result.isSuccess); // ❌ 성공 케이스만 검증, 실패 케이스 없음
}
```
**문제점**: 이 테스트는 서버가 켜져 있어야만 통과했고, CI 환경에서 네트워크가 불안정할 때마다 무관한 이유로 실패했다. 또한 "재화 부족 시 실패"하는 케이스를 전혀 검증하지 않아서, 실제로 `spendCurrency`에 음수 잔액을 허용하는 버그가 배포된 뒤에야 발견됐다. 이후 Service 테스트는 전부 Mock Repository로 교체하고, 실제 API 연동 테스트는 `IntegrationTests/` 폴더로 분리했다.