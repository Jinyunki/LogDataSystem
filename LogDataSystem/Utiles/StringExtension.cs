using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogDataSystem.Utiles {
    
    public static class StringExtension {
        // 확장 메서드
        public static string Log(this string str) {
            Console.WriteLine("========== Check Value Test ==========");
            Console.WriteLine("Input Value : " + str);
            Console.WriteLine("Debug Time  : " + DateTime.Now);
            return str;
        }

        public static string Debug(this string str, [System.Runtime.CompilerServices.CallerMemberName] string caller = "") {
            StackFrame stackFrame = new StackFrame(1, true);
            MethodBase methodBase = stackFrame.GetMethod();
            string methodName = methodBase.Name;

            Trace.WriteLine("==========   Position   ==========");
            Trace.WriteLine("Method Name : " + caller); // 사용된 메서드의 이름 가져오기
            Trace.WriteLine("Input Value : " + str);
            Trace.WriteLine("Debug Time  : " + DateTime.Now);
            return str;
        }

        /// <summary>
        /// += 연산자로 인자내 추가된 데이터를 자동으로 추가하게됨.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string AddData(this string str, params object[] data) {
            for (int i = 0; i < data.Length; i++) {
                str += data[i].ToString();
                if (i < data.Length - 1) // 마지막 요소가 아닌 경우에만 쉼표 추가
                    str += ",";
            }
            return str;
        }
    }
}

