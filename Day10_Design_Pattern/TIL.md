# Day 10 TIL

- 이름: 장종민
- 작성일: 2026-09-27

## 1. 오늘 막힌 부분 또는 내린 판단

Island 게임을 설계하면서 Repository와 Service의 책임을 나누는 기준이 어려웠다.   
게임 규칙은 Service로 분리하고, 과제 3에서는 구체 클래스 의존과 진행도 중복 저장 위험을  
인터페이스와 계산형 `ZoneProgress`로 보완했다.

## 2. 수정 전과 수정 후

### 수정 전

```plantuml
ProgressService ..> WalletService
TaskService ..> ProgressService
GameplayService ..> ProgressService
GameplayService ..> WalletService
```

과제 2에서는 Service끼리 구체 클래스를 직접 참조.  
프로필과 설정, 레벨과 Zone 진행도도 각각 한 Service에 묶여 있었음.

### 수정 후

```plantuml
ProfileService ..|> IProfileService
SettingsService ..|> ISettingsService
LevelProgressService ..|> ILevelProgressService
ZoneProgressService ..|> IZoneProgressService

GameplayService ..> ILevelProgressService
GameplayService ..> IWalletService
ZoneProgressService ..> ITaskRepository : derives ZoneProgress
```

프로필·설정과 레벨·Zone 진행도를 따로 분리.  
Service끼리는 인터페이스로 연결하고, Zone 진행도는 별도 저장 대신 작업 기록으로 계산.  
Repository는 조회·저장, Service는 게임 규칙과 `Result<T, GameError>` 처리 담당.

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용
- 질문: Island 화면을 보고 과제 1~3을 설계하고, 과제 4의 `docs/` 가이드를 작성하는 방법
- 제안받은 내용: Model·Repository 설계, Service 분리, 설계의 문제점 검토, 가이드 작성 순서
- 채택한 내용: Service 책임 분리와 인터페이스 의존, 기존 학습 코드의 경험을 가이드 예시에 활용
- 채택하지 않은 내용: 화면에 없는 별 계산식과 보상 조건, 실제 API·DB 구조를 임의로 정하는 방식
- 판단한 이유: 화면에 없는 내용은 확인할 근거가 부족

## 4. 검증 결과

- 빌드: 성공
- 실행 결과: 과제 1 Model·Repository,  
과제 2 Service,  
과제 3 보강 구조 다이어그램 정상 표시.  
과제 4 `docs/` 가이드 여섯 개와 목차 작성  
- 추가로 확인한 내용: `.fig` 화면과 설계 내용 비교, Repository·Service의 책임과 의존 관계 검토.

## 5. 아직 궁금한 점

- Zone 진행도에 작업 외의 목표도 포함되는지
- 레벨 완료 중 일부 저장이 실패하거나 요청이 중복되면 어떻게 처리해야 하는지

## 6. 다음에 적용할 것

실제 데이터 계약과 게임 규칙이 정해지면 `docs/` 가이드에 맞춰 
DataSource, DTO, Mapper, Repository, Service를 순서대로 구현할 것이다.  
가짜 Repository를 사용해 해금·보상·작업 완료의 성공과 실패를 테스트하고,  
`Result`가 오류를 올바르게 구분하는지도 확인할 것이다.
