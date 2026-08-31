# Day 01 TIL

- 이름: `<박성연>`
- 작성일: `<2026-08-25>`

## 1. 오늘 막힌 부분 또는 내린 판단

`<고계 함수를 사용할 때 순서나 옵션값에 주의했습니다.>`

## 2. 수정 전과 수정 후

### 수정 전

```csharp
 // 1. 2011년에 일어난 모든 트랜잭션을 찾아 가격 기준 오름차순으로 정리하여 이름을 나열하시오
        
        Console.WriteLine("1. ========================== ");
        transactions.Where(e  => e.Year == 2011) //IEnumerable<Transaction>
            .OrderBy(e => e.Value) //IOrderedEnumerable<Transaction>
            .ToList() //List<Transaction>
            .ForEach(e => Console.WriteLine(e.Trader. Name)) ;
        
        // 2. 거래자가 근무하는 모든 도시를 중복 없이 나열하시오
        
        Console.WriteLine("2. ========================== ");
        transactions.Where(e  => e.Trader.City) 
            .ToHashSet()
            .ToList()
            .ForEach(e  => Console.WriteLine(e.Trader.City)) ;
        
        // 3. 케임브리지에서 근무하는 모든 거래자를 찾아서 이름순으로 정렬하여 나열하시오
        
        Console.WriteLine("3. ========================== ");
        transactions.Where(e  => e.City == "Cambridge") 
            .ToHashSet()
            .ToList();
        
        // 4. 모든 거래자의 이름을 알파벳순으로 정렬하여 나열하시오
        Console.WriteLine("4. ========================== ");
        transactions.Where(e  => e.Name) 
            .OrderBy(e  => e.Trader. Name) //알파벳순?
            .ToList().ForEach(e  => Console.WriteLine(e.Trader. Name)) ;
        
        // 5. 밀라노에 거래자가 있는가?
        Console.WriteLine("5. ========================== ");
        bool result = transactions.Any(e  => e.City == "Milan");
        
        // 6. 케임브리지에 거주하는 거래자의 모든 트랙잭션값을 출력하시오
        Console.WriteLine("6. ========================== ");
        transactions.Where(e  => e.City == "Cambridge")
            .ToList()
            .ForEach(e  => Console.WriteLine(e.Trader. Name));
        
        // 7. 전체 트랜잭션 중 최대값은 얼마인가?
        Console.WriteLine("7. ========================== ");
        transactions.Where(e  => e.Value)
            .Aggregate((max, next) => Math.Max(max, next))
            .ForEach(Console.WriteLine);

        
        // 8. 전체 트랜잭션 중 최소값은 얼마인가?
        Console.WriteLine("8. ========================== ");
        // transactions.Aggregate((e1.Value, e2.Value) => Math.Min(e1.Value, e2.Value))
        //     .ForEach(Console.WriteLine);
        transactions.Where(e  => e.Value)
            .Aggregate((min, next) => Math.Min(min, next))
            .ForEach(Console.WriteLine);
```

### 수정 후

```csharp
        // 2. 거래자가 근무하는 모든 도시를 중복 없이 나열하시오

Console.WriteLine("2. ========================== ");

transactions
    .Select(e => e.Trader.City)
    .Distinct()
    .ToList()
    .ForEach(e => Console.WriteLine(e));


// 3. 케임브리지에서 근무하는 모든 거래자를 찾아서 이름순으로 정렬하여 나열하시오

Console.WriteLine("3. ========================== ");

transactions
    .Where(e => e.Trader.City == "Cambridge")
    .Select(e => e.Trader)
    .DistinctBy(e => e.Name)
    .OrderBy(e => e.Name)
    .ToList()
    .ForEach(e => Console.WriteLine(e.Name));


// 4. 모든 거래자의 이름을 알파벳순으로 정렬하여 나열하시오

Console.WriteLine("4. ========================== ");

transactions
    .Select(e => e.Trader.Name)
    .Distinct()
    .OrderBy(e => e)
    .ToList()
    .ForEach(e => Console.WriteLine(e));


// 5. 밀라노에 거래자가 있는가?

Console.WriteLine("5. ========================== ");

bool result = transactions
    .Any(e => e.Trader.City == "Milan");

Console.WriteLine(result);


// 6. 케임브리지에 거주하는 거래자의 모든 트랜잭션값을 출력하시오

Console.WriteLine("6. ========================== ");

transactions
    .Where(e => e.Trader.City == "Cambridge")
    .ToList()
    .ForEach(e => Console.WriteLine(e.Value));


// 7. 전체 트랜잭션 중 최대값은 얼마인가?

Console.WriteLine("7. ========================== ");

int maxValue = transactions.Max(e => e.Value);

Console.WriteLine(maxValue);


// 8. 전체 트랜잭션 중 최소값은 얼마인가?

Console.WriteLine("8. ========================== ");

int minValue = transactions.Min(e => e.Value);

Console.WriteLine(minValue);
```

`<교수님의 예시와 장표를 보고 제가 작성한 후 AI의 도움을 받았습니다.>`

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: `<코드 수정>`
- 제안받은 내용:
  `<수정 후와 같은 코드, 전체적으로 기억하면 좋은 LINQ 함수: Where()      // 조건에 맞는 데이터 필터링
Select()     // 원하는 데이터 추출/변환
Distinct()   // 중복 제거
OrderBy()    // 오름차순 정렬
Any()        // 하나라도 존재하는가?
Max()        // 최대값
Min()        // 최소값, where과 select의 차이: // Where: 데이터를 "걸러낸다"
  transactions.Where(e => e.Year == 2011);

// Select: 데이터에서 원하는 값을 "뽑아낸다"
transactions.Select(e => e.Trader.Name);>`

AI 대화 전문을 붙이지 말고 질문, 판단, 검증 내용을 요약합니다.

