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
        private ILogDataEditor LogDataEditor { get; set; }
        public ICommand LogUploadBtn { get; set; }
        private BackgroundWorker LogUpdateWorker = new BackgroundWorker();
        public MainViewModel() {
            WorkerAdd();
            WindowsButtonEvent();

            LogUploadBtn = new RelayCommand(() => { LogUpdateWorker.RunWorkerAsync(); });
        }

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
            Console.WriteLine("finish");
            LogUpdateWorker.CancelAsync();
        }

        private void LogUpdateWorker_DoWork(object sender, DoWorkEventArgs e) {
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
        }
    }
}