# Day 01 TIL

# 2026-08-24 [Java 코드 C# 이식]

## 오늘 배운 내용
- Java의 static final 상수가 C#에서는 const 키워드로 단순화된다는 것을 배웠다.
- NUnit 프레임워크를 이용해 단위 테스트를 작성하며 Assert.That과 Is.InRange 같은 검증 메서드 사용법을 공부했다.

## 기억할 것
- C#에서는 식별자 및 메서드 명명 규칙으로 카멜 케이스(selfAid) 대신 파스칼 케이스(SelfAid)를 사용한다.
- System 네임스페이스가 누락되면 ArgumentException 같은 기본 예외 클래스 참조 오류가 발생하므로 상단 using System;을 빼먹으면 안된다.

## 어려웠던 점
- C# 테스트 코드를 작성할 때 필요한 네임스페이스 참조(using System; 등)를 놓쳐 컴파일 에러가 발생했다.
- Rider 단축키(Alt + Enter)로 생성된 메뉴 구조와 NUnit 테스트의 '[TestOf]' 방식이 다소 생소했다.

## 해결 방법
- Rider의 자동 완성 기능과 Alt + Enter Quick Fix를 활용해 부족한 using 구문을 빠르게 추가했다.
- 자주 사용하며 익숙해지는 것 말고는 방법이 없다.
