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
    public class EditLogData : ILogDataEditor
    {
        // 이벤트가 발생할때마다 추가적으로들어가는 DATA 파일위치 경로
        public string receiveDataPath = "D:\\RECEIVE\\";
        // 메인 데이터가 저장될 경로 (년도/날짜/내부 CSV존재 안에서 지속 갱신)
        public string logPath = "D:\\LOG\\";
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


                StreamWriter file;
                file = new StreamWriter(String.Format("{0}{1}.csv", receiveDataPath, receiveFile), false);
                file.WriteLine(msg);
                file.Close();

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
        
    }
}
