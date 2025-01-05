using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class AdminHomeViewModel:BaseViewModel
    {
        private UserService _userService;
        public UserStore UserStore { get; set; }
        public int NumberOfWorkers { get; set; }
        public int NumberOfDeactivatedWorkers { get; set; }
        public int NumberOfManagers { get; set; }
        public int NumberOfDeactivatedManagers { get; set; }

        public int NumberOfSectors { get; set; }

        public string LabelTitle { get 
            {
                DateTime now = DateTime.Now;
                if ((now.Hour >= 21&& now.Hour <= 23) || (now.Hour >= 0 && now.Hour <= 5))
                {
                    return Application.Current.Resources["GoodNight"] as String;
                }
                else if (now.Hour >= 6 && now.Hour <= 10)
                {
                    return Application.Current.Resources["GoodMorning"] as String;
                }
                else if (now.Hour >= 11 && now.Hour <= 17)
                {
                    return Application.Current.Resources["GoodDay"] as String;
                }
                else
                {
                    return Application.Current.Resources["GoodEvening"] as String;
                }
            } 
        }
        public string UserNameSurname
        {
            get
            {
                return UserStore.User.Name + " " + UserStore.User.Surname;
            }
        }
        public AdminHomeViewModel(UserStore userStore,UserService userService)
        {
            _userService = userService;
            UserStore= userStore;
        }
        public override async Task Initialize()
        {
           NumberOfWorkers = await _userService.GetNumberOfWorkers();
           NumberOfSectors = await _userService.GetNumberOfSectors();
           NumberOfManagers = await _userService.GetNumberOfManagers();
           NumberOfDeactivatedWorkers = await _userService.GetNumberOfWorkersDeactivated();
           NumberOfDeactivatedManagers=await _userService.GetNumberOfManagersDeactivated();
        }
        public override void Dispose()
        {
            base.Dispose();

        }

    }
}
