﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight;
using LogDataSystem.Utiles;

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

        #region 로그 기능
        public ILogDataEditor LogDataEditor { get; set; } // 로그 데이터 편집기 인터페이스
        public ICommand LogUploadBtn { get; set; } // 로그 업로드 버튼 명령
        public ICommand StopUploadBtn { get; set; } // 로그 업로드  중지 버튼 명령
        #endregion
    }
}
