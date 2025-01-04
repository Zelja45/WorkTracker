using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WorkTracker.Services;
using WorkTracker.Utils.UtilityModels;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class WorkerMainViewModel:BaseViewModel
    {
        public INavigationService NavigationService { get; set; }
       
        public WorkerMainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            
        }   

    }
}
