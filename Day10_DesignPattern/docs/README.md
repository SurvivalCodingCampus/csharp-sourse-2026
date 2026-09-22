# AI 작업 가이드

이 문서는 현재 저장소의 Day01~Day10 소스와 테스트, 학습 기록을 검토하여 작성한 코드 생성·수정 기준이다. AI에게 작업을 요청할 때 이 문서와 관련 항목의 가이드를 함께 전달한다.

## 적용 우선순위

1. 사용자의 현재 요청과 프로젝트 작업 지침을 따른다.
2. 같은 역할이나 비슷한 설계가 있으면 **Day10을 최신 기준**으로 삼는다.
3. Day10에 없는 구현 세부 사항은 Day09 → Day08 → 이전 프로젝트 순서로 참고한다.
4. 이전 코드에 존재한다는 이유만으로 문제 있는 패턴을 반복하지 않는다. 새로 정한 예시 계약과 기존 구현을 구분한다.

Day10의 `Program.cs`는 시작용 코드이며, 최신 아키텍처의 근거는 과제 1·2 UML과 과제 3 문서다. 가이드의 Good 코드는 이 설계를 적용한 **새로운 교육용 예시**이며 현재 프로젝트에 이미 구현되어 있다는 의미가 아니다.

## 문서 목록

| 파일 | AI가 따라야 할 핵심 |
|---|---|
| [guide_datasource.md](guide_datasource.md) | 외부 통신·직렬화, DTO 반환 |
| [guide_repository.md](guide_repository.md) | 데이터 조회·저장, Mapper 호출, 도메인 모델 반환 |
| [guide_test.md](guide_test.md) | NUnit, Given/When/Then, 외부 통신 없는 단위 테스트 |
| [guide_dto.md](guide_dto.md) | 외부 데이터 구조와 누락 가능성 보존 |
| [guide_mapper.md](guide_mapper.md) | DTO ↔ Model 순수 변환, 빈 값 처리 계약 |
| [guide_result.md](guide_result.md) | Service의 Success/Failure 반환 및 오류 변환 |

## 공통 구조

```text
View / Program
    → Service: 입력·비즈니스 규칙 검사, Result<Model, Error> 반환
    → Repository 인터페이스: Task<Model> / Task 반환
    → Repository 구현: DTO와 Model 사이 변환을 Mapper에 위임
    → DataSource 인터페이스: Task<Dto> / Task 반환
    → DataSource 구현: API·DB·파일 접근과 직렬화/역직렬화
```

| 역할 | 권장 위치 | 넣지 않을 책임 |
|---|---|---|
| DTO | `Data/DTOs` | 비즈니스 규칙, 저장 호출 |
| Model | `Data/Models` | HTTP·JSON 형식에 대한 의존 |
| 인터페이스 | `Data/Interfaces` | 구체적인 통신 구현 |
| DataSource | `Data/DataSources` | 도메인 모델 생성, 재화 소비 판정 |
| Repository | `Data/Repository` | 화면 출력, 잔액·보상 수령 규칙 |
| Mapper | `Data/Mapper` | I/O, 상태 변경 |
| Result / Error | `Common`, `Common/Error` | 특정 API·Repository 구현에 대한 의존 |
| Service | `Services` | 화면 출력, 직접 HTTP 호출 |

## 기존 코드와 달라지는 부분

- **DataSource:** Day07~09는 JSON 문자열을 담은 `Response`를 반환하지만, 이번 요청의 규칙에 맞춰 Good 예시는 DTO를 반환한다. 따라서 역직렬화 위치도 DataSource로 이동한다.
- **Repository:** Day09는 `Result`를 반환하지만, Day10 구조를 우선하여 신규 예시는 모델을 반환하고 실패를 예외 계약으로 전달한다.
- **Service:** Day10처럼 비즈니스 규칙을 처리하고 예상 가능한 실패를 `Result`로 변환한다.
- **모델:** Day10의 위치 기반 record를 따른다. 변경할 때는 `with`로 새 값을 만들며, record 안에 가변 컬렉션이 있으면 깊은 불변성까지 보장되는 것은 아니다.
- **오류 계약:** Day10에서 미정인 예외 종류와 DTO 스키마는 각 예시에 명시했다. 기존 코드와 UML을 자동으로 변경하라는 지시는 아니다.

## 작성 스타일

- 파일 범위 namespace, PascalCase 타입·속성·메서드, camelCase 일반 매개변수를 사용한다.
- 위치 기반 record 매개변수는 생성될 속성과 동일한 PascalCase로 작성한다.
- 의존성은 인터페이스를 통해 생성자로 주입하고, 최근 코드처럼 기본 생성자 문법을 사용할 수 있다.
- 비동기 작업은 `Task`와 `await`, 메서드 이름은 `Async` 접미사를 사용한다. `.Result`, `.Wait()`, `async void`를 일반 비동기 처리에 사용하지 않는다.
- 주석과 테스트 이름은 기존 코드처럼 한국어로 목적을 설명한다.
- 새 JSON 처리 예시는 `System.Text.Json`으로 통일한다. 기존 Newtonsoft.Json 코드를 요청 없이 일괄 변환하지 않는다.

## 예시 사용 방법

6개 가이드의 Good 코드는 하나의 `Wallet` 흐름으로 연결된다. 예시의 `GuidelineExample` namespace는 교육용이며 실제 적용 시 대상 프로젝트 namespace로 바꾼다. 각 문서의 첫 번째 Good 코드 블록은 별도 `.cs` 파일로 볼 수 있다. DTO → Mapper → DataSource → Repository → Result → Test 순서로 읽으면 된다.

외부 API 규격은 제공되지 않았으므로 `wallet` 경로와 `coins`, `golden_keys` 필드는 **예시 계약**이다. 실제 API에 그대로 요청하지 않는다. 예시의 `HttpClient`는 호출 측에서 `BaseAddress`를 설정해 주입하며, 테스트는 외부 서버에 접속하지 않는다.

Bad 예시는 다음을 구분한다.

- **학습 기록에 남은 경험:** TIL에 원인·수정 과정이 있는 내용.
- **현재 코드에서 확인한 개선 대상:** 코드상 확인되지만 당시 오류 경험이 기록되지는 않은 내용.
- **기존 흐름을 축약·재구성한 예시:** 원문 전체를 그대로 옮긴 것이 아님을 표시한 코드.

## AI에게 전달할 요청 예시

> docs/README.md와 관련 guide 문서를 읽고 작업해 주세요. 중복되는 설계는 Day10을 우선하세요. DataSource는 DTO, Repository는 Model, Service는 Result를 반환하도록 역할을 구분하세요. 기존 계약과 충돌하는 부분은 먼저 설명하고, 요청 범위의 최소 변경만 수행하세요. Good 코드는 참고 예시이며 실제 프로젝트의 타입·API 계약을 확인한 뒤 적용하세요. 확인한 테스트 결과와 미검증 사항을 구분해 보고하세요.

## 검토 범위와 근거

- 현재 작업 폴더의 C# 소스 71개, 프로젝트 설정 14개, Day01~Day09의 기존 TIL, Day10 UML 2개와 과제 3 문서를 확인했다.
- `bin`, `obj`, `.git`, `Backups`는 현재 소스 검토 대상에서 제외했다. 대형 JSON fixture는 파싱하여 구조를 확인하고 테스트 로직과 분리해 검토했다.
- Day10 원칙: [과제 2 UML](../Day10_DesignPattern/과제_2_Service_계층_분리_설계.puml), [과제 3 점검](../Day10_DesignPattern/과제_3_SOLID_원칙_점검.md).
- 이 작업은 가이드 문서 작성이다. 기존 프로젝트 전체가 빌드되거나 모든 기존 테스트가 통과한다고 보증하지 않는다.

## 예시 검증 결과

- 각 가이드의 첫 번째 Good 코드 블록 6개를 임시 프로젝트로 추출하여 설치된 .NET SDK로 컴파일했다. nullable 검사와 경고의 오류 처리를 켠 상태에서 통과했다.
- 외부 API를 호출하지 않는 검증 프로그램으로 26개 검사를 통과했다. 문서의 NUnit 예시 4개 실행 경우와 DTO 누락·0 구분, Mapper 필수값 검사, 조회·저장 연결, 잘못된 JSON, HTTP 실패, 저장 실패, 잔액 경계값, 예외 분류를 포함한다.
- NUnit 예시는 검증 프로그램에서 메서드를 직접 호출했다. NUnit 테스트 러너로 기존 프로젝트 전체를 실행한 결과는 아니다.
