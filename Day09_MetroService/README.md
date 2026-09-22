# 서울 지하철 실시간 도착정보
.NET 10 콘솔 프로젝트. Newtonsoft.Json 사용, DTO 속성은 모두 nullable.
구조: DataSource → DTO → Repository/Mapper → Metro → Program.
기존 Reposetory 폴더 철자는 유지했습니다.

## 실행
`dotnet run --project Day09_MetroService.csproj`
인증키가 없으면 sample 키로 서울역 최대 5건을 조회합니다.
다른 역은 PowerShell에서 환경 변수를 설정한 뒤 실행하세요:
```powershell
$env:SEOUL_SUBWAY_API_KEY = '발급받은 키'
dotnet run --project Day09_MetroService.csproj -- 강남
```
실행할 때마다 최신 정보를 한 번 조회합니다. 정식 키는 최대 100건을 요청합니다.
역 이름은 API에 등록된 이름을 사용합니다(서울역은 '서울').
HTTP 상태 오류와 HTTP 200 응답의 API 오류 코드를 각각 분류합니다.
요청 제한시간은 10초입니다. 도착 초는 데이터 생성 시각(한국시간)으로 보정해 표시합니다.
0초 또는 누락된 도착 초는 즉시 도착으로 단정하지 않습니다.
인증키는 저장소에 기록하지 마세요.

데이터 출처 및 샘플 제한:
https://data.seoul.go.kr/dataList/OA-12764/A/1/datasetView.do
