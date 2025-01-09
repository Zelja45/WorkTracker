using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class WorkSessionViewModel:BaseViewModel
    {
        private UserStore _userStore;
        private WorksessionService _worksessionService;
        private Worksession _currentSession;
        private DispatcherTimer _timer;
        private TimeSpan _elapsedTime;

        public Worksession CurrentSession { get { return _currentSession; } }

        private bool _stopPausePanelVisibility;
        private bool _startPanelVisibility;
        private string _timeToDisplay="00:00:00";
        private bool _onPause = false;
        private bool _pauseBtnChecked = false;

        public bool StartPanelVisibility { get { return _startPanelVisibility; } set { _startPanelVisibility = value; OnPropertyChanged(); } }
        public bool StopPausePanelVisibility { get { return _stopPausePanelVisibility; } set { _stopPausePanelVisibility = value;OnPropertyChanged(); } }
        public string TimeToDisplay { get { return _timeToDisplay;} set { _timeToDisplay = value;OnPropertyChanged(); } }

        public bool PauseBtnChecked { get { return _pauseBtnChecked; } set { _pauseBtnChecked = value; } }

        public RelayCommand StartWorkingSessionCommand { get; set; }
        public RelayCommand StopWorkingSessionCommand { get; set; }
        public RelayCommand PauseWorkingSessionCommand { get;set; }

        public WorkSessionViewModel(UserStore userStore,WorksessionService worksessionService)
        {
            _stopPausePanelVisibility=false;
            _startPanelVisibility = true;
            _userStore = userStore;
            _worksessionService = worksessionService;
            _elapsedTime = TimeSpan.Zero;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;

            StartWorkingSessionCommand = new RelayCommand(async o => { await StartWorkingSession(); },o=>true);
            StopWorkingSessionCommand=new RelayCommand(async o => 
            {
                new CustomDialog(true, true, (string)Application.Current.Resources["EndingSessionTitle"], (string)Application.Current.Resources["EndingSessionDescription"], new RelayCommand (async o=> { await StopWorkingSession(); },o=>true )).Show();
            },o=>true);
            PauseWorkingSessionCommand = new RelayCommand(async o => { await PauseWorkingSession(); }, o => true);
        }

        private async System.Threading.Tasks.Task StartWorkingSession()
        {
            StartPanelVisibility = false ;
            _currentSession = new Worksession
            {
                StartTime = DateTime.Now,
                Status = 0,
                WorkerUsername = _userStore.User.Username,
            };
            await _worksessionService.AddNewWorkSession(_currentSession);
            StartTimer();
            StopPausePanelVisibility = true ;
        }
        public async System.Threading.Tasks.Task StopWorkingSession()
        {
            StartPanelVisibility = true;
            _currentSession.EndTime= DateTime.Now;
            _currentSession.Status = 2;
            _currentSession.WorkedHours=TimeOnly.FromTimeSpan(_elapsedTime); 
            await _worksessionService.StopWorkSession(_currentSession);
            StopTimer();
            Reset();
            StopPausePanelVisibility = false;
        }
        private async System.Threading.Tasks.Task PauseWorkingSession()
        {
            PauseBtnChecked = !PauseBtnChecked;
            if (_onPause)
            {
                await _worksessionService.ResumeWorkSession(_currentSession);
                StartTimer();
                _onPause = false;
            }
            else
            {
                await _worksessionService.PauseWorkSession(_currentSession);
                StopTimer();
                _onPause = true;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            _elapsedTime = _elapsedTime.Add(TimeSpan.FromSeconds(1));
            TimeToDisplay= _elapsedTime.ToString(@"hh\:mm\:ss");
        }
        private void StartTimer()
        {
            _timer.Start();
        }
        private void StopTimer()
        {
            _timer.Stop();
        }

        private void Reset()
        {
            _onPause = false;
            PauseBtnChecked= false;
            OnPropertyChanged(nameof(PauseBtnChecked));
            _timer.Stop();
            _elapsedTime = TimeSpan.Zero; 
            TimeToDisplay = "00:00:00";
            _currentSession = null;
        }



    }
}
