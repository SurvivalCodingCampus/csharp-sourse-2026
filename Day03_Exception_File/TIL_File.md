# Day 03 TIL

- 이름: `<박성연>`
- 작성일: `<2026-08-31>`

## 1. 오늘 막힌 부분 또는 내린 판단

`<써야 할 file 복사 함수를 알아야 할 것 같아 찾다가 폴더 자체 복사 로직도 알게 되었습니다..>`

## 2. 수정 전과 수정 후

### 수정 전

```csharp
void CopyFIle(string sourceFilePath, string destinationFilePath)
    {
        File.Copy(sourceFilePath, destinationFilePath, true);
        
        
    }
```

### 수정 후

```csharp
void CopyFIle(string sourceFilePath, string destinationFilePath)
    {
        if (File.Exists(sourceFilePath))
        {
            // 파일 생성
            File.Copy(sourceFilePath, destinationFilePath, true);
            Debug.Log("File copied successfully!");
        }
        else
        {
            Debug.LogError("File not found at path: " + sourceFilePath);
        }
        
    }
```



## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: `<사용안함>`



## 4. 검증 결과

- 빌드: `<성공 / 실패>`
- 실행 결과: `<확인한 동작>`
- 추가로 확인한 내용: `<테스트 또는 예외 상황>`

## 5. 아직 궁금한 점

`<해결하지 못했거나 더 알아보고 싶은 내용>`

## 6. 다음에 적용할 것

`<다음 코딩에서 직접 적용할 한 가지>`