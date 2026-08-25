# Day 02 TIL

여기에 템플릿 복사해서 활용할 것

Day 02 TIL

이름: 서인호

작성일: <2026-08-25>

1. 오늘 막힌 부분 또는 내린 판단
    


2. 수정 전과 수정 후
   수정 전
   Console.WriteLine("3. =============");
   transactions.Where(e => e.Trader.City == "Cambridge")
   .Select(e => e.Trader.Name)
   .ToList()
   .ForEach(e => Console.WriteLine(e));


   수정 후
   Console.WriteLine("3. =============");
   transactions.Where(e => e.Trader.City == "Cambridge")
   .Select(e => e.Trader.Name)
   .OrderBy(e => e)
   .Distinct()
   .ToList()
   .ForEach(e => Console.WriteLine(e));

  항목이 중복되고 정렬되지않은채로 나왓엇음

3. AI 사용 여부와 채택, 거절한 이유

   AI 사용 여부: 사용함

   질문: 오름차순 정렬 내림차순 정렬 중복 제거하는 함수는 뭐야

   제안받은 내용: OrderBy OrderByDescending Distinct 

   채택 또는 거절한 내용: 

   판단한 이유: 
  

4. 검증 결과
   빌드: 성공 / 실패
   실행 결과: ![img_1.png](img_1.png)

   추가로 확인한 내용: 



5. 아직 궁금한 점
   <해결하지 못했거나 더 알아보고 싶은 내용>
    줄내림 후 .함수() 이렇게 작성하는데 쭉 붙여써도 되는지
    
    

6. 다음에 적용할 것
   <다음 코딩에서 직접 적용할 한 가지>

    