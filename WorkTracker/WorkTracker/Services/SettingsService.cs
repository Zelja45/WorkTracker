using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Components.ViewModels;
using WorkTracker.Stores;
using WorkTracker.Utils;

namespace WorkTracker.Services
{
    public class SettingsService
    {
        private readonly SettingsStore _settingsStore;
        private readonly ThemeChanger _themeProvider;
        private readonly LanguageChanger _languageChanger;

        public bool IsDarkThemeSetted{ get { return _settingsStore.CurrentSettings.Theme.GetBaseTheme() == BaseTheme.Dark; } }
        

        public SettingsService(SettingsStore settingsStore, ThemeChanger themeProvider, LanguageChanger languageChanger) 
        {
            _settingsStore = settingsStore;
            _themeProvider = themeProvider;
            _languageChanger = languageChanger;
        }

        public void ApplyCurrentSettings()
        {
            var currentSettings = _settingsStore.CurrentSettings;
            if (currentSettings == null)
                return;
            _themeProvider.ApplyTheme(currentSettings.Theme);
            _languageChanger.ApplyLanguage(currentSettings.LanguageCode);
        }
        public void ChangeBaseThemeMode()
        {
            var currentSettings = _settingsStore.CurrentSettings;
            currentSettings.Theme=_themeProvider.SwitchUIBaseTheme();
            _settingsStore.SaveSettings();
           
        }
        public void ChangePrimaryColor(string colorCode)
        {
            var currentSettings=_settingsStore.CurrentSettings;
            currentSettings.PrimaryColorCode = colorCode;
            currentSettings.Theme = _themeProvider.SetPrimaryColor(colorCode);
            _settingsStore.SaveSettings();
        }
        public void ChangeLanguage(string languageCode)
        {
            var currentSettings = _settingsStore.CurrentSettings;
            currentSettings.LanguageCode = languageCode;
            _languageChanger.ApplyLanguage(languageCode);
            App.serviceProvider.GetRequiredService<HeaderViewModel>().ChangeLanguage(languageCode);
            _settingsStore.SaveSettings();
        }
    }
}
