using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class WorkerHomeViewModel:BaseViewModel
    {
        private TaskService _taskService;
        private UserStore _userStore;
        private SectorService _sectorService;
        private WorkerTaskCardViewModel? _pinnedTaskCardViewModel;
        private bool _noTaskLabelVisibility;
        private bool _pinnedTaskVisibility;

        public bool NoTaskLabelVisibility{ get { return _noTaskLabelVisibility; } set { _noTaskLabelVisibility = value;OnPropertyChanged(); } }
        public bool PinnedTaskVisibility { get { return _pinnedTaskVisibility; } set { _pinnedTaskVisibility = value;OnPropertyChanged(); } }
        public string HourlyRate { get; set; }
        public string OvertimeHourlyRate {  get; set; }    
        public string SectorName {  get; set; }
        public WorkerTaskCardViewModel? PinnedTaskCardViewModel { get { return _pinnedTaskCardViewModel; } set { _pinnedTaskCardViewModel = value; OnPropertyChanged(); } }

        public WorkerHomeViewModel(TaskService taskService, UserStore userStore, SectorService sectorService)
        {
            _taskService = taskService;
            _userStore = userStore;
            _sectorService = sectorService;
        }

        public override async System.Threading.Tasks.Task Initialize()
        {
            if (_userStore.User.IdSector == null)
            {
                SectorName = (string)Application.Current.Resources["NoAssignedSector"];
                OvertimeHourlyRate = "-";
                HourlyRate = "-";
            }
            else
            {
                Sector sector = await _sectorService.GetSector((int)_userStore.User.IdSector);
                SectorName = sector.Name;
                OvertimeHourlyRate = sector.HourlyRate.ToString("F2");
                HourlyRate = sector.HourlyRate.ToString("F2");
            }

            Model.Task pinnedTask = await _taskService.GetPinnedTask(_userStore.User.Username);
            if (pinnedTask != null)
            {
                PinnedTaskCardViewModel=new WorkerTaskCardViewModel(_taskService, pinnedTask,true);
                PinnedTaskCardViewModel.IsPinned = true;
                PinnedTaskCardViewModel.PinTaskCommad = new RelayCommand(async o => 
                {
                        pinnedTask.Pinned = 0;
                        await _taskService.UpdateTask(pinnedTask);
                        PinnedTaskCardViewModel = null;
                        PinnedTaskVisibility = false;
                        NoTaskLabelVisibility = true;
                }, o => true);
                PinnedTaskVisibility = true;
                NoTaskLabelVisibility = false;
            }
            else
            {
                PinnedTaskVisibility = false;
                NoTaskLabelVisibility = true;
            }
            App.serviceProvider.GetRequiredService<TODOListViewModel>().Initialize();
        }
    }
}
