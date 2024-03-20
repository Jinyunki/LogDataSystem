using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDataSystem.Utiles {
    public interface ILogDataEditor {
        /// <summary>
        /// 파라메터에 맞춰서 csv로그를 남기는 메서드
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="result"></param>
        /// <param name="RunTime"></param>
        /// <param name="tempSet"></param>
        /// <param name="tempRange"></param>
        /// <param name="TimeSet"></param>
        /// <param name="ErrorCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="logPath"></param>
        bool AddLogFile_Csv(int ch, bool result, int RunTime, int tempSet, int tempRange, int TimeSet, int ErrorCode, DateTime startTime, DateTime endTime, string logPath);
        //void AddLogFile_Csv(int ch, bool result, int RunTime, int tempSet, int tempRange, int TimeSet, int ErrorCode, DateTime startTime, DateTime endTime, string logPath);
    }
}
