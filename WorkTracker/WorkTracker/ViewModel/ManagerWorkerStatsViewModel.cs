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
    public class ManagerWorkerStatsViewModel:BaseViewModel
    {
        private INavigationService _naviagationService;
        private SectorService _sectorService;
        private UserStore _userStore;
        private string _searchText="";
        private List<StatWorkerCardViewModel> allCards = new List<StatWorkerCardViewModel>();
        private bool _noWorkers = false;


        public bool NoWorkers {  get { return _noWorkers; } set { _noWorkers = value; OnPropertyChanged(); } }
        public ObservableCollection<StatWorkerCardViewModel> CardsToShow { get; set; }=new ObservableCollection<StatWorkerCardViewModel>();
        public string SearchText { get {return _searchText; } set { _searchText = value; Filter(); OnPropertyChanged(); } }
        public ManagerWorkerStatsViewModel(INavigationService naviagationService, SectorService sectorService,UserStore userStore)
        {
            _naviagationService = naviagationService;
            _sectorService = sectorService;
            _userStore = userStore;
        }

        public override async System.Threading.Tasks.Task Initialize()
        {
            List<User> workers = await _sectorService.GetAllManagerWorkers(_userStore.User.Username);
            CardsToShow.Clear();
            allCards.Clear();
            foreach (var worker in workers) 
            {
                StatWorkerCardViewModel stc = new StatWorkerCardViewModel(worker, _naviagationService);
                CardsToShow.Add(stc);
                allCards.Add(stc);
            }
            Filter();
        }
        private void Filter()
        {
            CardsToShow.Clear();
            for (int i = 0; i < allCards.Count; i++)
            {
                if (allCards[i].User.Name.StartsWith(SearchText, true, null) || allCards[i].User.Surname.StartsWith(SearchText, true, null) || allCards[i].User.Username.StartsWith(SearchText, true, null) || SearchText.Length == 0)
                {
                    CardsToShow.Add(allCards[i]);
                }
            }
            NoWorkers = CardsToShow.Count == 0;

        }
    }
}
