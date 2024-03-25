using System.Windows;
using System;
using GalaSoft.MvvmLight.Command;
using LogDataSystem.DataModels;
using LogDataSystem.Utiles;
using System.Threading.Tasks;
using System.Threading;

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

        private CancellationTokenSource cancellationTokenSource;

        private async void StartEventHandling() {
            cancellationTokenSource = new CancellationTokenSource();
            await StartPeriodicWork(cancellationTokenSource.Token);
        }


        private void StopEventHandling() {
            cancellationTokenSource?.Cancel();
        }

        private async Task StartPeriodicWork(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                try {
                    // �ֱ������� �۾� ����
                    await Task.Delay(5000, cancellationToken);
                    Console.WriteLine("TEST RUN " + DateTime.Now);
                } catch (TaskCanceledException) {
                    // ��� ��û�� �߻��ϸ� ���ܰ� �߻��ϹǷ�, ���⼭ �۾� �ߴ� ó���� ������ �� �ֽ��ϴ�.
                    // �ʿ��� ��� �߰����� �۾��� ������ �� �ֽ��ϴ�.
                    Console.WriteLine("TEST END " + DateTime.Now);
                    break; // �۾� �ߴ�
                }
            }
        }
    }
}