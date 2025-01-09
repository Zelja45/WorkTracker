
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        private bool _isLogginable=false;
        private string _username="";
        private string _password="";
        private bool _isIncorrectCredentialsLabelVisible = false;
        private INavigationService _navigationService;
        public SettingsViewModel Settings { get; set; }
        public UserService UserService { get; set; }
        public RelayCommand DoLoginCommand { get; }
        public bool IsLogginable { get { return _isLogginable; } set { _isLogginable = value; OnPropertyChanged(); } }
        public bool IsIncorrectCredentialsLabelVisible { get { return _isIncorrectCredentialsLabelVisible; }set { _isIncorrectCredentialsLabelVisible = value;OnPropertyChanged(); } }

        public string Username { get { return _username; } set { _username = value; if (_password.Length != 0 && _username.Length != 0) { IsLogginable = true; IsIncorrectCredentialsLabelVisible =false; } else IsLogginable = false; Debug.WriteLine(_username); } }
        public string Password { get { return _password; } set { _password = value; if (_password.Length != 0 && _username.Length != 0) { IsLogginable = true; IsIncorrectCredentialsLabelVisible = false; } else IsLogginable = false; } }
        public LoginViewModel(SettingsViewModel settings,UserService userService,INavigationService navigationService)
        {
            _navigationService= navigationService;
            Settings = settings;
            DoLoginCommand = new RelayCommand (async(o)=> { App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true; await DoLogin(); App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false; },o=>true);
            UserService = userService;
           
        }
        private async System.Threading.Tasks.Task DoLogin()
        {
            
            User? user = await UserService.LoginUser(Username, Password);
            if (user != null)
            {
                App.serviceProvider.GetRequiredService<UserStore>().User = user;
                if (user.AccountType.Equals( Constants.AdminKeyWord))
                {
                    App.serviceProvider.GetRequiredService<MainViewModel>().PrepareAdminUI();
                    await _navigationService.NavigateTo<AdminHomeViewModel>();
                }
                else if (user.AccountType.Equals(Constants.WorkerKeyWord))
                {
                    App.serviceProvider.GetRequiredService<MainViewModel>().PrepareWorkerUI();
                    await _navigationService.NavigateTo<WorkerHomeViewModel>();
                }
                else if(user.AccountType.Equals(Constants.ManagerKeyWord))
                {
                    App.serviceProvider.GetRequiredService<MainViewModel>().PrepareManagerUI(); 
                    await _navigationService.NavigateTo<ManagerHomeViewModel>();

                }
                App.mainWindow = new MainWindow
                {
                    DataContext = App.serviceProvider.GetRequiredService<MainViewModel>()
                };
                Username = "";
                Password = "";
                App.mainWindow.Show();
                App.loginWindow.Close();
               
            }
            else
            {
                IsIncorrectCredentialsLabelVisible=true;
            }
        }
        
    }
}
