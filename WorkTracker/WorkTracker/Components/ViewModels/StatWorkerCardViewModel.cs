using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.ViewModel;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class StatWorkerCardViewModel:BaseViewModel
    {
        private INavigationService _navigationService;
        public User User { get; set; }

        public RelayCommand ViewDetailsCommand { get; set; }

        public string NameSurname { get { return User.Name + " " + User.Surname; } }

        public StatWorkerCardViewModel(User user,INavigationService navigationService)
        {
            _navigationService = navigationService;
            User = user;
            ViewDetailsCommand = new RelayCommand(async o => { 
                App.serviceProvider.GetRequiredService<WorkerStatsViewModel>().User = User;
               await _navigationService.NavigateTo<WorkerStatsViewModel>();
            },o=>true);
        }
        
    }
}
