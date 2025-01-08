using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using WorkTracker.Model;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class TaskCardViewModel:BaseViewModel
    {
        private string _dueDateText;
        private string _createdAtText;
        public Model.Task Task { get; set; }
        public string WorkerNameSurname { get { return Task.WorkerUsernameNavigation.Name + " " + Task.WorkerUsernameNavigation.Surname; } }
        public RelayCommand DeleteTask { get;set; }
        public bool IsRemoveable { get { return Task.Status == Constants.TODO&&Task.DueDate>=DateTime.Now; } }
        public string DueDateText { get { return _dueDateText; } set { _dueDateText = value; OnPropertyChanged(); } }
        public string CreatedAtText { get { return _createdAtText; } set { _createdAtText = value;OnPropertyChanged(); } }
        public string PriorityText { get; set; }
        public TaskCardViewModel(Model.Task task,RelayCommand command)
        {
            Task = task;
            DeleteTask = command;
            string languageCode = App.serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            DueDateText = Task.DueDate.ToString("f", new CultureInfo(languageCode));
            CreatedAtText = Task.CreatedAt.ToString("f", new CultureInfo(languageCode));
            if (task.Priority == 0)
                PriorityText = (string)Application.Current.Resources["Low"];
            else if (task.Priority == 1)
                PriorityText = (string)Application.Current.Resources["Medium"];
            else if (task.Priority == 2)
                PriorityText = (string)Application.Current.Resources["High"];
        }

    }
}
