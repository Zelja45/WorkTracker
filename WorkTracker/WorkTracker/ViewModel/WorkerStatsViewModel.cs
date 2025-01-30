using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{

    public class WorkerStatsViewModel : BaseViewModel
    {
        private WorksessionService _sessionService;
        private INavigationService _navigationService;
        private TaskService _taskService;
        private SectorService _sectorService;
        private List<String> months;
        private List<int> years;
        private int _selectedMonth;
        private int _selectedYear;
        private string _efficiency;
        private int _numberOfDoneTasks;
        private int _numberOfTotalAssignedTasks;
        private bool _noReports = false;
        private int _selectedSortIndex = 0;
        private bool _backVisibility = false;

        public ActivityCardViewModel ActivityCardViewModel { get; set; }
        public IncomeCardViewModel IncomeCardViewModel { get; set; }   

        public bool BackVisibility { get { return _backVisibility; } set { _backVisibility = value; OnPropertyChanged(); } }
        public User User { get; set; }
        public string UserName { get { return User.Name + " " + User.Surname; } }
        public List<String> Months {get{ return this.months; } }
        public List<int> Years {get{ return this.years; } }
        public int SelectedMonth { get { return _selectedMonth; } set { _selectedMonth = value; LoadSessionsInfo(); OnPropertyChanged(); } }
        public int SelectedYear { get { return _selectedYear; } set { _selectedYear= value;LoadSessionsInfo(); OnPropertyChanged(); } }
        public string Efficiency { get { return _efficiency; } set { _efficiency = value; OnPropertyChanged(); } }
        public bool NoReports { get { return _noReports; } set { _noReports = value; OnPropertyChanged(); } }
        public int NumberOfDoneTasks { get { return _numberOfDoneTasks; } set { _numberOfDoneTasks = value; OnPropertyChanged();  } }
        public int NumberOfTotalAssignedTasks { get { return _numberOfTotalAssignedTasks; } set { _numberOfTotalAssignedTasks = value;OnPropertyChanged(); } }
        public ObservableCollection<WorksessionReportItemViewModel> ReportItems { get; set; }=new ObservableCollection<WorksessionReportItemViewModel>();

        public RelayCommand NavigateBackCommand { get; }

        public int SelectedSortIndex { get { return _selectedSortIndex; } set { _selectedSortIndex = value; Sort(); OnPropertyChanged(); } }



        public WorkerStatsViewModel(WorksessionService sessionService,TaskService taskService,SectorService sectorService,INavigationService navigation)
        {
            _navigationService = navigation;
            _sessionService = sessionService;
            _taskService = taskService;
            _sectorService = sectorService;
            NavigateBackCommand = new RelayCommand(o => { navigation.NavigateTo<ManagerWorkerStatsViewModel>(); }, o => true);
        }

        public override async System.Threading.Tasks.Task Initialize()
        {
            PopulateMonthsAndYears();
            if (User == null)
            {
                User = App.serviceProvider.GetRequiredService<UserStore>().User;
                BackVisibility = false;
            }
            else
            {
                BackVisibility = true;
            }
            _selectedYear=DateTime.Now.Year;
            _selectedMonth=DateTime.Now.Month-1;

            ActivityCardViewModel = new ActivityCardViewModel(_taskService, User, _sessionService, _sectorService);
            await ActivityCardViewModel.Initialize();

            IncomeCardViewModel = new IncomeCardViewModel(_sessionService, User);
            await IncomeCardViewModel.Initialize();

            List<Model.Task> workerTasks = await _taskService.getAllTasksOfWorker(User.Username);
            NumberOfTotalAssignedTasks = workerTasks.Count;
            NumberOfDoneTasks = workerTasks.Where(t=>t.Status==2).Count();
            double efficiency = 100;
            if (NumberOfTotalAssignedTasks != 0)
            {
                efficiency=(NumberOfDoneTasks*1.0/NumberOfTotalAssignedTasks)*100;
            }
            Efficiency = efficiency.ToString("F2")+"%";

            LoadSessionsInfo();
        }

        private void Sort()
        {
            if (SelectedSortIndex == 0)
            {
                List<WorksessionReportItemViewModel> tmp = ReportItems.OrderBy(r => r.Worksession.StartTime).ToList();
                ReportItems.Clear();
                tmp.ForEach(r=>ReportItems.Add(r));
            }
            else if (SelectedSortIndex == 1)
            {
                List<WorksessionReportItemViewModel> tmp = ReportItems.OrderByDescending(r => r.Worksession.StartTime).ToList();
                ReportItems.Clear();
                tmp.ForEach(r => ReportItems.Add(r));
            }
            else if (SelectedSortIndex == 2)
            {
                List<WorksessionReportItemViewModel> tmp = ReportItems.OrderBy(r => r.AmountEarned).ToList();
                ReportItems.Clear();
                tmp.ForEach(r => ReportItems.Add(r));
            }
            else if (SelectedSortIndex == 3)
            {
                List<WorksessionReportItemViewModel> tmp = ReportItems.OrderByDescending(r => r.AmountEarned).ToList();
                ReportItems.Clear();
                tmp.ForEach(r => ReportItems.Add(r));
            }
        }

        private async void LoadSessionsInfo()
        {
            List<Worksession> sessions = await _sessionService.GetAllEndedWorkerSessionsInMonthOfYear(User.Username, SelectedYear, SelectedMonth + 1);
            ReportItems.Clear();
            foreach (Worksession session in sessions)
            {
                ReportItems.Add(new WorksessionReportItemViewModel(session));
            }
            NoReports = ReportItems.Count == 0;
            Sort();
        }

        private void PopulateMonthsAndYears()
        {
            string languageCode = App.serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            var culture = new CultureInfo(languageCode);
            months=culture.DateTimeFormat.MonthNames.ToList();

            years = new List<int>();
            years.Add(DateTime.Now.Year);
            years.Add(DateTime.Now.Year - 1);


        }
        public override void Dispose()
        {
            base.Dispose();
            User = null;
        }
    }
}
