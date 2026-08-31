# Day 02 TIL

# 2026-08-25 [람다식과 함수형 프로그래밍]

## 오늘 배운 내용
- LINQ의 [Where], [OrderBy], [Select], [ToHashSet]([Distinct]), [Any], [Max], [Min] 등 메서드 문법 사용법을 익힘
- 람다식('t => t.Value')을 활용해 컬렉션을 필터링/정렬/변환하는 방법을 연습
- Trader-Transaction처럼 연관된 객체를 다룰 때 [t.Trader.City], [t.Trader.Name]처럼 체이닝해서 접근하는 방식을 익힘

## 기억할 것
- [Where]는 조건에 맞는 요소만 필터링, [Select]는 원하는 필드만 뽑아서 변환(SQL의 SELECT와 비슷)
- [OrderBy] 는 오름차순, 내림차순은 [OrderByDescending]
- 중복 제거가 필요할 땐 원래 [ToHashSet()]을 쓰려고 했는데, SQL에서 통용되는 [Distinct()]가 동일하게 동작하는지 궁금해서 테스트 삼아 써봄 → 정상 작동 확인
- [Any()]는 조건에 맞는 요소가 하나라도 있는지 bool로 반환 (존재 여부 체크에 유용)

## 어려웠던 점
- SQL과 개념이 비슷해서('Where'=WHERE, 'Select'=SELECT, 'OrderBy'`=ORDER BY) 전체적인 흐름은 쉽게 이해됨
- 다만 SQL은 [SELECT → WHERE → ORDER BY] 순서인데 LINQ 메서드 체이닝은 [Where → OrderBy → Select] 순으로 써서 순서가 헷갈렸음

## 해결 방법
- SQL 문장을 먼저 떠올린 다음 LINQ로 순서를 바꿔서 매핑해보는 방식으로 연습
- 짧은 예제를 직접 콘솔에 출력해보며 각 메서드가 언제 실행되고 어떤 타입을 반환하는지 눈으로 확인