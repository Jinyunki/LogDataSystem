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
            //WorkerAdd(); // 백그라운드 작업 구독 추가
            WindowsButtonEvent(); // 윈도우 버튼 이벤트 구독

            LogDataEditor = new EditLogData();
            // 로그 업로드 버튼 명령 설정
            LogUploadBtn = new RelayCommand(StartEventHandling);
            StopUploadBtn = new RelayCommand(StopEventHandling);
        }

        // Instance 소멸 시 작동
        ~MainViewModel() {
            StopEventHandling();
        }


        /// <summary>
        /// 어플리케이션윈도우의 축소,사이즈조절,닫기 기본 버튼
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
                    // 주기적으로 작업 수행
                    await Task.Delay(5000, cancellationToken);
                    Console.WriteLine("TEST RUN " + DateTime.Now);
                } catch (TaskCanceledException) {
                    // 취소 요청이 발생하면 예외가 발생하므로, 여기서 작업 중단 처리를 수행할 수 있습니다.
                    // 필요한 경우 추가적인 작업을 수행할 수 있습니다.
                    Console.WriteLine("TEST END " + DateTime.Now);
                    break; // 작업 중단
                }
            }
        }
    }
}