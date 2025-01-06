using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class ManagerWorkerAddingSectorCardViewModel:BaseViewModel
    {
        private bool _isChecked = false;
        public bool IsChecked { get { return _isChecked; } set { _isChecked = value; OnPropertyChanged(); } }
        public User User { get; set; }
        public string NameSurname { get { return User.Name+" "+User.Surname; } }
        public RelayCommand Command { get; set; }
        public ManagerWorkerAddingSectorCardViewModel(User user)
        {
            User = user;
        }

    }
}
