using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class SingleUserCardAdminViewModel : BaseViewModel
    {
        private bool _status;
        private UserService _userService;
        public User User { get; set; }

        public RelayCommand SwitchStatus { get; set; }

        public bool Status { get { return _status; } set { _status = value; OnPropertyChanged(); } }
        public string NameSurname { get { return User.Name + " " + User.Surname; } }
        public string AccountType { get { return (string)Application.Current.Resources[User.AccountType]; } }

        public SingleUserCardAdminViewModel(User user, UserService userService)
        {
            User = user;
            _status = User.IsActive != 0;
            _userService = userService;
            SwitchStatus = new RelayCommand(async o => { await SwitchActivationStatusOfUser(); }, o => true);
        }
        private async System.Threading.Tasks.Task SwitchActivationStatusOfUser()
        {
            if (User.IsActive == 0)
            {
                User.IsActive = 1;
                Status = true;
            }
            else
            {
                User.IsActive = 0;
                Status = false;
            }
            await _userService.UpdateUserActiveStatus(User);
        }

    }
}
