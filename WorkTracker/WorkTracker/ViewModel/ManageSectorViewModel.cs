using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Components;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class ManageSectorViewModel:BaseViewModel
    {
        private UserStore _userStore;
        private SectorService _sectorService;
        private INavigationService _navigationService;
        private string _newHourlyRate="";
        private string _newOvertimeRate="";
        private bool _isUpdateable;
        private bool _noWorkersInSector;
        private int _numberOfWorkers;
        public Sector Sector { get; set; }
        public int NumberOfWorkers { get { return _numberOfWorkers; } set { _numberOfWorkers = value; OnPropertyChanged(); } }
        public string NewHourlyRate { get { return _newHourlyRate; } set { _newHourlyRate = value; CheckIsUpdateable(); OnPropertyChanged(); } }
        public string NewOvertimeRate { get { return _newOvertimeRate; } set { _newOvertimeRate = value; CheckIsUpdateable(); OnPropertyChanged(); } }

        public bool IsUpdateable { get { return _isUpdateable; } set { _isUpdateable = value;OnPropertyChanged(); } }
        public bool NoWorkersInSector { get { return _noWorkersInSector; } set { _noWorkersInSector = value; OnPropertyChanged(); } }
        public ObservableCollection<ManagerWorkerAddingSectorCardViewModel> WorkersCards { get; set; }=new ObservableCollection<ManagerWorkerAddingSectorCardViewModel>();  

        public RelayCommand NavigateBack { get; set; }
        public RelayCommand UpdateSectorCommand {  get; set; }
        public RelayCommand AddNewWorkerCommand { get;set; }


        public ManageSectorViewModel(SectorService sectorService,INavigationService navigationService,UserStore userStore)
        {
            _sectorService = sectorService;
            _navigationService = navigationService;
            _userStore = userStore;
            NavigateBack = new RelayCommand(async o => {
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
                await _navigationService.NavigateTo<ManageSectorsManagerViewModel>();
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
            }, o => true);
            UpdateSectorCommand = new RelayCommand(async o =>
            {
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
                await UpdateSector();
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
            }, o => true);
            AddNewWorkerCommand = new RelayCommand(async o => {
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
                App.serviceProvider.GetRequiredService<AddWorkerInSectorViewModel>().Sector = Sector;
                await _navigationService.NavigateTo<AddWorkerInSectorViewModel>();
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
            }, o => true);
        }
        private void CheckIsUpdateable()
        {
            if (NewHourlyRate.Length != 0 && NewOvertimeRate.Length != 0)
            {
                if (NewHourlyRate != Sector.HourlyRate.ToString() || NewOvertimeRate != Sector.OvertimeHourlyRate.ToString())
                {
                    IsUpdateable = true;
                }
                else
                {
                    IsUpdateable = false;
                }
            }
            else
            {
                IsUpdateable = false;
            }
        }
        private async System.Threading.Tasks.Task UpdateSector()
        {
            decimal HourlyRate = decimal.Parse(_newHourlyRate.Replace(',', '.'), CultureInfo.InvariantCulture);
            decimal OvertimeRate = decimal.Parse(_newOvertimeRate.Replace(',', '.'), CultureInfo.InvariantCulture);
            await _sectorService.UpdateSectorInfo(Sector.IdSector, HourlyRate, OvertimeRate);
            new CustomDialog(false, true, (string)Application.Current.Resources["SectorUpdatedTitle"], (string)Application.Current.Resources["SectorUpdatedDescription"]).Show();
        }
        public override async System.Threading.Tasks.Task Initialize()
        {
            NewHourlyRate = Sector.HourlyRate.ToString() ;
            NewOvertimeRate = Sector.OvertimeHourlyRate.ToString();
            WorkersCards.Clear();
            List<User> workers = await _sectorService.GetWorkersInSector(Sector.IdSector);
            foreach (var worker in workers) 
            { 
               ManagerWorkerAddingSectorCardViewModel vm=new ManagerWorkerAddingSectorCardViewModel(worker);
                vm.IsChecked = true;
                vm.Command = new RelayCommand(o => { 
                    new CustomDialog(true, true, (string)Application.Current.Resources["RemovingWorkerTitle"], (string)Application.Current.Resources["RemovingWorkerDescription"], new RelayCommand (async o => {
                        WorkersCards.Remove(vm); 
                        await _sectorService.RemoveWorkerFromSector(Sector.IdSector, vm.User.Username);
                        NumberOfWorkers=NumberOfWorkers-1;
                        NoWorkersInSector = NumberOfWorkers == 0;
                    },o=>true),new RelayCommand(o => { vm.IsChecked = true; },o=>true)).Show(); 
                }, o => true);
                WorkersCards.Add(vm);
                NumberOfWorkers++;
            }
            NoWorkersInSector = NumberOfWorkers == 0;
        }
        public override void Dispose()
        {
            NoWorkersInSector = false;
            NumberOfWorkers = 0;
            IsUpdateable = false;
        }

    }
}
