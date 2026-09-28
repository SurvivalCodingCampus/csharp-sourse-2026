# Service 계층 도입의 분리 효과 (핵심 요약)

## 1. 단일 책임 분리

### Repository vs Service의 책임 명확화

**Repository의 책임**
- "데이터를 어떻게 가져오는가"만 알고 있음
- DB에서 조회/저장하는 방법만 담당
- 비즈니스 규칙은 전혀 모름

**Service의 책임**
- "그 데이터로 무엇을 해도 되는가"만 안다
- 비즈니스 판정 로직만 담당

### 구체적 예시: GameTaskService

#### Repository (GameTaskRepository)
```csharp
// GameTaskRepository는 이것만 함
+ FindTasksByZoneId(zoneId: Guid): Task<List<GameTask>>
+ FindTaskById(taskId: Guid): Task<GameTask>
+ SaveTask(task: GameTask): Task

// energyCost가 뭔지 알 필요 없음 ✓
// "에너지 검사는 왜 필요한지" 모름 ✓
```

#### Service (GameTaskService)
```csharp
// Service에서 비즈니스 규칙을 담당
+ StartTask(playerId: Guid, taskId: Guid): Result<GameTask, ServiceError>

// 내부 로직:
1. Task 조회 (Repository)
2. Player의 에너지 확인 (PlayerRepository)
3. "에너지가 부족하면 시작 못 한다" 판단 ✓ (Service만 함)
4. 에너지 차감 및 Task 상태 변경
5. 결과를 Result<T>로 반환
```

---

## 2. 오케스트레이션의 가시화

### GameSession 종료 후 일어나는 일련의 흐름

#### 개선 전 (Repository Only)
```
GameSession을 이겼을 때 무엇이 일어나는가?
├─ 별점 계산 (어디서?)
├─ Level 갱신 (어디서?)
├─ 골든키 진행도 갱신 (어디서?)
└─ 코인 지급 (어디서?)

→ 코드를 여러 곳 뒤져봐야 함. 흐름이 명확하지 않음. ❌
```

#### 개선 후 (Service Layer)
```
GameSessionService.FinishSession()

┌─────────────────────────────────────┐
│  GameSessionService                 │
│  (게임이 끝났을 때)                   │
└──────────────┬──────────────────────┘
               │
      ┌────────┴────────┬──────────────┬────────────────┐
      ↓                 ↓              ↓                ↓
   LevelService    GoldenKeyService  PlayerService    (완료)
   (별점 기록)     (키 진행도 갱신)   (코인 지급)
```

**다이어그램에서 명확히 드러남:**
- GameSessionService의 의존 화살표
  ```
  GameSessionService ..> LevelService : 승리 시, 클리어 결과 기록 >
  GameSessionService ..> GoldenKeyService : 승리 시, 골든키 진행도 갱신 >
  GameSessionService ..> PlayerService : 승리 시, 코인 지급 >
  ```

**효과:**
- 다이어그램만 봐도 흐름이 한눈에 보임 ✓
- 새로운 팀원도 이해하기 쉬움 ✓
- 로직 추가/변경 시 변경 범위가 명확함 ✓

---

## 3. Repository 교체 자유도 증가

### 저장소 기술 변경 시 영향도

#### 개선 전
```
SQLite (로컬)
   ↓
Service + Repository 혼재된 로직
   ↓
Firestore (클라우드)로 변경하려면?
   → Service 로직도 함께 수정해야 함 ❌
   → 비즈니스 코드를 건드림 (위험)
```

#### 개선 후
```
Repository는 순수 I/O 인터페이스

interface PlayerRepository {
  Task<Player> FindById(Guid playerId);
  Task SavePlayer(Player player);
}

SQLite 구현체          Firestore 구현체
─────────────          ──────────────
- DB 연결              - Firestore SDK
- 조회 쿼리            - 컬렉션 접근
- 저장 로직            - 문서 저장 로직

Service의 비즈니스 로직은 변경 없음 ✅
```

**효과:**
- Repository 구현체만 교체 가능
- Service 코드는 전혀 손댈 필요 없음
- 테스트 환경에서는 Mock Repository 사용 가능

---

## 4. 에러 처리 일관성

### 모든 Service가 동일한 응답 형식

#### 일관된 Result 패턴
```csharp
// 모든 Service 메서드가 동일한 형태
Result<Player, ServiceError> SpendCoins(...);
Result<Level, ServiceError> UnlockLevel(...);
Result<GameSession, ServiceError> FinishSession(...);
```

#### UI/ViewModel 계층에서 처리
```csharp
// 패턴이 일관되므로 하나로 통일 가능
if (result.WasSuccessful) {
  var player = result.GetValueOrNull();
  RefreshUI(player);
} else {
  var error = result.GetErrorOrNull();
  ShowError(GetErrorMessage(error.Reason));
}

// 모든 기능의 성공/실패를 동일하게 다룸 ✓
```

**효과:**
- UI 코드 패턴이 일관됨
- 에러 처리를 빠뜨릴 수 없음 (타입 강제)
- 새로운 기능 추가 시 자동으로 같은 패턴 따름

---

## 5. 트레이드오프: 보일러플레이트 증가

### Service 계층 추가의 비용

#### 코드 증가
```
이전: Repository만 존재 (10개)
이후: Repository (10개) + Service (9개) = 19개

→ 인터페이스 + 구현체 수 증가
→ 각 Service마다 2~3개 메서드씩 정의
```

#### Thin Service 문제
```csharp
// AvatarOptionService 예시
interface AvatarOptionService {
  Result<List<AvatarOption>, ServiceError> ListAvatars();
}

// 단순히 Repository를 감싸기만 함
// 비즈니스 로직이 거의 없음
```

### 의도적 선택: 일관된 경계 유지

```
UI는 항상 Service 계층만 바라본다

┌─────────────────┐
│   UI/ViewModel  │ ← 항상 Service만 호출
└────────┬────────┘
         │
    (일관된 경계)
         │
┌────V────────────────┐
│   Service Layer     │ ← AvatarOptionService도 포함
│ (9개 모두 항상 거침)  │
└────┬────────────────┘
      │
┌────V────────────────┐
│ Repository Layer    │
└─────────────────────┘
```

### 프로젝트 규모별 가이드

| 프로젝트 규모 | 권장사항 |
|---|---|
| 매우 작음 (< 5개 기능) | Thin Service 생략 가능, Repository 직접 노출 |
| 소규모 (5~20개 기능) | **현재 상태 유지** (Island 게임) |
| 중규모 (20~50개 기능) | Service 계층 필수 |
| 대규모 (> 50개 기능) | Service 계층 + 추가 응용 서비스 계층 고려 |

**Island 게임 현재 규모**: 9개 Service → **Thin Service 포함의 비용 < 일관성 유지의 이득** ✓

---

## 종합 평가

### 분리 효과 요약

| 효과 | 수준 | 특징 |
|---|---|---|
| 책임 명확성 | ⭐⭐⭐⭐⭐ | 각 계층이 하나의 일만 함 |
| 흐름 가시성 | ⭐⭐⭐⭐⭐ | 다이어그램에 업무 흐름이 드러남 |
| 교체 유연성 | ⭐⭐⭐⭐⭐ | 구현 기술 변경 용이 |
| 에러 처리 | ⭐⭐⭐⭐⭐ | 일관된 패턴으로 처리 |
| 코드 복잡도 | ⭐⭐⭐☆☆ | Thin Service로 약간 증가 |

### 결론

**Service 계층 도입은 초기 복잡도 증가보다 장기적 이득이 훨씬 크다.**

- ✅ 코드의 의도가 명확해짐
- ✅ 새 기능 추가 시 적용할 패턴이 명확함
- ✅ 테스트 코드 작성이 간단해짐
- ✅ 팀 규모 확대 시 온보딩이 쉬워짐
- ✅ 리팩토링이 안전해짐

**특히 Island 게임처럼 지속적으로 기능이 추가되는 프로젝트에서는 필수적인 설계입니다.**
