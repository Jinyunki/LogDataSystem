using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight;

namespace LogDataSystem.DataModels
{
    public class MainModel : ViewModelBase
    {
        #region 윈도우 이벤트/버튼 
        public ICommand BtnMinmize { get; set; }
        public ICommand BtnMaxsize { get; set; }
        public ICommand BtnClose { get; set; }
        private WindowState _windowState;
        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                if (_windowState != value)
                {
                    _windowState = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region 경로
        private string logPath = "D:\\LOG\\";
        public string LogPath {
            get { return logPath; }
            set {
                if (logPath != value) {
                    logPath = value;
                    RaisePropertyChanged(nameof(LogPath));
                }
            }
        }
        #endregion
    }
}
