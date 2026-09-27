# 과제 4. 아키텍처별 AI 작업 지침

이 문서는 `csharp-sourse-2026` 저장소의 C# 데이터 계층 작업을 AI에게 맡길 때 함께 전달한다. 특히 `Day10_Design_Pattern/Puml/`의 Island 모바일 게임 설계를 구현할 때 사용한다.

## 먼저 확인할 것

1. `Day10_Design_Pattern/Puml/island_game_model_repository.puml`, `island_game_service_layer.puml`, `island_game_design_review_refined.puml`을 읽는다. 과제 3의 보강안이 이후 구현의 목표 구조다.
2. `Day10_Design_Pattern`에는 현재 게임용 구현 코드가 없다. 아래 코드는 **구현 예시**이며 실제 API, DB, 생성자, 비즈니스 규칙이 존재한다고 가정하지 않는다.
3. 실제 데이터 계약과 게임 규칙을 확인한다. `.fig`는 화면 근거이며 API 스키마, 별 산정식, 골든 키 지급 조건, Zone 목표 계산식까지 정의하지 않는다.
4. 한 계층만 수정해도 관련 Mapper, Repository, Service, Test 계약을 함께 점검한다. 기존 파일이나 사용자 변경 사항은 임의로 덮어쓰지 않는다.

## 데이터 흐름과 책임

`외부 API/DB → DataSource → DTO → Mapper → Domain Model → Repository → Service → UI`

| 계층 | 담당 | 금지할 일 |
| --- | --- | --- |
| DataSource | 외부 통신, 저장 요청, 원시 응답 수신 | 도메인 모델 생성, 게임 규칙 판정 |
| DTO | 외부 데이터 형식 표현 | 화면용/도메인용 계산 |
| Mapper | DTO와 도메인 모델 사이의 순수 변환 및 형식 검증 | 네트워크 호출, 저장, 보상 지급 |
| Repository | DataSource와 Mapper 조합, 도메인 모델 조회·저장 | 해금·보상·작업 완료 판정 |
| Service | 게임 규칙, 사용 사례, 최종 `Result<T, GameError>` | HTTP/JSON/SQL 직접 처리 |
| Test | 경계·규칙·오류 변환 검증 | 구현을 그대로 복제한 무의미한 단언 |

## 가이드 목록

- [DataSource](guide_datasource.md)
- [Repository](guide_repository.md)
- [Test](guide_test.md)
- [DTO](guide_dto.md)
- [Mapper](guide_mapper.md)
- [Result 패턴](guide_result.md)

각 파일은 적용 범위, 규칙, Good 예시, Bad 예시, 기존 학습 코드에서 확인한 교훈을 담는다. 코드 블록은 설명용이며 `Day10_Design_Pattern`의 실행 가능한 구현이라고 주장하지 않는다.
