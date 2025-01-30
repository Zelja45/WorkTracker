using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Components;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class ManagerHomeViewModel:BaseViewModel
    {
        private INavigationService _navigationService;
        private UserService _userService;
        private SectorService _sectorService;
        private TaskService _taskService;
        private UserStore _userStore;
        private int _numberOfMyWorkers;
        private int _numberOfMySectors;
        private int _selectedTabIndex;
        private List<TaskCardViewModel> allTasks;
        private bool _noTasks = false;

        public bool NoTasks { get { return _noTasks; } set { _noTasks = value;OnPropertyChanged(); } }
        public int NumberOfMyWorkers { get { return _numberOfMyWorkers; } set { _numberOfMyWorkers = value;OnPropertyChanged(); } }
        public int NumberOfMySectors { get { return _numberOfMySectors; } set { _numberOfMySectors = value; OnPropertyChanged(); } }
        public ObservableCollection<TaskCardViewModel> TaskCardsToShow { get; set; } = new ObservableCollection<TaskCardViewModel>();
        public int SelectedTabIndex { get { return _selectedTabIndex; } set { _selectedTabIndex = value; SwitchTab(); OnPropertyChanged(); } }
        public RelayCommand AddNewTaskCommand { get; set; }
        public ManagerHomeViewModel(UserService userService,UserStore userStore,SectorService sectorService,TaskService taskService,INavigationService navigationService)
        {
            _navigationService = navigationService;
            _userService = userService;
            _userStore = userStore;
            _taskService = taskService;
            _sectorService = sectorService;
            AddNewTaskCommand = new RelayCommand(o => { _navigationService.NavigateTo<AddNewTaskViewModel>(); }, o => true);
        }
        public override async System.Threading.Tasks.Task Initialize()
        {
            allTasks = new List<TaskCardViewModel>();
            NumberOfMySectors = await _sectorService.CountNumberOfMenagersSectors(_userStore.User.Username);
            List<User> workers= await _sectorService.GetAllManagerWorkers(_userStore.User.Username);
            NumberOfMyWorkers = workers.Count();
            List<Model.Task> tasks = await _taskService.GetAllTasksOfManager(_userStore.User.Username);
            foreach (Model.Task task in tasks)
            {
                TaskCardViewModel card=null;
                RelayCommand command = new RelayCommand(o => 
                {
                    new CustomDialog(true,true,"Uklanjanje zadatka","Da li ste sigurni da zelite ukloiti zadatak",new RelayCommand(o => {_taskService.DeleteTask(task); TaskCardsToShow.Remove(card); },o=>true)).Show();
                }, o => true);
                card=new TaskCardViewModel(task,command);
                allTasks.Add(card);
                if(task.Status==Constants.TODO && task.DueDate >= DateTime.Now)
                    TaskCardsToShow.Add(card); 
            }
            NoTasks = TaskCardsToShow.Count == 0;
        }
        public void SwitchTab()
        {
            TaskCardsToShow.Clear(); 
            foreach(TaskCardViewModel task in allTasks)
            {
                if (SelectedTabIndex == 0)
                {
                    if (task.Task.Status == Constants.TODO && task.Task.DueDate >= DateTime.Now)
                        TaskCardsToShow.Add(task);
                }
                else if (SelectedTabIndex == 1)
                {
                    if (task.Task.Status == Constants.Doing && task.Task.DueDate >= DateTime.Now)
                        TaskCardsToShow.Add(task);
                }
                else if (SelectedTabIndex == 2)
                {
                    if (task.Task.Status == Constants.Done)
                        TaskCardsToShow.Add(task);
                }
                else if (SelectedTabIndex == 3)
                {
                    if ( task.Task.DueDate < DateTime.Now&& task.Task.Status != Constants.Done)
                        TaskCardsToShow.Add(task);
                }
            }
            NoTasks = TaskCardsToShow.Count == 0;
            
        }
        public override void Dispose()
        {
            SelectedTabIndex = 0;
            TaskCardsToShow.Clear();
            NoTasks = false;
        }
    }
}
