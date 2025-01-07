using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WorkTracker.Utils.UtilityModels;

namespace WorkTracker.Stores
{
    public class SettingsStore
    {
        private ApplicationSettings? _currentSettings;
        public ApplicationSettings CurrentSettings { get => _currentSettings; }
        public bool IsSettingsLoaded { get => _currentSettings != null; }

        public SettingsStore()
        { }

        public void LoadSettings()
        {   
            var settings = Properties.Settings.Default;
            IBaseTheme baseTheme = settings.Theme=="Light"?Theme.Light : Theme.Dark;
            String primaryColorCode = settings.PrimaryColorCode;

            Color primaryColor=(Color)ColorConverter.ConvertFromString(primaryColorCode);
            Color secondaryColor=(Color)ColorConverter.ConvertFromString("#647178");

            Theme theme=Theme.Create(baseTheme,primaryColor,secondaryColor);


            ApplicationSettings appSettings = new ApplicationSettings()
            {
                LanguageCode = settings.LanguageCode,
                Theme = theme,
                PrimaryColorCode = primaryColorCode,
                FontCode=settings.FontCode
            };
            _currentSettings = appSettings;
        }

        public void SaveSettings()
        {
            if (CurrentSettings == null)
                return;

            var settings = Properties.Settings.Default;
   
            if (CurrentSettings.Theme.GetBaseTheme() == BaseTheme.Light)
                settings.Theme = "Light";
            else
                settings.Theme = "Dark";

            settings.LanguageCode = CurrentSettings.LanguageCode;
            settings.PrimaryColorCode = CurrentSettings.PrimaryColorCode;
            settings.FontCode= CurrentSettings.FontCode;
            settings.Save();
        }


        #region Internal Backing Settings
        /// <summary>
        /// Provides internal instance to be used if loading of application settings fails
        /// </summary>
       /*private static readonly ApplicationSettings _internalBackupSettings = new ApplicationSettings()
        {
            LanguageCode = LanguageProvider.Instance.Languages.ElementAt(0).Key,
            BaseTheme = Theme.Light,
            PrimaryColorCode = ThemeProvider.Instance.PrimaryColors.ElementAt(0).Key,
            SecondaryColorCode = ThemeProvider.Instance.SecondaryColors.ElementAt(0).Key,
        };*/
        #endregion
    }
}
