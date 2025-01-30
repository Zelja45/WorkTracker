using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class IncomeCardViewModel:BaseViewModel
    {
        private WorksessionService _worksessionService;
        private User user;

        private List<String> months;
        private List<int> years;
        private int _selectedMonth;
        private int _selectedYear;
        private string _earning;
        private bool _noData = false;
        private bool _showEarning = true;
        private List<String> allMonths;
        public List<String> Months { get { return this.months; } }
        public List<int> Years { get { return this.years; } }
        public int SelectedMonth { get { return _selectedMonth; } set { _selectedMonth = value; LoadSessionsInfo(); OnPropertyChanged(); } }
        public int SelectedYear { get { return _selectedYear; } set { _selectedYear = value;  LoadSessionsInfo(); OnPropertyChanged(); } }
        public string Earning { get { return _earning; } set { _earning = value; OnPropertyChanged(); } }

        public bool NoData { get { return _noData; } set { _noData = value; OnPropertyChanged(); } }
        public bool ShowEarning { get { return _showEarning; } set { _showEarning = value; OnPropertyChanged(); } }

        public IncomeCardViewModel(WorksessionService worksessionService, User user)
        {
            _worksessionService = worksessionService;
            this.user = user;
            SelectedMonth = DateTime.Now.Month-1;
            SelectedYear = DateTime.Now.Year;
            PopulateMonthsAndYears();
        }

        public override async System.Threading.Tasks.Task Initialize()
        {
           await LoadSessionsInfo();
        }

        private async System.Threading.Tasks.Task LoadSessionsInfo() 
        {
            if (SelectedYear == DateTime.Now.Year && SelectedMonth > DateTime.Now.Month)
            {
                NoData = true;
                ShowEarning = false;
            }
            else
            {
                NoData = false;
                ShowEarning = true;
                List<Worksession> sessions = await _worksessionService.GetAllEndedWorkerSessionsInMonthOfYear(user.Username, SelectedYear, SelectedMonth + 1);
                decimal earningTmp = 0;
                foreach (Worksession session in sessions)
                {
                    earningTmp += Util.calculateIncome(session.Worksessionreport.WorkedHours, session.Worksessionreport.HourlyRate) +
                        Util.calculateIncome(session.Worksessionreport.OvertimeHours, session.Worksessionreport.OvertimeHourlyRate);
                }

                Earning = earningTmp.ToString("F2");
            }
        }

        private void PopulateMonthsAndYears()
        {
            string languageCode = App.serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            var culture = new CultureInfo(languageCode);
            months = culture.DateTimeFormat.MonthNames.ToList();

            years = new List<int>();
            years.Add(DateTime.Now.Year);
            years.Add(DateTime.Now.Year - 1);


        }


    }
}
