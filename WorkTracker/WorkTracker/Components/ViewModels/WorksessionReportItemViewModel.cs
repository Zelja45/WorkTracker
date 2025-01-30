using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class WorksessionReportItemViewModel : BaseViewModel
    {
        private Worksession _worksession;
        private string _amountEarned;
        private string _periodValue;
        private string _totalWorkedHours;

        public Worksession Worksession { get { return _worksession; } }
        public string AmountEarned { get { return _amountEarned; } }
        public string PeriodValue { get { return _periodValue; } }
        public string TotalWorkedHours { get { return _totalWorkedHours; } }
        public RelayCommand OpenDetailsCommand { get; }

        public WorksessionReportItemViewModel(Worksession worksession)
        {
            _worksession = worksession;

            string languageCode = App.serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            string startDate = worksession.StartTime.ToString("f", new CultureInfo(languageCode));
            string endDate = worksession.EndTime?.ToString("f", new CultureInfo(languageCode));
            _periodValue = startDate+" - "+endDate;

            _amountEarned = (Util.calculateIncome(worksession.Worksessionreport.WorkedHours, worksession.Worksessionreport.HourlyRate)+
                Util.calculateIncome(worksession.Worksessionreport.OvertimeHours, worksession.Worksessionreport.OvertimeHourlyRate)).ToString("F2");

            _totalWorkedHours = Util.SumTimeOnly(worksession.Worksessionreport.WorkedHours, worksession.Worksessionreport.OvertimeHours).ToString("HH:mm:ss");

            OpenDetailsCommand = new RelayCommand(o => { WorksessionReportDetails report=new WorksessionReportDetails(); report.DataContext = new WorksessionReportDetailsViewModel(worksession); report.Show(); }, o => true);
        }
    }
}
