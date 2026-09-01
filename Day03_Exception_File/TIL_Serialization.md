# Day 03 TIL

- 이름: `<박성연>`
- 작성일: `<2026-08-31>`

## 1. 오늘 막힌 부분 또는 내린 판단

`<써야 할 file 복사 함수를 알아야 할 것 같아 찾다가 폴더 자체 복사 로직도 알게 되었습니다..>`

## 2. 수정 전과 수정 후

### 수정 전

```csharp
public class Department
{
    public string DeptName { get; }
    public Empolyee Name { get; }

    public Department(string deptName, Empolyee name)
    {
        DeptName = deptName;
        this.Name = Name;
    }
    

}


static void Main(string[] args)
    {
        
        //
        Department department = new Department("총무", 41);

        string jsonString = JsonSerializer.Serialize(department);
        
        Employee? loadedUser = JsonSerializer.Deserialize<Employee>(jsonString);
        
        //
        Employee employee = new Employee("홍길동", 41);

        string jsonString = JsonSerializer.Serialize(employee);
        
        Employee? loadedUser = JsonSerializer.Deserialize<Employee>(jsonString);
        
    }
```

### 수정 후

```csharp
public class Department
{
    public string Name { get;  }
    public Employee Leader { get;  }

    public Department(string name, Employee leader)
    {
        Name = name;
        this.Leader = leader;
    }
    
}

static void Main(string[] args)
    {
        
        // 1. 홍길동(41세) Employee 인스턴스 생성
        Employee leader = new Employee("홍길동", 41);

        // 2. 총무부 Department 인스턴스 생성
        Department department = new Department("총무부", leader);

        // 3. Department 객체를 JSON 문자열로 직렬화
        string jsonString = JsonSerializer.Serialize(
            department,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }
        );

        // 4. JSON 문자열을 company.json 파일에 저장
        File.WriteAllText("company.json", jsonString);

        // 확인
        Console.WriteLine(jsonString);
    }
```



## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용함>`
- 질문: `<다른클래스를 생성자 this로 이어진 클래스의 인스턴스 생성이 익숙하지 않아 문제 전문의 풀이 요구>`
- 제안받은 내용:
  `main 함수 관련 코드, 직렬화에서의 들여쓰기 옵션`
- 채택 또는 거절한 내용: `<AI가 작성한 코드를 채택했습니다./set은 역직렬화시 필요하여 작성하징 않았습니다.>`



## 4. 검증 결과

- 빌드: `<성공 / 실패>`
- 실행 결과: `<확인한 동작>`
- 추가로 확인한 내용: `<테스트 또는 예외 상황>`

## 5. 아직 궁금한 점

`<해결하지 못했거나 더 알아보고 싶은 내용>`

## 6. 다음에 적용할 것

`<다음 코딩에서 직접 적용할 한 가지>`