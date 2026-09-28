● SRP (단일 책임)
○ 과제 1의 Repository에 데이터 통신 외의 비즈니스 로직이나 데이터 가공 코드가 섞여 있었는지 확인하고, 과제 2에서 Service가 추가되면서 책임이 어떻게 분리되었는지 비교

과제 1 Repository에 섞여 있는 비즈니스 로직/가공 코드

과제 1의 Repository 인터페이스를 메서드 단위로 뜯어보면, 순수 CRUD가 아닌 것들이 있습니다.

IPlayerRepository

updateCurrency(playerId, coins, gems, goldenKeys): void — 문제가 되는 지점은 이름입니다. "update"가 단순 덮어쓰기 저장인지, 아니면 "차감 가능한가/충분한가"를 판정한 뒤 반영하는 것인지 시그니처만으로는 구분이 안 됩니다. 실제 구현체가 여기서 "잔액 부족 검증"까지 하게 되면 재화 증감이라는 비즈니스 규칙이 저장소 계층에 숨게 됩니다.

ITaskRepository

updateTaskProgress(taskId, amount): void — "진행도를 얼마나 올릴지"가 아니라, 이 메서드가 내부적으로 currentProgress + amount를 계산하고 targetProgress와 비교해 isCompleted 여부까지 판단한다면, 이는 가공 로직입니다. Repository는 원래 "이미 결정된 상태를 저장"만 해야 하는데, 이 메서드는 "무엇을 저장할지 계산"하는 책임까지 지고 있습니다.
completeTask(taskId): void — 이름 자체가 판정 결과의 실행입니다. "완료 조건을 만족했는가"라는 도메인 규칙(보상 지급 가능 여부, 이미 완료된 태스크는 재완료 불가 등)이 이 메서드 호출 이전에 어디선가 검증돼야 하는데, 과제 1 구조에서는 그 검증 주체가 없습니다. 결국 이 판정이 Repository 구현체 내부로 스며들 위험이 큽니다.

IGameSessionRepository

endSession(sessionId): void — GameSession.checkResult(): SessionStatus와 역할이 겹칩니다. "세션을 종료 처리한다"는 것이 단순 상태 저장인지, 승패 판정(checkResult의 결과)을 스스로 계산해서 반영하는 것인지 애매합니다.

Entity 쪽도 동일한 문제

Player.addCoins(), LevelProgress.updateResult(), Task.addProgress(), GameSession.checkResult() 같은 엔티티 메서드들도 "규칙에 따라 상태를 바꾼다"는 점에서 사실상 도메인 로직입니다. Repository가 이 엔티티를 그대로 get/save만 한다 해도, 호출부(UI/컨트롤러)가 직접 엔티티 메서드를 호출해 판정을 수행하게 되므로 판정 로직의 소재지가 Repository와 Entity 사이에서 불명확해집니다.

정리하면, 과제 1의 문제는 "Repository가 판정을 한다"기보다 **"어디가 판정을 하는지 계약(인터페이스)만 봐서는 알 수 없다"**는 것입니다. updateCurrency, completeTask처럼 결과를 지시하는 이름의 메서드가 있으면, 구현자마다 검증 로직을 다르게(혹은 누락되게) 넣을 위험이 생깁니다.

과제 2에서 책임이 어떻게 재배치됐는지
항목	과제 1	과제 2
Repository 메서드	updateCurrency, updateTaskProgress, completeTask, endSession 등 판정성 이름 존재	getPlayer, savePlayer, getTask, saveTask, deleteSession처럼 get/save/delete로만 한정
Entity 메서드	addCoins, updateResult, addProgress, checkResult 등 상태 변경 로직 보유	모든 필드만 남기고 메서드 제거 — 순수 데이터 구조
판정 로직 위치	불명확 (Repository 구현체 또는 Entity에 흩어질 소지)	PlayerService.spendCurrency, TaskService.isTaskCompletable/claimReward, LevelService.calculateStars, GameSessionService.evaluateSessionEnd로 한 곳에 모임
실패 표현	없음 (void, 실패 시 동작 불명)	Task<Result<T, ServiceError>>로 성공/실패와 실패 사유를 명시적 타입으로 표현

구체적으로 무엇이 옮겨갔는지

ITaskRepository.completeTask() → TaskService.claimReward() + TaskService.isTaskCompletable()
과제 1에서는 "완료시켜라"는 명령형 메서드 하나였다면, 과제 2에서는 "완료 가능한지 판단"(isTaskCompletable, 순수 판정 함수)과 "보상을 지급하며 완료 처리"(claimReward, 부수효과 + 실패 가능)로 판정과 실행이 분리됐습니다. claimReward가 실패하면 (TASK_NOT_COMPLETABLE, TASK_ALREADY_COMPLETED 등) ServiceError로 사유가 드러납니다.
IPlayerRepository.updateCurrency() → PlayerService.spendCurrency() / grantCurrency()
차감과 지급을 별도 메서드로 나누고, 차감 시 "잔액이 충분한가"라는 검증을 spendCurrency가 전담합니다. Repository의 savePlayer()는 검증이 끝난 최종 Player 객체를 그대로 저장만 합니다.
Task.addProgress() (엔티티 메서드) → TaskService.addProgress() (서비스 메서드)
같은 이름이지만 위치가 이동했습니다. 과제 1에서는 엔티티가 스스로 진행도를 갱신했지만, 과제 2에서는 Service가 Repository로부터 Task를 읽고, 진행도를 계산한 새 값을 만들어 Repository에 저장을 요청하는 흐름으로 바뀝니다.
GameSession.checkResult() / IGameSessionRepository.endSession() → GameSessionService.evaluateSessionEnd() + finishSession()
승패 판정(evaluateSessionEnd, 순수 함수)과 세션 종료 저장(finishSession, 부수효과 + 실패 가능)이 분리됐고, 매칭 알고리즘 자체는 별도의 MatchEvaluator 클래스로 한 번 더 위임됩니다.

핵심 차이 한 줄 요약: 과제 1은 "저장소가 무엇을 하는지" 메서드 이름만으로는 판정 포함 여부를 알 수 없었지만, 과제 2는 Repository = get/save/delete만, Service = 판정 후 Result 반환이라는 규칙이 타입 시그니처 수준에서 강제되어, 코드를 읽는 사람이 계약만 보고도 각 계층의 책임 범위를 확정할 수 있게 됐습니다.

● DIP / SDP (의존성 원칙)
○ 구현 클래스에 직접 의존하는 부분이 남아있는지, 인터페이스를 통해 결합도가 낮아졌는지 검토

과제 1 (Repository만 있는 구조)

Service가 없으므로, "누가 Repository를 호출하는가"가 다이어그램에 드러나 있지 않습니다. 실제로는 UI/컨트롤러가 IPlayerRepository, ITaskRepository 등을 직접 호출해야 하는데, 이 경우 두 가지 문제가 생깁니다.

UI가 여러 Repository를 조합해서 비즈니스 규칙(예: "재화가 충분하면 차감하고 태스크를 완료 처리한다")을 직접 구현하게 되면, UI 계층이 도메인 규칙을 알아야 합니다. 이는 인터페이스에 의존하더라도 의존의 방향은 지켰지만 의존의 "위치"가 잘못된 경우입니다 — 저수준 정책(재화 검증, 완료 조건)이 고수준 모듈(UI)에 새어 들어갑니다.
GameSession.checkResult()처럼 엔티티가 자체적으로 판정 로직을 가지면, 이 엔티티를 사용하는 모든 곳(UI, Repository 구현체)이 암묵적으로 엔티티의 내부 규칙에 의존하게 됩니다. 엔티티는 안정적이어야 할 "정책의 핵심"에 가까운데, 여기에 가변적인 비즈니스 규칙이 섞이면 안정성이 떨어집니다.

Repository 자체는 인터페이스이므로 구현 클래스에 대한 직접 의존은 없지만, 인터페이스 뒤에 무엇이 있는지(판정 로직 포함 여부)가 계약에 드러나지 않는다는 점이 DIP의 취지(안정된 추상화에 의존)를 약화시킵니다.

과제 2 (Service 추가 구조)

UI → Service (interface 없이 클래스로만 존재) → Repository (interface) → Entity
PlayerService, LevelService, TaskService, GameSessionService가 모두 Repository를 인터페이스로만 참조합니다 (-playerRepo: IPlayerRepository). 구현 클래스(로컬 저장, 서버 API 등)에 대한 직접 참조가 없으므로, 저장소 구현을 교체해도 Service 코드는 변경되지 않습니다 — DIP가 명확히 지켜집니다.
다만 한 가지 남은 문제: 현재 다이어그램에서 PlayerService, LevelService 등은 인터페이스가 아니라 구체 클래스로 선언되어 있고, GameSessionService가 LevelService와 PlayerService를 구체 클래스 타입으로 직접 참조합니다. 이는 Service-to-Service 의존에서는 DIP가 적용되지 않았다는 뜻입니다. 엄밀히 하려면 ILevelService, IPlayerService 같은 인터페이스를 추가로 두고 그걸 참조하게 해야 완전한 DIP가 됩니다.
SDP(안정된 의존성 원칙) 관점에서는, Entity(가장 안정적, 의존받기만 함) → Repository Interface(안정적) → Service(상대적으로 변동성 높음, 비즈니스 규칙 자주 변경) → UI(가장 변동성 높음) 순서로 의존 방향이 안정된 쪽을 향하고 있어, 과제 1보다 원칙에 부합합니다.

● 에러 처리 : Result 패턴 누락 여부

과제 1

모든 Repository 메서드가 void 또는 값 자체를 반환하며, 실패 시 무슨 일이 일어나는지 전혀 정의되어 있지 않습니다. 예를 들어 IPlayerRepository.updateCurrency()가 잔액 부족으로 실패하면? 예외를 던지는지, 조용히 무시하는지 알 수 없습니다.
Task.addProgress(), GameSession.checkResult() 같은 엔티티 메서드도 반환 타입이 void 또는 단순 enum이라, "이 진행도 추가가 유효한 것이었는가"를 호출부가 확인할 방법이 없습니다.
Result 패턴이 완전히 누락된 상태입니다.

과제 2

Repository: Task<T> / Task(비동기)까지만 적용하고, 의도적으로 Result를 씌우지 않았습니다. Repository는 판정을 하지 않는 순수 I/O이므로, 실패는 인프라 예외(네트워크 오류, 파일 없음 등)로 처리하는 것이 적절하다는 설계 판단입니다.
Service: 판정이 필요한 모든 public 메서드가 Task<Result<T, ServiceError>>를 반환합니다. ServiceError enum(INSUFFICIENT_CURRENCY, LEVEL_LOCKED, TASK_ALREADY_COMPLETED 등)으로 실패 사유가 타입 수준에 명시되어, 호출부가 실패 종류별로 분기할 수 있습니다.
남은 누락 지점: LevelService.calculateStars(), TaskService.isTaskCompletable(), GameSessionService.evaluateSessionEnd() 세 메서드는 Result가 아닌 일반 값을 반환합니다. 이는 의도적 설계(입력이 유효하면 항상 성공하는 순수 계산 함수)이지만, 검토 시 "왜 이 세 개만 예외인가"를 명시적으로 설명할 수 있어야 지적받지 않습니다 — 부수효과가 없고 실패 케이스가 존재하지 않는 함수라는 근거가 필요합니다.

● 구조적 변경에 따른 트레이드오프
○ 계층 분리로 인해 코드가 복잡해진 부분은 없는지, 유지보수 관점에서 어떤 이점이 생겼는지 정리

복잡해진 부분

클래스/인터페이스 개수가 6개(Entity+Repository)에서 6개 Entity + 6개 Repository + 5개 Service + Result + ServiceError로 거의 두 배 늘었습니다. 작은 기능(예: 닉네임 변경) 하나에도 PlayerService.changeNickname() → IPlayerRepository.getPlayer() → 검증 → savePlayer()까지 호출 경로가 한 단계 늘어납니다.
Task<Result<T, ServiceError>>라는 중첩 제네릭 반환 타입은 호출부(UI)에서 await와 Result 언패킹을 함께 처리해야 해서, 단순 조회 하나에도 보일러플레이트가 늘어납니다.
Service 간 의존(TaskService → PlayerService, GameSessionService → LevelService, PlayerService)이 생기면서, Service 계층 내부에도 호출 순서/조합 관계를 파악해야 하는 복잡도가 추가됩니다.

유지보수 관점 이점

단일 변경 지점: 별점 계산 공식이 바뀌면 과제 1은 LevelProgress.updateResult()(엔티티)와 이를 호출하는 모든 UI 코드를 다 찾아야 할 수 있지만, 과제 2는 LevelService.calculateStars() 한 곳만 수정하면 됩니다.
저장소 교체 안전성: 로컬 저장(PlayerPrefs)에서 서버 API로 바꿀 때, 과제 1은 판정 로직이 Repository 구현체에 있을 경우 로직 자체를 새 구현체에 복제해야 하지만, 과제 2는 Repository 구현체만 교체하면 Service의 규칙은 그대로 재사용됩니다.
테스트 격리: Service 단위 테스트 시 Repository를 Mock으로 대체하면 "재화 부족 시 실패하는가", "이미 완료된 태스크는 재완료 안 되는가" 같은 규칙만 순수하게 검증 가능합니다. 과제 1 구조에서는 이런 검증이 Repository 구현체(DB/네트워크 의존)까지 함께 띄워야 테스트되는 경우가 많습니다.
실패 처리 명시성: UI가 Result.error를 보고 INSUFFICIENT_CURRENCY면 "코인 부족" 팝업을, LEVEL_LOCKED면 "잠긴 레벨" 팝업을 띄우는 식으로 분기 가능해져, 과제 1처럼 실패 상황을 임기응변으로 처리(null 체크, try-catch 남발)하지 않아도 됩니다.

종합: 복잡도는 초기 구현 비용으로 늘었지만, 이는 "규칙이 자주 바뀌는 게임 로직(별점 공식, 재화 정책, 태스크 보상 조건)"을 안전하게 격리하기 위한 비용입니다. 트레이드오프를 정리하면 — 단기 개발 속도는 과제 1이 유리, 장기 유지보수·테스트·저장소 교체는 과제 2가 유리합니다.