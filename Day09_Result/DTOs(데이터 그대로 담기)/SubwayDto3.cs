namespace Day09_Result;
//using System;
using System.Collections.Generic;

// 1. 최상위 응답 DTO
[Serializable]
public class SubwayDto3 {
    public ErrorMessageDto errorMessage;
    public List<RealtimeArrivalDto> realtimeArrivalList;
}

// 2. 에러 메시지 및 공통 응답 정보 DTO
[Serializable]
public class ErrorMessageDto {
    public int status;
    public string code;
    public string message;
    public string link;
    public string developerMessage;
    public int total;
}

// 3. 개별 지하철 실시간 도착 정보 DTO
[Serializable]
public class RealtimeArrivalDto {
    public string beginRow;
    public string endRow;
    public string curPage;
    public string pageRow;
    public int totalCount;
    public int rowNum;
    public int selectedCount;
    public string subwayId;
    public string subwayNm;
    public string updnLine;        // 상행 / 하행
    public string statnFid;
    public string trainLineNm;     // 도착지 방면 (예: 의정부행 - 시청방면)
    public string subwayHeading;
    public string statnTid;
    public string statnId;
    public string statnNm;         // 지하철역 이름
    public string trainCo;
    public string trnsitCo;        // 환승 노선 수
    public string ordkey;
    public string subwayList;      // 환승 가능한 지하철 노선 ID 목록
    public string statnList;       // 환승 가능한 지하철역 ID 목록
    public string btrainSttus;     // 열차 종류 (일반, 급행 등)
    public string barvlDt;         // 열차 도착 예정 시간 (초 단위)
    public string btrainNo;        // 열차 번호
    public string bstatnId;        // 종착지 지하철역 ID
    public string bstatnNm;        // 종착지 지하철역 이름
    public string recptnDt;        // 데이터 수신 시간
    public string arvlMsg2;        // 첫 번째 도착 메시지 (예: 서울 도착, 4분 후 (삼각지))
    public string arvlMsg3;        // 두 번째 도착 메시지 (현재 위치 역명)
    public string arvlCd;          // 도착 코드 (0:진입, 1:도착, 2:출발, 99:운행중 등)
    public string lstcarAt;        // 막차 여부 (0:아님, 1:막차)
}