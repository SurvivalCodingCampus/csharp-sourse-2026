# Day 01 TIL

여기에 템플릿 복사해서 활용할 것

Day 03 TIL

이름: 서인호

작성일: <2026-08-31>

1. 오늘 막힌 부분 또는 내린 판단

   Program 3.cs를 실행했음에도 json파일이 안 만들어짐 

 AI한테 물어봐도 동일함 이유를 모르겟음


2. 수정 전과 수정 후
   수정 전


   수정 후



3. AI 사용 여부와 채택, 거절한 이유

   AI 사용 여부: 사용함

   질문: json파일이 생성되지 않아

   제안받은 내용:
   string fileName = "company.json";
   File.WriteAllText(fileName, jsonString);

// 생성된 진짜 경로 출력
string fullPath = Path.GetFullPath(fileName);
Console.WriteLine($"파일 저장 완료!");
Console.WriteLine($"저장된 위치: {fullPath}");
로 수정해봐
ii bin/Debug/net10.0으로 실행해보기

   채택 또는 거절한 내용:
   

   판단한 이유: 해도 나오지 않음
  

4. 검증 결과
   빌드: 성공 / 실패
   실행 결과: X
   추가로 확인한 내용:



5. 아직 궁금한 점
   


6. 다음에 적용할 것
