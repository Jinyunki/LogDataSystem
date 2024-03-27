using System.Windows;
using System;
using GalaSoft.MvvmLight.Command;
using LogDataSystem.DataModels;
using LogDataSystem.Utiles;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls.Primitives;

namespace LogDataSystem.ViewModel {
    public class MainViewModel : MainModel
    {
        public MainViewModel() {
            //WorkerAdd(); // ��׶��� �۾� ���� �߰�
            WindowsButtonEvent(); // ������ ��ư �̺�Ʈ ����

            LogDataEditor = new EditLogData();
            // �α� ���ε� ��ư ��� ����
            LogUploadBtn = new RelayCommand(StartEventHandling);
            StopUploadBtn = new RelayCommand(StopEventHandling);
        }
        // Instance �Ҹ� �� �۵�
        ~MainViewModel() {
            StopEventHandling();
        }

        /// <summary>
        /// ���ø����̼��������� ���,����������,�ݱ� �⺻ ��ư
        /// </summary>
        private void WindowsButtonEvent() {
            BtnMinmize = new RelayCommand(() => { WindowState = WindowState.Minimized; });
            BtnMaxsize = new RelayCommand(() => { WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal; });
            BtnClose = new RelayCommand(() => { Application.Current.Shutdown(); });
        }

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private async void StartEventHandling() {
            "START".Debug();
            LogDataEditor.AddLogFile_Csv(1, "", 5000, 80, 5, 45, 600, 0, DateTime.Now, DateTime.Now, "START");
            await StartPeriodicWork(cancellationTokenSource.Token);
        }


        private void StopEventHandling() {
            cancellationTokenSource?.Cancel();
        }

        private async Task StartPeriodicWork(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                try {
                    // �ֱ������� �۾� ����
                    await Task.Delay(1000, cancellationToken);
                    "RUN".Debug();
                    LogDataEditor.AddLogFile_Csv(1, "", 5000, 80, 5, 45, 600, 0, DateTime.Now, DateTime.Now, "RUN");

                } catch (TaskCanceledException e) {
                    // ��� ��û�� �߻��ϸ� ���ܰ� �߻��ϹǷ�, ���⼭ �۾� �ߴ� ó���� ������ �� �ֽ��ϴ�.
                    // �ʿ��� ��� �߰����� �۾��� ������ �� �ֽ��ϴ�.
                    LogDataEditor.AddLogFile_Csv(1, "", 5000, 80, 5, 45, 600, 0, DateTime.Now, DateTime.Now, "END");
                    "END".Debug();
                    e.ToString().Debug();
                    break; // �۾� �ߴ�
                }
            }
        }
    }
}