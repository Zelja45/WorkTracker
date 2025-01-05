using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Services;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class SettingsViewModel:BaseViewModel
    {
        private bool _isDarkThemeSetted;
        private bool _englishSelected;
        private bool _bosnianSelected;
        private SettingsService _settingsService;
        public bool IsDarkThemeSetted { get { return _isDarkThemeSetted; } set { _isDarkThemeSetted = value; OnPropertyChanged(); } }
        public bool EnglishSelected { get { return _englishSelected; } set { _englishSelected = value; OnPropertyChanged(); } } 
        public bool BosnianSelected { get { return _bosnianSelected; } set { _bosnianSelected = value; OnPropertyChanged(); } }

        public RelayCommand SwitchTheme { get; }
        public RelayCommand SwitchLanguage { get; }

        public SettingsViewModel(SettingsService settingsService)
        {
            _settingsService = settingsService;
            SwitchTheme = new RelayCommand(o => { _settingsService.ChangeBaseThemeMode(); IsDarkThemeSetted = _settingsService.IsDarkThemeSetted; }, o => true);
            SwitchLanguage = new RelayCommand(o => { ChangeLanguage(o); }, o => true);
        }
        public void ChangeLanguage(object param)
        {

            if (param is string code)
            {
                switch (code)
                {
                    case "sr-Latn-RS":
                        _settingsService.ChangeLanguage(code);
                        BosnianSelected = true;
                        EnglishSelected = false;
                        break;
                    case "en-US":
                        _settingsService.ChangeLanguage(code);
                        EnglishSelected=true;
                        BosnianSelected = false;
                        break;
                }
            }
        }
    }
}
