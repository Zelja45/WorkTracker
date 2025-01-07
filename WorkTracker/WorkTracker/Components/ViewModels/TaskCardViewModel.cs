using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkTracker.Model;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class TaskCardViewModel:BaseViewModel
    {
        public Model.Task Task { get; set; }
        public string WorkerNameSurname { get { return Task.WorkerUsernameNavigation.Name + " " + Task.WorkerUsernameNavigation.Surname; } }
        public RelayCommand DeleteTask { get;set; }
        public bool IsRemoveable { get { return Task.Status == Constants.TODO&&Task.DueDate>=DateTime.Now; } }
        public TaskCardViewModel(Model.Task task)
        {
            Task = task;
            DeleteTask = new RelayCommand(o => { }, o => true);
        }

    }
}
