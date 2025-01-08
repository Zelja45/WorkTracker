using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Components;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class AddWorkerInSectorViewModel:BaseViewModel
    {
        private INavigationService _navigationService;
        private SectorService _sectorService;
        private UserService _userService;

        private bool _isAdddable = false;
        private ManagerWorkerAddingSectorCardViewModel? _selectedWorker = null;
        private string _filterText="";
        private bool _noWorkersToAdd = false;

        public Sector Sector { get; set; }
        public ManagerWorkerAddingSectorCardViewModel? SelectedWorker { get { return _selectedWorker; } set { _selectedWorker = value; CheckIsAddable();  OnPropertyChanged(); } }
        public List<ManagerWorkerAddingSectorCardViewModel> WorkerCards { get; set; }=new List<ManagerWorkerAddingSectorCardViewModel>();
        public ObservableCollection<ManagerWorkerAddingSectorCardViewModel> WorkerCardsToShow { get; set; } = new ObservableCollection<ManagerWorkerAddingSectorCardViewModel>();

        public bool IsAddable { get { return _isAdddable; } set { _isAdddable = value; OnPropertyChanged(); } }
        public bool NoWorkersToAdd { get { return _noWorkersToAdd; } set { _noWorkersToAdd = value; OnPropertyChanged(); } }

        public string FilterText { get { return _filterText; } set { _filterText = value; Filter(); OnPropertyChanged(); } }

        public RelayCommand AddWorkerCommand { get; set; }
        public RelayCommand NavigateBackCommand { get; set; }

        public AddWorkerInSectorViewModel(INavigationService navigationService,SectorService sectorService,UserService userService)
        {
            _navigationService = navigationService;
            _sectorService = sectorService;
            _userService = userService;
            NavigateBackCommand = new RelayCommand(async o => { await _navigationService.NavigateTo<ManageSectorViewModel>(); }, o => true);
            AddWorkerCommand=new RelayCommand(async o => {
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
                await AddWorker();
                
                await _navigationService.NavigateTo<ManageSectorViewModel>();
                App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
            }, o=>true);
        }

        private void CheckIsAddable()
        {
            if (_selectedWorker != null)
            {
                IsAddable = true;
            }
            else
                IsAddable = false;
        }

        private void Filter()
        {
            WorkerCardsToShow.Clear();
            foreach(var workerCard in WorkerCards)
            {
                if(workerCard.User.Name.StartsWith(FilterText, true, null) || workerCard.User.Surname.StartsWith(FilterText, true, null) || workerCard.User.Username.StartsWith(FilterText, true, null) || FilterText.Length == 0)
                {
                    WorkerCardsToShow.Add(workerCard);
                }
            }
        }
        private async System.Threading.Tasks.Task AddWorker()
        {
            try
            {
                await _sectorService.AddWorkerToSector(Sector.IdSector, SelectedWorker.User.Username);
                new CustomDialog(false, true,(string) Application.Current.Resources["WorkerIsAddedTitle"], (string)Application.Current.Resources["WorkerIsAddedDescription"]).Show();
            }
            catch (Exception ex)
            {
                new CustomDialog(false, true, (string)Application.Current.Resources["Erorr"], (string)Application.Current.Resources["SomethingWentWrong"]).Show();
            }
        }

        public override async System.Threading.Tasks.Task Initialize()
        {
            List<User> workers= await _userService.GetFreeWorkers();
            WorkerCards.Clear();
            foreach (var worker in workers) 
            {
                ManagerWorkerAddingSectorCardViewModel card = new ManagerWorkerAddingSectorCardViewModel(worker);
                card.Command = new RelayCommand(o =>
                {
                    if (card.IsChecked)
                    {
                        if (SelectedWorker != null && SelectedWorker != card)
                        {
                            SelectedWorker.IsChecked = false;
                        }
                        SelectedWorker = card;
                    }
                    else
                    {
                        if (SelectedWorker == card)
                        {
                            SelectedWorker = null;
                        }
                    }
                }, o => true);
                WorkerCards.Add(card);
                WorkerCardsToShow.Add(card);
            }
            NoWorkersToAdd = WorkerCards.Count == 0;
            
        }
        public override void Dispose()
        {
            IsAddable = false;
            FilterText = "";
            SelectedWorker = null;
            WorkerCards.Clear();
            WorkerCardsToShow.Clear();
            NoWorkersToAdd = false;
        }

    }
}
