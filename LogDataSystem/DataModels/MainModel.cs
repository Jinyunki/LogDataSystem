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
    }
}
