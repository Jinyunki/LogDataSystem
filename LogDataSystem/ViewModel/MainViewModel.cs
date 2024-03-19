using GalaSoft.MvvmLight;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using LogDataSystem.DataModels;
using LogDataSystem.Utiles;

namespace LogDataSystem.ViewModel
{
    public class MainViewModel : MainModel
    {

        public string Path = "D:\\LOG\\";
        EditLogData editLogData = new EditLogData();
        public ICommand TESTLOGBTN { get; set; }
        public MainViewModel()
        {
            BtnMinmize = new RelayCommand(WinMinmize);
            BtnMaxsize = new RelayCommand(WinMaxSize);
            BtnClose = new RelayCommand(WindowClose);

            TESTLOGBTN = new RelayCommand(() => { editLogData.AddLogFile_Csv(0, true, 1, 2, 3, 4, 5, DateTime.Now, DateTime.Now, Path); });


        }
        

        // Window Minimize
        private void WinMinmize()
        {
            Trace.WriteLine("==========   Start   ==========\nMethodName : " + MethodBase.GetCurrentMethod().Name + "\n");
            try
            {
                WindowState = WindowState.Minimized;
            } catch (Exception ex)
            {
                Trace.WriteLine("========== Exception ==========\nMethodName : " + MethodBase.GetCurrentMethod().Name + "\nException : " + ex);
                throw;
            }
        }

        // Window Size
        private void WinMaxSize()
        {

            Trace.WriteLine("==========   Start   ==========\nMethodName : " + MethodBase.GetCurrentMethod().Name + "\n");
            try
            {
                WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
            } catch (Exception ex)
            {
                Trace.WriteLine("========== Exception ==========\nMethodName : " + MethodBase.GetCurrentMethod().Name + "\nException : " + ex);
                throw;
            }

        }
        private void WindowClose()
        {

            Trace.WriteLine("==========   Start   ==========\nMethodName : " + MethodBase.GetCurrentMethod().Name + "\n");
            try
            {
                Application.Current.Shutdown();

            } catch (Exception ex)
            {
                Trace.WriteLine("========== Exception ==========\nMethodName : " + MethodBase.GetCurrentMethod().Name + "\nException : " + ex);
                throw;
            }

        }
    }
}