# Test 가이드

AI는 [공통 가이드](README.md)를 적용한다. 테스트 방식은 기존 NUnit 코드를 따르되, 검증할 계층 책임은 Day10의 Service 분리 구조에 맞춘다.

## [규칙]

- 프로젝트의 NUnit과 `[TestFixture]`, `[SetUp]`, `[Test]`, `[TestCase]`, `Assert.That` 스타일을 사용한다.
- 테스트 이름은 기존 코드처럼 한국어로 조건과 기대 결과를 설명한다. Given/When/Then으로 준비·실행·검증을 구분한다.
- 각 테스트는 독립적으로 실행할 수 있어야 한다. `[SetUp]`에서 필드에 새 대역과 대상 객체를 넣고, 같은 이름의 지역변수로 가리지 않는다.
- 단위 테스트는 실제 API·DB·공유 파일에 의존하지 않는다. 외부 통신 검증은 통합 테스트로 구분한다.
- Fake·Stub은 대상 인터페이스와 같은 반환·실패 계약을 지켜야 한다. 비동기 대역에는 `Task.FromResult`와 `Task.FromException`을 사용할 수 있다.
- 정상, 경계값, 실패를 확인한다. Service 테스트는 실패 결과뿐 아니라 저장이 발생했는지도 검증한다.
- 예외가 안 났다는 사실만 확인하지 말고 결과의 의미를 검증한다. 실패 경로를 테스트하기 위해 운영 코드를 테스트 전용 분기로 바꾸지 않는다.
- 비동기 성공 검증은 `async Task`와 `await`, 예외 검증은 `Assert.ThrowsAsync`를 사용한다.
- record에 `List<T>`가 포함되면 바깥 record의 동등성만 믿지 말고 컬렉션 요소를 따로 검증한다.

## [Good 예시 코드]

Day10 스타일의 Service와 Repository 인터페이스를 검증한다. 아래 대역은 메모리만 사용하며 API나 파일에 접근하지 않는다. NUnit 참조 및 다른 가이드의 Good 타입이 필요하다.

```csharp
using GuidelineExample;
using NUnit.Framework;

namespace GuidelineExample.Tests;

[TestFixture]
[TestOf(typeof(WalletService))]
public class WalletServiceTest
{
    private FakeWalletRepository _repository = null!;
    private WalletService _service = null!;

    [SetUp]
    public void SetUp()
    {
        // Given
        _repository = new FakeWalletRepository(new Wallet(100, 2));
        _service = new WalletService(_repository);
    }

    [Test]
    public async Task 잔액이_충분하면_차감한_지갑을_저장한다()
    {
        // When
        var result = await _service.SpendCoinsAsync(30);

        // Then
        Assert.That(result, Is.EqualTo(
            new Result<Wallet, GameError>.Success(new Wallet(70, 2))));
        Assert.That(_repository.Current, Is.EqualTo(new Wallet(70, 2)));
        Assert.That(_repository.SaveCount, Is.EqualTo(1));
    }

    [Test]
    public async Task 잔액이_부족하면_저장하지_않는다()
    {
        var result = await _service.SpendCoinsAsync(101);

        Assert.That(result, Is.EqualTo(
            new Result<Wallet, GameError>.Failure(GameError.InsufficientCoins)));
        Assert.That(_repository.Current, Is.EqualTo(new Wallet(100, 2)));
        Assert.That(_repository.SaveCount, Is.Zero);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public async Task 수량이_유효하지_않으면_조회도_하지_않는다(int amount)
    {
        var result = await _service.SpendCoinsAsync(amount);

        Assert.That(result, Is.EqualTo(
            new Result<Wallet, GameError>.Failure(GameError.InvalidAmount)));
        Assert.That(_repository.GetCount, Is.Zero);
        Assert.That(_repository.SaveCount, Is.Zero);
    }
}

public class FakeWalletRepository(Wallet initialWallet) : IWalletRepository
{
    public Wallet Current { get; private set; } = initialWallet;
    public int GetCount { get; private set; }
    public int SaveCount { get; private set; }

    public Task<Wallet> GetWalletAsync()
    {
        GetCount++;
        return Task.FromResult(Current);
    }

    public Task SaveWalletAsync(Wallet wallet)
    {
        Current = wallet;
        SaveCount++;
        return Task.CompletedTask;
    }
}
```

## [Bad 예시 코드]

**현재 코드에서 확인한 개선 대상:** [Day08 PokemonMapperTest.cs](../Day08_DTO_Mapper_Test/Data/Mapper/PokemonMapperTest.cs)의 빈 DTO 테스트다. 테스트 자체가 틀렸다는 의미가 아니라, 결과 검증을 보완해야 하는 사례다.

```csharp
var noneDataDto = new PokemonDto();
Assert.DoesNotThrow(() => noneDataDto.ToModel());
```

예외가 없어도 ID, 이름, 이미지, 타입 기본값이 잘못될 수 있다. 해당 Mapper가 약속한 기본값을 각각 검증해야 한다. 필수 필드 누락을 실패로 정한 Wallet 예시에는 이 테스트를 복사하지 말고 `JsonException`을 검증한다.

**학습 기록에 남은 경험:** [Day07 TIL](../Day07_Network/TIL.md)에는 실제 API 결과를 테스트에 연결하는 데 어려움이 있어 Fake 응답을 작성한 과정이 있다. [Day06 MockItemDataSource](../Day06_Model_Repository/Tests/MockItemDataSource.cs)는 이름과 달리 실제 파일을 사용하므로 격리된 메모리 대역으로 취급하면 안 된다.

## [계층별 확인 기준]

| 계층 | 검증할 내용 | 격리 방식 |
|---|---|---|
| DataSource | 요청 경로·메서드, DTO 역직렬화, HTTP 실패, 잘못된 JSON | 가짜 `HttpMessageHandler` |
| DTO | JSON 필드 이름, null·누락·0 구분, 오류 응답 구조 | 짧은 JSON fixture |
| Mapper | 모든 값의 대응, 필수값 누락, 선택값·빈 목록 정책 | DTO 직접 생성 |
| Repository | DTO → Model, Model → DTO, 데이터 없음·저장 실패 계약 | DataSource 대역 |
| Service / Result | 성공, 규칙 실패, 경계값, 조회·저장 실패 변환, 저장 여부 | Repository 대역 |

HTTP 200 본문의 오류 코드와 실제 파서의 JSON 예외도 확인한다. 테스트 대역이 특정 예외를 던진다는 사실만으로 실제 외부 데이터 경로까지 검증됐다고 보고하지 않는다.

## [근거 파일]

- [Day09 SubwayArrivalRepositoryTest.cs](../Day09_Result_Test/Data/Repository/SubwayArrivalRepositoryTest.cs)
- [Day09 MockSubwayDataSource.cs](../Day09_Result_Test/Data/DataSource/MockSubwayDataSource.cs)
- [Day08 PokemonMapperTest.cs](../Day08_DTO_Mapper_Test/Data/Mapper/PokemonMapperTest.cs)
- [Day07 TIL](../Day07_Network/TIL.md)
