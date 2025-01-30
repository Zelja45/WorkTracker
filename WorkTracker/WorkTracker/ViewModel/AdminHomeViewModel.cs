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
        public int NumberOfFreeWorkers { get; set; }

        
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
           NumberOfFreeWorkers = (await _userService.GetFreeWorkers()).Count();
        }
        public override void Dispose()
        {
            base.Dispose();

        }

    }
}
