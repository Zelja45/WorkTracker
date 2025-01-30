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
    public class WorksessionReportDetailsViewModel:BaseViewModel
    {
        private Worksession _worksession;
        private decimal _totalAmountEarned;
        private string _periodValue;
        private string _totalWorkedHours;

        public Worksession Worksession { get { return _worksession; } }

        public decimal RegularAmountEarned { get; }
        public decimal OvertimeAmountEarned { get; }
        public decimal TotalAmountEarned { get { return _totalAmountEarned; } }
        public string PeriodValue { get { return _periodValue; } }
        public string TotalWorkedHours { get { return _totalWorkedHours; } }
        public string RegularWorkHours { get; }
        public string OvertimeWorkHours { get; }

        public List<string> PauseRecords{ get; }
        public bool ShowNoPauseRecordsLabel { get; }
        

        public WorksessionReportDetailsViewModel(Worksession worksession)
        {
            _worksession = worksession;

            string languageCode = App.serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            string startDate = worksession.StartTime.ToString("f", new CultureInfo(languageCode));
            string endDate = worksession.EndTime?.ToString("f", new CultureInfo(languageCode));
            _periodValue = startDate + " - " + endDate;

            OvertimeAmountEarned = Util.calculateIncome(worksession.Worksessionreport.OvertimeHours, worksession.Worksessionreport.OvertimeHourlyRate);
            RegularAmountEarned = Util.calculateIncome(worksession.Worksessionreport.WorkedHours, worksession.Worksessionreport.HourlyRate);
            RegularAmountEarned=Math.Round(RegularAmountEarned, 2);
            OvertimeAmountEarned = Math.Round(OvertimeAmountEarned, 2);
            _totalAmountEarned =OvertimeAmountEarned + RegularAmountEarned;


            _totalWorkedHours = Util.SumTimeOnly(worksession.Worksessionreport.WorkedHours, worksession.Worksessionreport.OvertimeHours).ToString("HH:mm:ss");
            RegularWorkHours = worksession.Worksessionreport.WorkedHours.ToString("HH:mm:ss");
            OvertimeWorkHours = worksession.Worksessionreport.OvertimeHours.ToString("HH:mm:ss");

            PauseRecords = new List<string>();
            foreach(Pauselog log in worksession.Pauselogs)
            {
                string tmp = log.StartTime.ToString("f", new CultureInfo(languageCode)) + " - " + log.EndTime?.ToString("f", new CultureInfo(languageCode));
                PauseRecords.Add(tmp);
            }
            
            ShowNoPauseRecordsLabel = PauseRecords.Count == 0;

        }
    }
}
