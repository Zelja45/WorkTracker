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
    public class SectorCardViewModel:BaseViewModel
    {
        public Sector Sector { get; set; }
        public int NumberOfWorkers { get; set; }
        public RelayCommand OpenDetails { get; set; }
        public SectorCardViewModel(INavigationService navigationService,Sector sector,int numberOfWorkers)
        {
            Sector = sector;
            NumberOfWorkers = numberOfWorkers;
            OpenDetails = new RelayCommand(async o => { App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
                App.serviceProvider.GetRequiredService<ManageSectorViewModel>().Sector= sector;
                await navigationService.NavigateTo<ManageSectorViewModel>();
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
            }, o => true);
        }
    }

}
