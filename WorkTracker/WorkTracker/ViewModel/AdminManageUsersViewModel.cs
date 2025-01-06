using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class AdminManageUsersViewModel:BaseViewModel
    {
        private UserService _userService;
        private string _searchText;
        private bool _isManagerSelected = false;
        private bool _isWorkerSelected = false;
        private List<SingleUserCardAdminViewModel> allUsers=new List<SingleUserCardAdminViewModel>();
        public ObservableCollection<SingleUserCardAdminViewModel> CardsViewModelsToShow { get; set; }=new ObservableCollection<SingleUserCardAdminViewModel>();
        public string SearchText { get { return _searchText; } set { _searchText = value; FilterByText(); OnPropertyChanged(); } }
        public bool IsManagerSelected { get { return _isManagerSelected; } set { _isManagerSelected = value; if (_isManagerSelected == true && _isWorkerSelected == true) IsWorkerSelected = false; OnPropertyChanged(); } }
        public bool IsWorkerSelected { get { return _isWorkerSelected; } set { _isWorkerSelected = value; if (_isManagerSelected == true && _isWorkerSelected == true) IsManagerSelected = false; OnPropertyChanged(); } }
        public AdminManageUsersViewModel(UserService userService)
        {
            _userService = userService;
        }
        private void FilterByText()
        {
            CardsViewModelsToShow.Clear();
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].User.Name.StartsWith(SearchText,true,null)|| allUsers[i].User.Surname.StartsWith(SearchText, true, null)||allUsers[i].User.Username.StartsWith(SearchText, true, null) || SearchText.Length == 0)
                {
                    CardsViewModelsToShow.Add(allUsers[i]);
                }
            }
        }
        public override async System.Threading.Tasks.Task Initialize()
        {
            List<User> workersAndManagers=await _userService.GetManagersAndWorkers();
            for(int i = 0; i < workersAndManagers.Count; i++)
            {
                SingleUserCardAdminViewModel sm= new SingleUserCardAdminViewModel(workersAndManagers[i], _userService);
                allUsers.Add(sm);
                CardsViewModelsToShow.Add(sm);
            }
        }
        public override void Dispose()
        {
            allUsers.Clear();
            CardsViewModelsToShow.Clear();
            SearchText = "";
            IsWorkerSelected = false;
            IsWorkerSelected = false;
        }
    }
}
