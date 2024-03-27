using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace LogDataSystem.Utiles
{
    /// <summary>
    /// 로그 데이터를 편집하고 관리하는 클래스입니다.
    /// </summary>
    public class EditLogData : ILogDataEditor
    {
        // 이벤트가 발생할때마다 추가적으로들어가는 DATA 파일위치 경로
        public string receiveDataPath = "D:\\RECEIVE\\";
        // 메인 데이터가 저장될 경로 (년도/날짜/내부 CSV존재 안에서 지속 갱신)
        public string logPath = "D:\\LOG\\";

        /// <summary>
        /// CSV 파일에 로그 데이터를 추가합니다.
        /// </summary>
        /// <param name="ch">채널 번호</param>
        /// <param name="result">결과</param>
        /// <param name="RunTime">실행 시간</param>
        /// <param name="tempSet">설정된 온도</param>
        /// <param name="tempRange">온도 범위</param>
        /// <param name="TimeSet">설정 시간</param>
        /// <param name="ErrorCode">에러 코드</param>
        /// <param name="startTime">시작 시간</param>
        /// <param name="endTime">종료 시간</param>
        /// <param name="logPath">로그 파일 경로</param>
        /// <returns>성공 여부</returns>
        public bool AddLogFile_Csv(int ch, bool result, int RunTime, int tempSet, int tempRange, int TimeSet, int ErrorCode, DateTime startTime, DateTime endTime, string logPath)
        {
            try
            {
                string chNo = (ch + 1).ToString();
                string strResult = result == true ? "PASS" : "NG";

                DateTime tm = DateTime.Now;
                var receiveFile = DateTime.Now.ToString("yyyyMMddHHmmss_" + chNo);
                var logFile = string.Format("{0:D4}_{1:D2}_{2:D2}", tm.Year, tm.Month, tm.Day) + "_" + chNo;

                //해당 경로에 폴더를 생성하는 디렉토리정보 하단 이프문을통하여, 디렉토리경로에 존재하지않을시 폴더 생성
                DirectoryInfo di = new DirectoryInfo(receiveDataPath);
                if (!di.Exists)
                    di.Create();

                StringBuilder builder = new StringBuilder();
                //생성된 시간
                builder.Append(tm.ToString("yyyy-MM-dd HH:mm:ss")).Append(",");

                builder.Append(chNo).Append(",");
                builder.Append(strResult).Append(",");
                builder.Append(RunTime).Append(",");
                builder.Append(tempSet).Append(",");
                builder.Append(tempRange).Append(",");
                builder.Append(TimeSet).Append(",");
                builder.Append(ErrorCode).Append(",");
                builder.Append(startTime.ToString("yyyy-MM-dd HH:mm:ss")).Append(",");
                builder.Append(endTime.ToString("yyyy-MM-dd HH:mm:ss")).Append(",");
                builder.Append((endTime - startTime).TotalSeconds);
                string msg = builder.ToString();

                // 수신된 데이터를 CSV 파일에 씁니다.
                StreamWriter file;
                file = new StreamWriter(String.Format("{0}{1}.csv", receiveDataPath, receiveFile), false);
                file.WriteLine(msg);
                file.Close();

                // 로그 경로 설정
                var tmLog = DateTime.Now;
                di = new DirectoryInfo(logPath);
                if (!di.Exists)
                    di.Create();
                di = new DirectoryInfo(di.FullName + "\\" + tmLog.Year.ToString("D4"));
                if (!di.Exists)
                    di.Create();
                di = new DirectoryInfo(di.FullName + "\\" + tmLog.Month.ToString("D2"));
                if (!di.Exists)
                    di.Create();

                var dch = new DirectoryInfo(di.FullName + "\\");
                if (!dch.Exists)
                    dch.Create();

                string title = "DateTime,CH,Result,Run Time,Set Temp,Temp Range,Set Time,Error Code,Start Time,End Time,Run Seconds";

                // CSV 파일이 존재하지 않으면 헤더와 데이터를 씁니다.
                if (!File.Exists(string.Format("{0}{1}.csv", dch.FullName + "\\", logFile)))
                {
                    file = new StreamWriter(string.Format("{0}{1}.csv", dch.FullName + "\\", logFile), true);
                    file.WriteLine(title);
                    file.WriteLine(msg);
                    file.Close();
                } else
                {
                    file = new StreamWriter(string.Format("{0}{1}.csv", dch.FullName + "\\", logFile), true);
                    file.WriteLine(msg);
                    file.Close();
                }

                return true;
            } catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }
        public bool AddLogFile_Csv(int ch, string result, int RunTime, int tempSet, int tempRange, int tempValue, int TimeSet, int ErrorCode, DateTime startTime, DateTime endTime, string EqStatus) {

            try {
                string msg = "";
                string chNo = (ch + 1).ToString();
                string strResult = result;

                DateTime tm = DateTime.Now;
                var mesFile = DateTime.Now.ToString("yyyyMMddHHmmss_" + chNo);

                DirectoryInfo di = new DirectoryInfo(receiveDataPath);

                if (!di.Exists)
                    di.Create();
                msg = msg.AddData(
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    chNo,
                    strResult,
                    RunTime.ToString(),
                    tempSet.ToString(),
                    tempRange.ToString(),
                    tempValue.ToString(),
                    TimeSet.ToString(),
                    ErrorCode.ToString(),
                    startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    endTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    (endTime - startTime).TotalSeconds.ToString(),
                    EqStatus
                    );

                // 해당 경로의 위치에 해당 .csv File이 있는 지 여부 체크
                StreamWriter file = new StreamWriter(string.Format("{0}{1}.csv", receiveDataPath, mesFile), false);
                file.WriteLine(msg);
                file.Close();

                // 현재의 시간
                var tmLog = DateTime.Now;

                // PathLog 경로/지정File명 생성
                di = new DirectoryInfo(logPath);
                if (!di.Exists)
                    di.Create();

                // 현재 년도 파일 생성
                di = new DirectoryInfo(di.FullName + "\\" + tmLog.Year.ToString("D4"));
                if (!di.Exists)
                    di.Create();

                // 현재 월단위 파일 생성
                di = new DirectoryInfo(di.FullName + "\\" + tmLog.Month.ToString("D2"));


                if (!di.Exists)
                    di.Create();

                // 파일 경로가 모두 생성된 곳을 지정하기 위한 위치Target
                var dch = new DirectoryInfo(di.FullName + "\\");
                if (!dch.Exists)
                    dch.Create();

                // .csv FileName
                var logFile = string.Format("{0:D4}_{1:D2}_{2:D2}", tm.Year, tm.Month, tm.Day) + "_" + chNo;
                // .csv File의 헤더 컬럼
                string title = "DateTime,CH,Result,Run Time,Set Temp,Temp Range,Current Value,Set Time,Error Code,Start Time,End Time,Run Seconds,Event";

                // 해당 경로의 위치에 해당 .csv File이 있는 지 여부 체크
                if (!File.Exists(string.Format("{0}{1}.csv", dch.FullName + "\\", logFile))) {
                    // 경로에 .csv File이 없다면
                    // 해당 파일을 새로운 인스턴스를 통하여 생성하고, 있다면 업데이트
                    // 현재로직에서는 신규 생성을 진행
                    file = new StreamWriter(string.Format("{0}{1}.csv", dch.FullName + "\\", logFile), true);
                    // 헤더 Data 업데이트
                    file.WriteLine(title);
                    // msg Data 업데이트
                    file.WriteLine(msg);
                    // 종료
                    file.Close();

                } else {
                    // 경로에 .csv File이 있으면
                    // 해당 파일을 새로운 인스턴스를 통하여 생성하고, 있다면 업데이트
                    // 현재로직에서는 기존 File에 Data 업데이트
                    file = new StreamWriter(string.Format("{0}{1}.csv", dch.FullName + "\\", logFile), true);
                    // Title이 존재하기때문에 추가적으로 msg Data만 업데이트
                    file.WriteLine(msg);
                    // 종료
                    file.Close();
                }
                return true;
            } catch (Exception ex) {
                ex.ToString().Debug();
                return false;
            }
        }
    }
}
