# Day 10 DesignPattern

- 이름: `윤우상`
- 작성일: `2026-09-22`

## 1. 오늘 막힌 부분 또는 내린 판단

- AI가 작성한 설계 구조를 보고 직접 이해하기에 난해하거나 기존에 작성했던 체계와 다른 경우가 있어 이해에 어려움이 있었음.
  - 해당 부분과 직접 구성했던 내용을 비교시켜 설명을 받아내어 이해에 도움을 받음
- 기존 Day08·09 코드와 비교하여 UML의 모델은 `record`, 의존성 전달은 기본 생성자, 결과는 `Success`와 `Failure`로 표현하도록 정리함.
- Day09에서는 Repository가 `Result`를 반환했지만, Day10에서는 데이터 조회·저장은 Repository, 비즈니스 규칙과 최종 결과 처리는 Service로 역할을 나누기로 함.
- AI에게도 같은 기준을 전달할 수 있도록 `docs/`에 DataSource, Repository, Test, DTO, Mapper, Result 가이드를 작성함. 같은 내용은 Day10을 최신 기준으로 삼고 규칙과 Good/Bad 예시를 함께 기록함.

## 2. 수정 전과 수정 후

### 수정 전

기존 UML에서는 성공 여부, 데이터, 오류를 하나의 Result에 함께 표시했다. 아래 코드는 UML의 변경 내용을 C#으로 표현한 비교 예시이며, Day10의 실제 구현 코드를 수정한 것은 아니다.

```csharp
public class Result<TData, TError>
{
    public bool IsSuccess { get; set; }
    public TData? Value { get; set; }
    public TError? Error { get; set; }
}
```

### 수정 후

```csharp
public abstract record Result<TData, TError>
{
    private Result()
    {
    }

    public sealed record Success(TData Data) : Result<TData, TError>;
    public sealed record Failure(TError Error) : Result<TData, TError>;
}
```


## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용함
- 질문: PlantUML 설계의 SOLID 위반·누락 부분 확인, 기존 작성 스타일에 맞춘 UML 수정, AI에게 전달할 계층별 가이드 작성 및 수정 요청.
- 제안받은 내용: Service와 Repository의 책임 분리, 생성자 주입 명시, Repository의 실패 처리 규칙 통일, 실제 코드와 TIL을 근거로 한 Good/Bad 예시 작성.
- 채택한 내용: 기존에 사용한 `record`, 기본 생성자, `Success/Failure` 구조를 UML과 가이드에 반영함. 테스트는 구현 코드와 분리하고 실제 API 대신 대역을 사용하도록 정리함.
- 바로 적용하지 않은 내용: 
- 판단한 이유: 기존 코드와 연결해서 이해할 수 있는 구조가 필요

## 4. 검증 결과

- 빌드: 
- 실행 결과: 
- 확인한 내용: 
- 추가 확인:
- 검증하지 않은 내용:

## 5. 아직 궁금한 점

- 

## 6. 다음에 적용할 것

- 이후 AI를 통한 작업에 [가이드라인 문서 폴더](../docs)를 적절히 이용하여 설계 원칙과 디자인 패턴을 적용해보기
