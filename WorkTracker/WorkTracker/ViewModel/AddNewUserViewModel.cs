
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Components;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class AddNewUserViewModel:BaseViewModel
    {
        private string _name="";
        private string _surname="";
        private string _email="";
        private string _phoneNumber="";
        private string _username="";
        private string _password="";
        private string? _accountType;
        private bool _isRegistrable = false;
        private UserService _userService;
        public string Name { get => _name; set { _name = value; CheckPropertiesToEnable(); OnPropertyChanged(); } }
        public string Surname { get => _surname; set { _surname = value; CheckPropertiesToEnable(); OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; CheckPropertiesToEnable(); OnPropertyChanged(); } }
        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; CheckPropertiesToEnable(); OnPropertyChanged(); } }
        public string Username { get => _username; set { _username = value; CheckPropertiesToEnable(); OnPropertyChanged(); } }
        public string Password{ get => _password; set { _password = value; CheckPropertiesToEnable(); OnPropertyChanged(); } }

        public List<string> AccountTypes { get; } = new List<string> { (string)Application.Current.Resources[Constants.AdminKeyWord], (string)Application.Current.Resources[Constants.ManagerKeyWord], (string)Application.Current.Resources[Constants.WorkerKeyWord] };
        public string? AccountType { get { return _accountType; } set { _accountType = value; CheckPropertiesToEnable(); OnPropertyChanged();} }

        public bool IsRegistrable { get { return _isRegistrable; } set { _isRegistrable = value;OnPropertyChanged(); } }

        public RelayCommand Register { get; set; }
        private Dictionary<string, string> _typesCodesMap = new Dictionary<string, string>
        {
            {(string) Application.Current.Resources[Constants.AdminKeyWord],Constants.AdminKeyWord },
            {(string) Application.Current.Resources[Constants.ManagerKeyWord],Constants.ManagerKeyWord },
            {(string) Application.Current.Resources[Constants.WorkerKeyWord],Constants.WorkerKeyWord }
        };


        public AddNewUserViewModel(UserService userService) 
        {
            _userService = userService;
            Register = new RelayCommand(async o => { App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true; await DoRegistration(); App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false; }, o => true);
        }
        private void CheckPropertiesToEnable() 
        {
            if (_name.Length != 0 && _surname.Length != 0 && _email.Length != 0 && _phoneNumber.Length != 0 && _password.Length != 0 && _username.Length != 0 && _accountType != null)
            {
                IsRegistrable = true;
            }
            else
            { 
                IsRegistrable = false;
            }
        }
        public override void Dispose()
        {
            Name = "";
            Surname = "";
            Email = "";
            PhoneNumber = "";
            Username = "";
            Password = "";
            AccountType = null;
            IsRegistrable= false;
        }
        private async System.Threading.Tasks.Task DoRegistration()
        {
            string phonePattern = @"^\d{9}$";
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            ResourceDictionary resources = Application.Current.Resources;
            if(!Regex.IsMatch(Password, passwordPattern))
            {
                new CustomDialog(false, false,(string)resources["PasswordNotSecureTitle"], (string)resources["PasswordNotSecureDesc"]);
            }
            else if(!Regex.IsMatch(Email, emailPattern))
            {
                new CustomDialog(false, false, (string)resources["EmailIncorrenctTitle"], (string)resources["EmailIncorrenctDescription"]);
            }else if (!Regex.IsMatch(PhoneNumber, phonePattern))
            {
                new CustomDialog(false, false, (string)resources["NumberIncorrenctTitle"], (string)resources["NumberIncorrenctDescription"]);
            }
            else
            {
                User user = new User
                {
                    Name = _name,
                    Email = _email,
                    PhoneNumber = _phoneNumber,
                    Surname = _surname,
                    Username = _username,
                    Password = _password,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                    AccountType = _typesCodesMap[_accountType],
                    IsActive = (sbyte)1
                };
                try
                {
                   
                   await _userService.AddNewUser(user);
                   this.Dispose();
                    new CustomDialog(false, true, (string)resources["NewUserAddedTitle"], (string)resources["NewUserAddedDescription"]);
                }
                catch (Exception ex)
                {
                    new CustomDialog(false, false, (string)resources["IncorrectUsernameTitle"], (string)resources["IncorrectUsernameDescription"]);
                }
            }
        }
    }
}
