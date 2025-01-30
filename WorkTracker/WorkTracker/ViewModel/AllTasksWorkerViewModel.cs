
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Components.ViewModels;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class AllTasksWorkerViewModel:BaseViewModel
    {
        private TaskService _taskService;
        private UserStore _userStore;

        private int _selectedTabIndex;
        private int _selectedSortIndex=0;
        private List<WorkerTaskCardViewModel> allTasks;
        private bool _showNoTasksLabel = false;

        public WorkerTaskCardViewModel? pinnedTask { get; set; }

        public ObservableCollection<WorkerTaskCardViewModel> TaskCardsToShow { get; set; } = new ObservableCollection<WorkerTaskCardViewModel>();
        public int SelectedTabIndex { get { return _selectedTabIndex; } set { _selectedTabIndex = value; SwitchTab(); OnPropertyChanged(); } }
        public int SelectedSortIndex { get { return _selectedSortIndex; } set { _selectedSortIndex = value; Sort(); OnPropertyChanged(); } }
        public bool ShowNoTasksLabel { get { return _showNoTasksLabel; } set { _showNoTasksLabel= value; OnPropertyChanged(); } }

        public AllTasksWorkerViewModel(TaskService taskService, UserStore userStore)
        {
            _taskService = taskService;
            _userStore = userStore;
        }

        public override async Task Initialize()
        {
            allTasks = new List<WorkerTaskCardViewModel>();
            List<Model.Task> tasks = await _taskService.getAllTasksOfWorker(_userStore.User.Username);
            Model.Task? pinnedTaskModel=await _taskService.GetPinnedTask(_userStore.User.Username); 
            foreach (Model.Task task in tasks) 
            {
                WorkerTaskCardViewModel wtc = new WorkerTaskCardViewModel(_taskService, task, true);
                if (pinnedTaskModel!=null&&task.IdTask == pinnedTaskModel?.IdTask) {
                    pinnedTask = wtc;
                    wtc.IsPinned = true;
                }
                wtc.UpdateStatusCallback = new RelayCommand(o => { TaskCardsToShow.Remove(wtc); }, o => true);
                wtc.PinTaskCommad = new RelayCommand(async o =>
                {
                    if (wtc.IsPinned)
                    {
                        if (pinnedTask != null && pinnedTask != wtc)
                        {
                            pinnedTask.IsPinned = false;
                            pinnedTask.Task.Pinned = 0;
                            await _taskService.UpdateTask(pinnedTask.Task);
                        }
                        pinnedTask=wtc;
                        wtc.Task.Pinned = 1;
                        await _taskService.UpdateTask(wtc.Task);
                    }
                    else
                    {
                        if (pinnedTask == wtc)
                        {
                            wtc.IsPinned = false;
                            wtc.Task.Pinned = 0;
                            await _taskService.UpdateTask(wtc.Task);
                            pinnedTask =null;
                        }
                    }

                }, o => true);
                allTasks.Add(wtc);
                if (task.Status == Utils.Constants.TODO && task.DueDate >= DateTime.Now)
                    TaskCardsToShow.Add(wtc);
            }
            Sort();
        }

        private void Sort()
        {
            if (SelectedSortIndex == 0)
            {
                allTasks.Sort((t1, t2) => t2.Task.CreatedAt.CompareTo(t1.Task.CreatedAt));
            }
            else if (SelectedSortIndex == 1)
            {
                allTasks.Sort((t1, t2) => t1.Task.CreatedAt.CompareTo(t2.Task.CreatedAt));
            }
            else if (SelectedSortIndex == 2)
            {
                allTasks.Sort((t1, t2) => t1.Task.DueDate.CompareTo(t2.Task.DueDate));
            }
            else if (SelectedSortIndex == 3)
            {
                allTasks.Sort((t1, t2) => t2.Task.DueDate.CompareTo(t1.Task.DueDate));
            }
            else if (SelectedSortIndex == 4)
            {
                allTasks.Sort((t1, t2) => t2.Task.Priority.CompareTo(t1.Task.Priority));
            }
            else if (SelectedSortIndex == 5)
            {
                allTasks.Sort((t1, t2) => t1.Task.Priority.CompareTo(t2.Task.Priority));
            }
            SwitchTab();
        }

        public void SwitchTab()
        {
            TaskCardsToShow.Clear();
            foreach (WorkerTaskCardViewModel task in allTasks)
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
                    if (task.Task.DueDate < DateTime.Now && task.Task.Status != Constants.Done)
                        TaskCardsToShow.Add(task);
                }
            }
            if (TaskCardsToShow.Count == 0)
            {
                ShowNoTasksLabel = true;
            }
            else
            {
                ShowNoTasksLabel = false;
            }

        }

        public override void Dispose()
        {
            SelectedTabIndex = 0;
            TaskCardsToShow.Clear();
        }
    }
}
