using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogDataSystem.Utiles
{
    public class EditLogData
    {
        public string Path { get; set; } = "D:\\LOG\\";

        public void AddLogFile_Csv(int ch, bool result, int RunTime, int tempSet, int tempRange, int TimeSet, int ErrorCode, DateTime startTime, DateTime endTime)
        {
            
            try
            {
                string msg = "";
                string chNo = (ch + 1).ToString();
                string strResult = result == true ? "PASS" : "NG";

                DateTime tm = DateTime.Now;
                var mesFile = DateTime.Now.ToString("yyyyMMddHHmmss_" + chNo);
                var logFile = string.Format("{0:D4}_{1:D2}_{2:D2}", tm.Year, tm.Month, tm.Day) + "_" + chNo;

                DirectoryInfo di = new DirectoryInfo(Path);

                if (!di.Exists)
                    di.Create();

                msg = msg + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",";
                msg = msg + chNo + ",";
                msg = msg + strResult + ",";
                msg = msg + RunTime.ToString() + ",";
                msg = msg + tempSet.ToString() + ",";
                msg = msg + tempRange.ToString() + ",";
                msg = msg + TimeSet.ToString() + ",";
                msg = msg + ErrorCode.ToString() + ",";
                msg = msg + startTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
                msg = msg + endTime.ToString("yyyy-MM-dd HH:mm:ss") + ",";
                msg = msg + (endTime - startTime).TotalSeconds.ToString() + ",";

                StreamWriter file = new StreamWriter(String.Format("{0}{1}.csv", Path, mesFile), false);
                file.WriteLine(msg);
                file.Close();

                var tmLog = DateTime.Now;
                di = new DirectoryInfo(Path);
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
            } catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
