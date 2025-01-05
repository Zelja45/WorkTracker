using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Stores
{
    public class UserStore:ObeservableObject
    {
        private User _user;
        public User User { get { return _user; } set { _user = value; OnPropertyChanged(); } }
    }
}
