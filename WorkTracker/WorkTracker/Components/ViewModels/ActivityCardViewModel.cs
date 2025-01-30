using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class ActivityCardViewModel : BaseViewModel
    {
        private TaskService _taskService;
        private User _user;
        private WorksessionService _worksessionService;
        private SectorService _sectorService;

        private int _selectedActivityPeriod = 0;
        private int _taskToComplete;
        private int _completedTasks;
        private int _hourlyNorm;
        private string _workedHours;

        private string _hoursToShow;
        private string _tasksToShow;
        private int _hoursProgress;
        private int _tasksProgress;

        public int SelectedActivityPeriod { get { return _selectedActivityPeriod; } set { _selectedActivityPeriod = value; LoadInfo(); OnPropertyChanged(); } }

        public int TaskToComplete { get => _taskToComplete; set { _taskToComplete = value; OnPropertyChanged(); } }

        public int CompletedTasks { get => _completedTasks; set { _completedTasks = value; OnPropertyChanged(); } }

        public int HourlyNorm { get => _hourlyNorm; set { _hourlyNorm = value; OnPropertyChanged(); } }


        public string WorkedHours { get => _workedHours; set { _workedHours = value; OnPropertyChanged(); } }
        public string HoursToShow { get { return _hoursToShow; } set { _hoursToShow = value; OnPropertyChanged(); } }
        public string TasksToShow { get { return _tasksToShow; } set { _tasksToShow = value; OnPropertyChanged(); } }

        public int HoursProgress { get { return _hoursProgress; } set { _hoursProgress = value;OnPropertyChanged(); } }
        public int TasksProgress { get { return _tasksProgress; } set { _tasksProgress = value; OnPropertyChanged(); } }

        public ActivityCardViewModel(TaskService taskService, User user, WorksessionService worksessionService, SectorService sectorService)
        {
            _taskService = taskService;
            _user = user;
            _worksessionService = worksessionService;
            _sectorService = sectorService;
        }

        public override async System.Threading.Tasks.Task Initialize()
        {
           await LoadInfo();
        }
        private async System.Threading.Tasks.Task LoadInfo()
        {

            List<Model.Task> tasks;
            TimeOnly workHours;
            Sector sector = await _sectorService.GetSector((int)_user.IdSector);
            if (SelectedActivityPeriod == 0)
            {
                tasks = await _taskService.GetTasksForCurrentDay();
                workHours = await _worksessionService.GetTotalTodayWorkingTime();
                HourlyNorm = sector.DailyHoursNorm;
            }
            else
            {
                tasks = await _taskService.GetTasksForCurrentWeek();
                workHours = await _worksessionService.GetTotalCurrentWeekWorkingTime();
                HourlyNorm = sector.WeeklyHoursNorm;
            }
            

            CompletedTasks = tasks.Where(t => t.Status == 2).Count();
            TaskToComplete = tasks.Count;

           
            double decimalHours = workHours.Hour + workHours.Minute / 60.0;
            WorkedHours = decimalHours.ToString("F1");

            TasksToShow = CompletedTasks + "/" + TaskToComplete;
            HoursToShow = WorkedHours + "/" + HourlyNorm;
            if (TaskToComplete == 0)
            {
                TasksProgress = 100;
            }else
                TasksProgress = (int)((CompletedTasks / TaskToComplete) * 100);
            if (HourlyNorm == 0)
            {
                HoursProgress = 100;
            }else
                HoursProgress = (int)((decimalHours / HourlyNorm) * 100);
        }
    }
}
