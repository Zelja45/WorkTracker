using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class ManageSectorsManagerViewModel:BaseViewModel
    {
        private INavigationService _navigationService;
        private SectorService _sectorService;
        private UserStore _userStore;
        private List<SectorCardViewModel> allSectors;
        private string _filterText="";
        private bool _noSectors=false;

        public bool NoSectors { get { return _noSectors; } set { _noSectors = value; OnPropertyChanged(); } }
        public ObservableCollection<SectorCardViewModel> CardsToShow { get; set; } = new ObservableCollection<SectorCardViewModel>();
        public string FilterText { get { return _filterText; } set { _filterText = value; Filter();  OnPropertyChanged(); } }
        public ManageSectorsManagerViewModel(INavigationService navigationService,SectorService sectorService,UserStore userStore)
        {
            _navigationService = navigationService;
            _sectorService = sectorService;
            _userStore = userStore;
        }
        public void Filter()
        {
            CardsToShow.Clear();
            foreach(SectorCardViewModel card in allSectors)
            {
                if(card.Sector.Name.Contains(FilterText))
                    CardsToShow.Add(card);
            }
            NoSectors = CardsToShow.Count == 0;
        }
        public override async System.Threading.Tasks.Task Initialize()
        {
            allSectors = new List<SectorCardViewModel>();
            CardsToShow.Clear();
            List<Sector> sectors = await _sectorService.GetManagerSectors(_userStore.User.Username);
            foreach (Sector sector in sectors) 
            { 
                int numberOfWorkersInSector=await _sectorService.GetNumberOfWorkersInSector(sector.IdSector);
                SectorCardViewModel vm = new SectorCardViewModel(_navigationService,sector, numberOfWorkersInSector);
                allSectors.Add(vm);
                CardsToShow.Add(vm);
            }
            NoSectors = CardsToShow.Count == 0;
            Filter();
        }
       

    }
}
