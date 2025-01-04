using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        private bool _isLogginable=false;
        private string _username="";
        private string _password="";
        private bool _isIncorrectCredentialsLabelVisible = false;
        public SettingsViewModel Settings { get; set; }
        public UserService UserService { get; set; }
        public AsyncRelayCommand DoLoginCommand { get; }
        public bool IsLogginable { get { return _isLogginable; } set { _isLogginable = value; OnPropertyChanged(); } }
        public bool IsIncorrectCredentialsLabelVisible { get { return _isIncorrectCredentialsLabelVisible; }set { _isIncorrectCredentialsLabelVisible = value;OnPropertyChanged(); } }

        public string Username { get { return _username; } set { _username = value; if (_password.Length != 0 && _username.Length != 0) { IsLogginable = true; IsIncorrectCredentialsLabelVisible =false; } else IsLogginable = false; Debug.WriteLine(_username); } }
        public string Password { get { return _password; } set { _password = value; if (_password.Length != 0 && _username.Length != 0) { IsLogginable = true; IsIncorrectCredentialsLabelVisible = false; } else IsLogginable = false; } }
        public LoginViewModel(SettingsViewModel settings,UserService userService)
        {
            Settings = settings;
            DoLoginCommand = new AsyncRelayCommand (DoLogin );
            UserService = userService;
           
        }
        private async System.Threading.Tasks.Task DoLogin()
        {
            App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
            User? user = await UserService.LoginUser(Username, Password);
            if (user != null)
            {
                Manager? manager =await UserService.LoginManager(user);
                if(manager != null)
                {
                    IsIncorrectCredentialsLabelVisible = false;
                    App.serviceProvider.GetRequiredService<MainViewModel>().PrepareManagerUI();
                }
                else
                {
                    Worker? worker =await UserService.LoginWorker(user);
                    if(worker != null)
                    {
                        IsIncorrectCredentialsLabelVisible = false;
                        App.serviceProvider.GetRequiredService<MainViewModel>().PrepareWorkerUI();
                    }
                    else
                    {
                        IsIncorrectCredentialsLabelVisible = true;
                        return;
                    }
                }
                IsIncorrectCredentialsLabelVisible = false;
                App.serviceProvider.GetRequiredService<MainWindow>().Show();
                App.serviceProvider.GetRequiredService<LoginWindow>().Close();

                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
            }
            else
            {
                //Admin login
                IsIncorrectCredentialsLabelVisible=true;
            }
        }
        
    }
}
