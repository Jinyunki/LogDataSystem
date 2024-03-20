using GalaSoft.MvvmLight;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using LogDataSystem.DataModels;
using LogDataSystem.Utiles;
using System.ComponentModel;

namespace LogDataSystem.ViewModel
{
    public class MainViewModel : MainModel
    {

        private BackgroundWorker LogUpdateWorker = new BackgroundWorker(); // 백그라운드 작업자 객체
        public MainViewModel() {
            WorkerAdd(); // 백그라운드 작업 구독 추가
            WindowsButtonEvent(); // 윈도우 버튼 이벤트 구독

            // 로그 업로드 버튼 명령 설정
            LogUploadBtn = new RelayCommand(() => { LogUpdateWorker.RunWorkerAsync(); });
        }

        // Instance 소멸 시 작동
        ~MainViewModel() {
            LogUpdateWorker.CancelAsync();
            LogUpdateWorker.Dispose();
        }

        /// <summary>
        /// Background 작업 구독 관리
        /// </summary>
        private void WorkerAdd() {
            //LOG DATA UPLOAD 구독
            LogUpdateWorker.DoWork += LogUpdateWorker_DoWork;
            LogUpdateWorker.RunWorkerCompleted += LogUpdateWorker_RunWorkerCompleted;
            LogUpdateWorker.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// 어플리케이션윈도우의 축소,사이즈조절,닫기 기본 버튼
        /// </summary>
        private void WindowsButtonEvent() {
            BtnMinmize = new RelayCommand(() => { WindowState = WindowState.Minimized; });
            BtnMaxsize = new RelayCommand(() => { WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal; });
            BtnClose = new RelayCommand(() => { Application.Current.Shutdown(); });
        }

        private void LogUpdateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            Trace.WriteLine("==========   Start   ==========\nMethodName : " + (MethodBase.GetCurrentMethod().Name) + "\n");
            try {
                LogUpdateWorker.CancelAsync();
            } catch (Exception ex) {
                Trace.WriteLine("========== Exception ==========\nMethodName : " + (MethodBase.GetCurrentMethod().Name) + "\nException : " + ex);
                throw;
            }

        }
        /// <summary>
        /// 백그라운드 작업 실행 이벤트 핸들러
        /// </summary>
        private void LogUpdateWorker_DoWork(object sender, DoWorkEventArgs e) {

            Trace.WriteLine("==========   Start   ==========\nMethodName : " + (MethodBase.GetCurrentMethod().Name) + "\n");
            try {
                LogDataEditor = new EditLogData();
                int ch = 1;
                bool result = true;
                int RunTime = 100;
                int tempSet = 30;
                int tempRange = 5;
                int TimeSet = 60;
                int ErrorCode = 0;
                DateTime startTime = DateTime.Now;
                DateTime endTime = DateTime.Now;
                string logPath = "D:\\LOG\\";
                // EditLogData 클래스의 AddLogFile_Csv 메서드를 호출
                LogDataEditor.AddLogFile_Csv(ch, result, RunTime, tempSet, tempRange, TimeSet, ErrorCode, startTime, endTime, logPath);
            } catch (Exception ex) {
                Trace.WriteLine("========== Exception ==========\nMethodName : " + (MethodBase.GetCurrentMethod().Name) + "\nException : " + ex);
                throw;
            }

        }
    }
}