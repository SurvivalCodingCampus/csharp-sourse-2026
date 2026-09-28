1. SRP (단일 책임 원칙) 비교 분석
   과제 1의 한계점 (책임 혼재):

과제 1의 IRestorationTaskRepository.UpdateTaskStatus와 같은 인터페이스는 상태 값 자체를 외부에서 임의로 주입받거나, 구현체 내부에서 "열쇠가 충분한지", "이미 완료된 상태인지" 등의 비즈니스 적합성을 직접 판정해야 하는 구조였습니다.

스테이지 클리어 시에도 보상(코인, 별점, 열쇠) 계산과 다음 레벨 해금 로직이 Repository의 저장 메서드 안팎에 섞여 들어가면서, 영속성 계층(DB/네트워크)이 게임 규칙(도메인 로직)의 변경 이유까지 떠안는 책임 오염이 발생했습니다.

과제 2의 개선점 (Service 도입을 통한 완전 분리):

Repository의 단일 책임: 오직 저장소에 엔티티를 조회하고 쓰는 영속성 I/O 파이프라인(Task<T>, Task) 역할로만 축소되었습니다.

Service의 단일 책임: IRestorationService와 IPuzzleStageService가 유저 지갑의 열쇠 검증, 태스크 완료 처리, 클리어 목표 달성 여부 판정, 재화 차감 및 보상 분배와 같은 비즈니스 트랜잭션 오케스트레이션을 전담합니다.

2. DIP / SDP (의존성 역전 및 안정성 원칙) 검토
   DIP (Dependency Inversion Principle):

상위 정책을 다루는 IPuzzleStageService, IRestorationService는 데이터베이스나 원격 서버의 저수준 구현 클래스를 참조하지 않고, IWalletRepository, IRestorationTaskRepository 등의 추상 인터페이스에만 의존합니다.

UI/프레젠테이션 계층 역시 구체 클래스가 아닌 Service 인터페이스를 주입받아 사용하므로 결합도가 제거되었습니다.

SDP (Stable Dependencies Principle):

변경 빈도가 높은 세부 구현(SQLite, PlayFab API, JSON 파일 입출력 등 불안정한 컴포넌트)이 변경 빈도가 낮은 도메인 규칙 및 추상 인터페이스(안정한 컴포넌트)를 의존하도록 방향이 올바르게 수립되었습니다.

추상 인터페이스(IRepository, IService)를 도입함으로써 실제 구현 클래스에 대한 직접 의존성은 완전히 제거되었습니다.

3. 에러 처리 : Result 패턴 누락 여부 점검
   검증 결과: 누락 없음 (모든 비즈니스 실패 경로가 명시적 타입으로 캡슐화됨)

상세 구현 점검:

단순 성공/실패(bool)나 런타임 예외(Exception) 대신, 명시적 컨테이너 타입인 Result<T, GameErrorCode>를 전면 적용했습니다.

비즈니스 예외(예: INSUFFICIENT_KEYS, LEVEL_GOAL_NOT_REACHED, TASK_ALREADY_COMPLETED)를 시스템 오류(DB 접속 불가, 네트워크 타임아웃)와 엄격히 분리하여 GameErrorCode 열거형으로 모델링했습니다.

Repository는 실패 시 I/O 레벨 예외를 던지거나 null을 반환할 수 있으나, 이를 조합해 최종 클라이언트로 넘기는 Service 단계에서는 무조건 Result.Success(value) 또는 Result.Failure(errorCode) 형태로 반환하도록 통일했습니다.

4. 구조적 변경에 따른 트레이드오프
   비용 및 복잡도 증가 (단점):

보일러플레이트 코드 증가: 단순 조회 기능(예: 단순 프로필 열람)에서도 Controller/ViewModel → Service → Repository를 거치는 패스스루(Pass-through) 호출이 발생할 수 있습니다.

타입 변환 및 래핑 비용: 단순 반환값 대신 Result<T, E>로 포장하고, 호출부에서 매번 result.isSuccess를 검사하고 언래핑해야 하는 코드 작성이 요구됩니다.

인터페이스 관리 개수 증가: 클래스마다 대응하는 인터페이스(IService, IRepository)와 DTO(예: StageClearReward)의 수가 늘어나 초기 프로젝트 세팅 시 구조적 피로감이 생길 수 있습니다.

유지보수 및 아키텍처 관점의 이점 (장점):

단위 테스트(Unit Test) 용이성 극대화: 비즈니스 규칙을 테스트할 때 실제 DB나 모바일 환경 없이 Mock Repository만 Service에 주입하여 즉시 검증할 수 있습니다.

제어 흐름의 예측 가능성 확보: 예외(try-catch) 기반의 흐름 제어를 배제하여 성능 낭비를 줄이고, 어떤 비즈니스 오류가 발생할 수 있는지 메서드 시그니처(Result<T, GameErrorCode>)만으로 한눈에 파악할 수 있습니다.

저장소 기술 교체의 유연성: 로컬 싱글플레이(JSON/PlayerPrefs) 환경에서 서버 기반 동기화(REST API/gRPC/RDBMS)로 인프라가 변경되더라도, Service 및 도메인 모델 코드는 단 한 줄도 수정할 필요가 없습니다.