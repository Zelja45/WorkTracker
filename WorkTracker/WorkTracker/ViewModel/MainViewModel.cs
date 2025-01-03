using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Services;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
       private SettingsService _settingsService;

        public RelayCommand SwitchTheme { get; }
        public RelayCommand SwitchLanguage { get; }
       public MainViewModel(SettingsService settingsService)
       {
            _settingsService = settingsService;
            SwitchTheme = new RelayCommand(o => { _settingsService.ChangeBaseThemeMode(); }, o=>true);
            SwitchLanguage=new RelayCommand(o => { _settingsService.ChangeLanguage("sr-RS"); }, o=>true);
       } 
        
       
    }
}
