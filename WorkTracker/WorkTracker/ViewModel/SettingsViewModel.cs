using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WorkTracker.Services;
using WorkTracker.Utils;
using WorkTracker.Utils.UtilityModels;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class SettingsViewModel:BaseViewModel
    {
        private readonly List<String> fonts = new List<String> {"Segoe UI","Times New Roman","Verdana"};
        private bool _isDarkThemeSetted;
        private bool _englishSelected;
        private bool _bosnianSelected;
        private string _selectedFont;
        private int _selectedColorIndex;
        private SettingsService _settingsService;
        public bool IsDarkThemeSetted { get { return _isDarkThemeSetted; } set { _isDarkThemeSetted = value; OnPropertyChanged(); } }
        public bool EnglishSelected { get { return _englishSelected; } set { _englishSelected = value; OnPropertyChanged(); } } 
        public bool BosnianSelected { get { return _bosnianSelected; } set { _bosnianSelected = value; OnPropertyChanged(); } }
        public string SelectedFont { get { return _selectedFont; } set { _selectedFont = value; _settingsService.ChangeFont(_selectedFont); OnPropertyChanged(); } }
        public int SelectedColorIndex { get { return _selectedColorIndex; } set { _selectedColorIndex = value; ChangePrimaryColor(ThemeChanger.ColorCodes[_selectedColorIndex]); OnPropertyChanged(); } }
        public List<string> Fonts { get { return fonts; } }
        public List<ColorItem> ColorItems { get; set; }
        public RelayCommand SwitchTheme { get; }
        public RelayCommand SwitchLanguage { get; }

        public SettingsViewModel(SettingsService settingsService)
        {

            ColorItems = new List<ColorItem>
            {
                new ColorItem{NameKey="Green",Name=(string)Application.Current.Resources["Green"],Color=(Brush)new BrushConverter().ConvertFrom(ThemeChanger.ColorCodes[0]) },
                new ColorItem{NameKey="Blue",Name=(string)Application.Current.Resources["Blue"],Color=(Brush)new BrushConverter().ConvertFrom(ThemeChanger.ColorCodes[1]) },
                new ColorItem{NameKey="Red",Name=(string)Application.Current.Resources["Red"],Color=(Brush)new BrushConverter().ConvertFrom(ThemeChanger.ColorCodes[2]) }
            };
            _settingsService = settingsService;
            SwitchTheme = new RelayCommand(o => { _settingsService.ChangeBaseThemeMode(); IsDarkThemeSetted = _settingsService.IsDarkThemeSetted; }, o => true);
            SwitchLanguage = new RelayCommand(o => { ChangeLanguage(o); }, o => true);
        }
        public void ChangePrimaryColor(string colorCode)
        {
            _settingsService.ChangePrimaryColor(colorCode);
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
