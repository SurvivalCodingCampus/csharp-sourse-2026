# Day 03 TIL

# 2026-08-25 [Exceptions]

## 오늘 배운 내용
- XML과 JSON의 구조 차이(태그 vs key-value)를 비교하고, C#의 int.Parse()에서 발생하는 FormatException을 실습했다.

## 기억할 것
- int.Parse()는 소수점 문자열도 파싱하지 못해 예외가 발생하며, try-catch로 안전하게 처리할 수 있다.

## 어려웠던 점
- 인터페이스(IFileCopier)를 구현하는 클래스에서 어떤 예외 상황들을 catch로 나눠야 할지 판단하기 어려웠다.

## 해결 방법
- File.Copy() 사용 시 발생 가능한 FileNotFoundException, IOException, UnauthorizedAccessException을 각각 catch로 구분해서 처리했다.