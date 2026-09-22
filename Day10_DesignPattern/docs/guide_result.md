# Result 패턴 가이드

AI는 [공통 가이드](README.md)를 적용한다. **Result의 사용 위치는 Day10**, 구체적인 C# 타입 작성법은 Day10에도 반영된 Day09의 중첩 record 구조를 따른다.

## [규칙]

- Service는 `Task<Result<Model, Error>>`로 성공 데이터 또는 예상 가능한 실패를 반환한다.
- `abstract record Result<TData, TError>` 안에 `sealed record Success(TData Data)`와 `Failure(TError Error)`를 둔다.
- `IsSuccess`, `Value`, `Error`를 동시에 변경 가능한 속성으로 두어 서로 모순되는 상태를 만들지 않는다.
- 실패를 `null`, 특수 ID, 빈 정상 모델, 화면용 문자열로 대신하지 않는다. 실패 종류는 오류 enum으로 구분한다.
- 입력·비즈니스 실패는 Service가 판단한다. Repository의 데이터 없음·통신·직렬화 실패도 Service 경계에서 약속된 오류로 바꾼다.
- 사용하는 직렬화 라이브러리와 잡는 예외 타입을 맞춘다. `System.Text.Json`의 형식 오류는 `JsonException`이다.
- 예상 가능한 예외만 처리한다. 모든 예외를 `Unknown`으로 숨겨 프로그래밍 오류를 정상 흐름처럼 만들지 않는다.
- 호출 측은 Success/Failure를 패턴 매칭으로 구분한 뒤 Data 또는 Error를 사용한다. 실제 사용자 취소를 통신 장애로 바꾸지 않는다.

## [Good 예시 코드]

Day10의 `WalletService.SpendCoinsAsync()`를 구현한 교육용 예시다. `GameError`는 해당 흐름에 필요한 항목만 표시했으며 **`SerializationFailed`는 Day09의 오류 구분을 참고해 보완한 예시 항목**이다. 현재 Day10 UML에 이미 존재하는 값이라고 가정하지 않는다.

```csharp
using System.Net.Http;
using System.Text.Json;

namespace GuidelineExample;

public abstract record Result<TData, TError>
{
    private Result() { }

    public sealed record Success(TData Data) : Result<TData, TError>;
    public sealed record Failure(TError Error) : Result<TData, TError>;
}

public enum GameError
{
    NotFound,
    NetworkError,
    SerializationFailed,
    InvalidAmount,
    InsufficientCoins
}

public class WalletService(IWalletRepository walletRepository)
{
    public async Task<Result<Wallet, GameError>> SpendCoinsAsync(int amount)
    {
        if (amount <= 0)
        {
            return new Result<Wallet, GameError>.Failure(GameError.InvalidAmount);
        }

        try
        {
            Wallet wallet = await walletRepository.GetWalletAsync();
            if (wallet.Coins < amount)
            {
                return new Result<Wallet, GameError>.Failure(GameError.InsufficientCoins);
            }

            Wallet updatedWallet = wallet with { Coins = wallet.Coins - amount };
            await walletRepository.SaveWalletAsync(updatedWallet);
            return new Result<Wallet, GameError>.Success(updatedWallet);
        }
        catch (KeyNotFoundException)
        {
            return new Result<Wallet, GameError>.Failure(GameError.NotFound);
        }
        catch (JsonException)
        {
            return new Result<Wallet, GameError>.Failure(GameError.SerializationFailed);
        }
        catch (HttpRequestException)
        {
            return new Result<Wallet, GameError>.Failure(GameError.NetworkError);
        }
        catch (TimeoutException)
        {
            return new Result<Wallet, GameError>.Failure(GameError.NetworkError);
        }
        catch (TaskCanceledException)
        {
            // 이 예시는 호출 측 취소 토큰을 받지 않는 계약이다.
            return new Result<Wallet, GameError>.Failure(GameError.NetworkError);
        }
    }
}
```

이 예시는 호출 측 취소 토큰을 지원하지 않는 계약이다. 취소를 지원하도록 확장할 때는 `TaskCanceledException`을 무조건 오류로 바꾸지 말고 토큰의 취소 여부에 따라 다시 전파한다. 저장이 실패하면 Success를 반환하지 않는다. 통신 오류가 반환되더라도 원격 저장이 이미 반영되었을 수 있으므로 무조건 재시도하지 않는다.

## [Bad 예시 코드]

**현재 코드에서 확인한 개선 대상:** [Day08 PokemonRepository.cs](../Day08_DTO_Mapper/Data/Repository/PokemonRepository.cs)의 실패 처리다.

```csharp
if (response.StatusCode == 404)
{
    return new Pokemon(0, "MissingNo.", "", []);
}

if (response.StatusCode != 200)
{
    return new Pokemon(-1, pokemonName, "", []);
}
```

호출 측이 ID와 이름에 숨겨진 실패 의미를 알아야 한다. Day10 기준에서는 Repository가 실패 계약을 전달하고 Service가 `Failure`로 바꿔야 한다.

추가 확인 사례: [Day09 PokemonRepository.cs](../Day09_Result/Data/Repository/PokemonRepository.cs)는 `System.Text.Json.JsonSerializer`를 쓰면서 `JsonSerializationException`을 검사한다. 실제 잘못된 JSON이 던지는 `JsonException`과 타입이 달라 의도한 오류로 분류되지 않을 수 있다. Fake가 Newtonsoft 예외를 직접 던지는 테스트만으로 실제 역직렬화 오류 처리가 검증되지는 않는다.

## [확인 기준]

- 성공 데이터와 실패 오류가 각각 올바른 record에 들어가는가?
- 잘못된 수량·잔액 부족 시 저장하지 않는가?
- 조회 실패뿐 아니라 저장 실패도 Failure로 전달하는가?
- 실제 사용하는 파서의 잘못된 JSON 입력을 테스트했는가?
- 예상하지 않은 프로그래밍 오류와 사용자 취소를 무조건 숨기지 않는가?

## [근거 파일]

- [Day10 과제 2 UML](../Day10_DesignPattern/과제_2_Service_계층_분리_설계.puml)
- [Day09 Result.cs](../Day09_Result/Common/Result.cs)
- [Day09 PokemonRepository.cs](../Day09_Result/Data/Repository/PokemonRepository.cs)
- [Day08 PokemonRepository.cs](../Day08_DTO_Mapper/Data/Repository/PokemonRepository.cs)
