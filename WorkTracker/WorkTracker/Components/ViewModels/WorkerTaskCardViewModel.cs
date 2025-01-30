using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class WorkerTaskCardViewModel : BaseViewModel
    {
        private TaskService _taskService;
        private Model.Task _task;
        private bool _isPinnable;
        private int _progress;
        private string _buttonUpdateStatusText;
        private bool _changeTaskStatusButtonVisibility;
        private bool _changeTaskStatusButtonEnabled;
        private bool _selectableTask;
        private string _dueDateText;
        private string _createdAtText;
        private bool _isProgressSaveable=false;
        private bool _dissableProgressSlider;
        private bool _isPinned = false;

        public int Progress { get { return _progress; } set { _progress = value; CheckIsProgressSaveable();  OnPropertyChanged(); } }
        public string ButtonStatusUpdateText { get { return _buttonUpdateStatusText; } set { _buttonUpdateStatusText = value; OnPropertyChanged(); } }
        public bool ChangeTaskStatusButtonVisibility { get { return _changeTaskStatusButtonVisibility; } set { _changeTaskStatusButtonVisibility = value; OnPropertyChanged(); } }

        public bool ChangeTaskStatusButtonEnabled { get { return _changeTaskStatusButtonEnabled; } set { _changeTaskStatusButtonEnabled = value;OnPropertyChanged(); } }

        public bool PineableTask { get { return _selectableTask; } set { _selectableTask = value;OnPropertyChanged(); } }

        

        public bool IsPinned { get { return _isPinned; } set { _isPinned = value; OnPropertyChanged(); } }

        public string DueDateText { get { return _dueDateText; } set { _dueDateText = value; OnPropertyChanged(); } }
        public string CreatedAtText { get { return _createdAtText; } set { _createdAtText = value; OnPropertyChanged(); } }
        public string PriorityText { get; set; }
        public bool DissableProgressSlider { get {return _dissableProgressSlider; } set { _dissableProgressSlider = value;OnPropertyChanged(); } }

        public bool IsProgressSaveable { get { return _isProgressSaveable; } set { _isProgressSaveable = value; OnPropertyChanged(); } }
        public Model.Task Task { get { return _task; } }
        public RelayCommand UpdateStatusCallback { get; set; }
        public RelayCommand UpdateStatusCommand { get; set; }
        public RelayCommand UpdateProgress { get; set; }

        public RelayCommand PinTaskCommad { get; set; }
        public WorkerTaskCardViewModel(TaskService taskService, Model.Task task,bool pinnableTask)
        {
            _taskService = taskService;
            _task = task;
            _progress = task.Progress;
            _isPinnable = pinnableTask;

            CheckChangeTaskStatusButtonVisibility();
            CheckIsSelectableTask();
            CheckDissableProgressSlider();

            string languageCode = App.serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            DueDateText = Task.DueDate.ToString("f", new CultureInfo(languageCode));
            CreatedAtText = Task.CreatedAt.ToString("f", new CultureInfo(languageCode));
            if (task.Priority == 0)
                PriorityText = (string)Application.Current.Resources["Low"];
            else if (task.Priority == 1)
                PriorityText = (string)Application.Current.Resources["Medium"];
            else if (task.Priority == 2)
                PriorityText = (string)Application.Current.Resources["High"];

            SetButtonStatusUpdateText();
            CheckChangeTaskStatusButtonEnabled();

            UpdateProgress = new RelayCommand(o => {
                new CustomDialog(true, true,(string) Application.Current.Resources["SaveProgressTitle"], (string)Application.Current.Resources["SaveProgressDescription"], new RelayCommand(async o =>
            {
                task.Progress = Progress;
                await _taskService.UpdateTask(task);
                IsProgressSaveable = false;
                CheckChangeTaskStatusButtonEnabled();
            }, o =>true)); }, o => true);

            UpdateStatusCommand = new RelayCommand(o => {
                new CustomDialog(true, true, (string)Application.Current.Resources["ChangeStatusTitle"], (string)Application.Current.Resources["ChangeStatusDescription"], new RelayCommand(async o => {
                    task.Status = (sbyte)(task.Status + 1);
                    await _taskService.UpdateTask(task);
                    if(UpdateStatusCallback != null)
                        UpdateStatusCallback.Execute(o);
                    CheckChangeTaskStatusButtonEnabled();
                    SetButtonStatusUpdateText();
                    CheckChangeTaskStatusButtonVisibility();
                    CheckIsSelectableTask();
                    CheckDissableProgressSlider();
                }, o => true));
            }, o => true);


        }
        private void CheckIsProgressSaveable()
        {
            if (Progress != Task.Progress)
            {
                IsProgressSaveable= true;
            }
            else
            {
                IsProgressSaveable= false;
            }
        }
        private void CheckChangeTaskStatusButtonEnabled()
        {
            if (Task.Status == 0)
            {
                ChangeTaskStatusButtonEnabled = true;
            }
            else if (Task.Status == 1 && Task.Progress == 100)
            {
                ChangeTaskStatusButtonEnabled = true;
            }
            else
            {
                ChangeTaskStatusButtonEnabled = false;
            }
        }
        private void CheckChangeTaskStatusButtonVisibility()
        {
            if (Task.Status >= 2 || Task.DueDate < DateTime.Now)
            {
                ChangeTaskStatusButtonVisibility = false;
            }
            else
            {
                ChangeTaskStatusButtonVisibility = true;
            }
        }
        private void CheckIsSelectableTask()
        {
            if (_isPinnable && (Task.Status == 0 || Task.Status == 1) && Task.DueDate >= DateTime.Now)
            {
                PineableTask = true;
            }
            else
            {
                PineableTask = false;
            }
        }
        private void CheckDissableProgressSlider()
        {
            DissableProgressSlider = !(Task.Status==0||Task.Status == 2 || Task.DueDate < DateTime.Now);
        }
        private void SetButtonStatusUpdateText()
        {
            if (_task.Status == 0)
            {
                ButtonStatusUpdateText = (string)Application.Current.Resources["MarkAsDoing"];
            }
            else if (_task.Status == 1)
            {
                ButtonStatusUpdateText = (string)Application.Current.Resources["MarkAsDone"];

            }

        }
    }
}
