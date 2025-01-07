using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class HeaderViewModel:BaseViewModel
    {
        private string _dateText;
        public string DateText
        {

            get
            {
                return _dateText;
            }
            set
            {
                _dateText = value; OnPropertyChanged();
            }
        }
        public HeaderViewModel()
        {
            DateTime now = DateTime.Now;
            string languageCode= App.serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            DateText = now.ToString("D", new CultureInfo(languageCode));
        }
        public void ChangeLanguage(string languageCode)
        {
            DateTime now = DateTime.Now;
            DateText=now.ToString("D", new CultureInfo(languageCode));
        }
    }
}
