# DataSource 가이드

## 적용 범위

외부 API, DB, 파일의 읽기·쓰기 경계. Island 게임에서 실제 저장소가 정해지면 `IPlayerDataSource` 같은 계약과 구현에 적용한다.

## 규칙

- DataSource는 HTTP/DB/파일 접근, 응답 코드, DTO 역직렬화만 담당한다. `PlayerProfile` 같은 도메인 모델을 만들지 않는다.
- API 응답의 `404`, `200`과 본문 없음, 네트워크 실패를 구별한다. 상태를 임의의 마법 숫자로 뭉개지 않는다.
- `CancellationToken`을 전달하고 호출 취소를 실패 응답으로 바꾸지 않는다. 예상 가능한 통신 오류만 필요한 범위에서 처리한다.
- `HttpClient` 같은 외부 의존성은 주입한다. URL과 인증 정보는 설정에서 받는다.
- DTO의 필수 필드 검증과 게임 규칙은 각각 Mapper/Repository와 Service에서 수행한다.

## Good 예시 코드

아래의 `ProfileDto`, `Response<T>`는 설명용 타입이다. 실제 API 계약이 정해진 뒤 필드와 상태 처리를 맞춘다.

```csharp
public async Task<Response<ProfileDto>> GetProfileAsync(
    Guid playerId, CancellationToken cancellationToken)
{
    using var response = await _httpClient.GetAsync(
        $"players/{playerId}", cancellationToken);

    if (response.StatusCode == HttpStatusCode.NotFound)
        return new Response<ProfileDto> { StatusCode = 404 };

    response.EnsureSuccessStatusCode();
    var dto = await response.Content.ReadFromJsonAsync<ProfileDto>(
        cancellationToken: cancellationToken);

    return new Response<ProfileDto>
    {
        StatusCode = (int)response.StatusCode,
        Body = dto
    };
}
```

## Bad 예시 코드

```csharp
catch (TaskCanceledException)
{
    return new Response<ProfileDto> { StatusCode = -1 };
}
catch
{
    return new Response<ProfileDto> { StatusCode = 0 };
}
```

이 코드는 사용자 취소와 시간 초과를 섞고, 알 수 없는 오류를 `0` 하나로 숨긴다. 도메인 모델을 여기서 생성하거나 레벨 해금을 판정하는 코드도 넣지 않는다.

## 기존 코드에서 확인한 교훈

`Day09_Result_Pattern/Data/DataSources/PokemonApiDataSource.cs`에 위와 같은 넓은 예외 처리와 `-1`, `0` 상태 코드가 있다. 학습용 코드는 동작했지만 게임 DataSource를 만들 때는 취소와 실패 원인을 구분해야 한다.
