using ProgressBar.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgressBar
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            predictWorker = new BackgroundWorker();
            predictWorker.DoWork += new DoWorkEventHandler(execute_DoWork);
            predictWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            //predictWorker.ProgressChanged += new ProgressChangedEventHandler(predictWorker_ProgressChanged);
            predictWorker.WorkerReportsProgress = true;
            predictWorker.WorkerSupportsCancellation = true;
        }
        private BackgroundWorker predictWorker = new BackgroundWorker();
        public ICommand ExecuteClickCommand
        {
            get { return new DelegateCommand(ExecuteClick, CanCommand); }
        }
        private void ExecuteClick()
        {
            if (!IsPause)
                return;

            predictWorker.RunWorkerAsync();
            ExecuteButtonEnable = false;
            PauseButtonEnable = true;

        }
        public bool ExecuteButtonEnable
        {
            get { return executeButtonEnable; }
            set { executeButtonEnable = value; OnPropertyChanged(); }
        }
        private bool executeButtonEnable = true;
        public bool PauseButtonEnable
        {
            get { return pauseButtonEnable; }
            set { pauseButtonEnable = value; OnPropertyChanged(); }
        }
        private bool pauseButtonEnable = false;

        private bool IsPause = true;

        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; OnPropertyChanged(); }
        }
        private string buttonText = "開始";
        public string ButtonCancelText
        {
            get { return buttonCancelText; }
            set { buttonCancelText = value; OnPropertyChanged(); }
        }
        private string buttonCancelText = "取消";
        private void execute_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (ProgressBarValue < 100)
            {
                //暫停
                if (worker.CancellationPending)
                {
                    ButtonText = "繼續";
                    ExecuteButtonEnable = true;
                    PauseButtonEnable = false;
                    IsPause = true;
                    return;
                }
                ProgressBarValue = ProgressBarValue + 1;
                IsPause = false;
                Thread.Sleep(100);
            }

        }
        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ExecuteButtonEnable = false;
            PauseButtonEnable = false;
            ButtonCancelText = "完成";
        }

        public ICommand PauseClickCommand
        {
            get { return new DelegateCommand(PauseClick, CanCommand); }
        }

        private void PauseClick()
        {
            predictWorker.CancelAsync();
        }
        public ICommand CancelClickCommand
        {
            get { return new DelegateCommand(CancelClick, CanCommand); }
        }

        private void CancelClick()
        {
            predictWorker.CancelAsync();
            ProgressBarValue = 0;
            ButtonText = "開始";
            ButtonCancelText = "取消";
            ExecuteButtonEnable = true;
            PauseButtonEnable = false;
        }
        public int ProgressBarValue
        {
            get { return progressBarValue; }
            set { progressBarValue = value; OnPropertyChanged(); }
        }

        private int progressBarValue = 0;

        private bool CanCommand()
        {
            return true;
        }
    }
}
