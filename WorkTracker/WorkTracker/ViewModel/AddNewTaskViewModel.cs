
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Components;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.View;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class AddNewTaskViewModel:BaseViewModel
    {

        private UserStore _userStore;
        private INavigationService _navigationService;
        private SectorService _sectorService;
        private TaskService _taskService;

        private bool _noWorkers = false;
        private string _name="";
        private string _description="";
        private DateTime? _date=null;
        private DateTime? _time=null;
        private int _priority=-1;
        private bool _isAdddable=false;
        private ManagerWorkerAddingSectorCardViewModel? _selectedWorker = null;

        public bool NoWorkers { get { return _noWorkers; } set { _noWorkers = value;OnPropertyChanged(); } }

        public string Name { get { return _name; } set { _name = value; CheckIsAddable(); OnPropertyChanged(); } }
        public string Description { get { return _description; } set { _description = value; CheckIsAddable(); OnPropertyChanged(); } }
        public DateTime? Date { get { return _date; } set { _date = value; CheckIsAddable(); OnPropertyChanged(); } }  
        public DateTime? Time { get { return _time; } set { _time = value; CheckIsAddable(); OnPropertyChanged(); } }
        public int Priority { get { return _priority; } set { _priority = value; CheckIsAddable(); OnPropertyChanged(); } }
        public ManagerWorkerAddingSectorCardViewModel? SelectedWorker { get { return _selectedWorker; } set { _selectedWorker = value; CheckIsAddable(); OnPropertyChanged(); } }
        public bool IsAddable { get { return _isAdddable; } set { _isAdddable = value;  OnPropertyChanged(); } }
        public List<ManagerWorkerAddingSectorCardViewModel> Cards { get; set; }
        public RelayCommand NavigateBack {  get; set; }
        public RelayCommand AddNewTaskCommand { get; set; }    

        public AddNewTaskViewModel(UserStore userStore,SectorService sectorService,TaskService taskService,INavigationService navigationService)
        {
            _userStore = userStore;
            _sectorService = sectorService;
            _taskService = taskService;
            _navigationService = navigationService;
            NavigateBack = new RelayCommand(async o => { await _navigationService.NavigateTo<ManagerHomeViewModel>(); }, o => true);
            AddNewTaskCommand = new RelayCommand(async o => { await AddNewTask(); }, o => true);
        }
        private void CheckIsAddable()
        {
            if(_name.Length!=0&&_description.Length!=0&&_date!=null&& _time!=null && _priority != -1 &&_selectedWorker!=null)
            {
                IsAddable = true;
            }else
                IsAddable=false;
        }
        private async System.Threading.Tasks.Task AddNewTask()
        {
            App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true;
            DateTime dueDate=DateTime.Now;

            if (_date.HasValue&&_time.HasValue)
            {
                dueDate = _date.Value.Date.Add(_time.Value.TimeOfDay);
                if (dueDate < DateTime.Now) 
                {
                    new CustomDialog(false, false, (string)Application.Current.Resources["Erorr"], (string)Application.Current.Resources["ChooseCorrectDate"]).Show();
                    App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
                    return;
                }
            }
            Model.Task task = new Model.Task
            {
                CreatedAt = DateTime.Now,
                Description = _description,
                DueDate = dueDate,
                Priority = _priority,
                Progress = 0,
                Status = (sbyte)Constants.TODO,
                Title = _name,
                WorkerUsername = SelectedWorker.User.Username
            };
            await _taskService.AddNewTask(task, SelectedWorker.User);
            App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false;
            new CustomDialog(false, true, (string)Application.Current.Resources["TaskCreatedTitle"], (string)Application.Current.Resources["TaskCreatedDescription"]).Show();
            Reset();


        }
        private void Reset()
        {
            Name = "";
            Description = "";
            Priority = -1;
            IsAddable = false;
            Date = null;
            Time = null;
            NoWorkers = false;
            if (SelectedWorker != null)
            {
                SelectedWorker.IsChecked = false;
            }
            SelectedWorker = null;
        }
        public override void Dispose()
        {
            Reset();
        }
        public override async System.Threading.Tasks.Task Initialize()
        {
            List<User> workers = await _sectorService.GetAllManagerWorkers(_userStore.User.Username);
            Cards=new List<ManagerWorkerAddingSectorCardViewModel>();
            foreach (var worker in workers) 
            {
                ManagerWorkerAddingSectorCardViewModel card=new ManagerWorkerAddingSectorCardViewModel(worker);
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
                Cards.Add(card);
            }
            NoWorkers = Cards.Count == 0;
        }
    }
}
