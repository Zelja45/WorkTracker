using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Components.ViewModels;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class ManagerHomeViewModel:BaseViewModel
    {
        private UserService _userService;
        private SectorService _sectorService;
        private TaskService _taskService;
        private UserStore _userStore;
        private int _numberOfMyWorkers;
        private int _numberOfMySectors;
        private int _selectedTabIndex;
        private List<Model.Task> allTasks;

        public int NumberOfMyWorkers { get { return _numberOfMyWorkers; } set { _numberOfMyWorkers = value;OnPropertyChanged(); } }
        public int NumberOfMySectors { get { return _numberOfMySectors; } set { _numberOfMySectors = value; OnPropertyChanged(); } }
        public ObservableCollection<TaskCardViewModel> TaskCardsToShow { get; set; } = new ObservableCollection<TaskCardViewModel>();
        public int SelectedTabIndex { get { return _selectedTabIndex; } set { _selectedTabIndex = value; SwitchTab(); OnPropertyChanged(); } }
        public ManagerHomeViewModel(UserService userService,UserStore userStore,SectorService sectorService,TaskService taskService)
        {
            _userService = userService;
            _userStore = userStore;
            _taskService = taskService;
            _sectorService = sectorService;
        }
        public override async Task Initialize()
        {
            NumberOfMySectors = await _sectorService.CountNumberOfMenagersSectors(_userStore.User.Username);
            NumberOfMyWorkers = await _sectorService.CountNumberOfWorkersInAllSectorsOfManager(_userStore.User.Username);
            allTasks = await _taskService.GetAllTasksOfManager(_userStore.User.Username);
            foreach (Model.Task task in allTasks)
            { 
                if(task.Status==Constants.TODO && task.DueDate >= DateTime.Now)
                    TaskCardsToShow.Add(new TaskCardViewModel(task)); 
            }
        }
        public void SwitchTab()
        {
            TaskCardsToShow.Clear(); 
            foreach(Model.Task task in allTasks)
            {
                if (SelectedTabIndex == 0)
                {
                    if (task.Status == Constants.TODO && task.DueDate >= DateTime.Now)
                        TaskCardsToShow.Add(new TaskCardViewModel(task));
                }
                else if (SelectedTabIndex == 1)
                {
                    if (task.Status == Constants.Doing && task.DueDate >= DateTime.Now)
                        TaskCardsToShow.Add(new TaskCardViewModel(task));
                }
                else if (SelectedTabIndex == 2)
                {
                    if (task.Status == Constants.Done)
                        TaskCardsToShow.Add(new TaskCardViewModel(task));
                }
                else if (SelectedTabIndex == 3)
                {
                    if ( task.DueDate < DateTime.Now&& task.Status != Constants.Done)
                        TaskCardsToShow.Add(new TaskCardViewModel(task));
                }
            }
            
        }
        public override void Dispose()
        {
            SelectedTabIndex = 0;
            TaskCardsToShow.Clear();
        }
    }
}
