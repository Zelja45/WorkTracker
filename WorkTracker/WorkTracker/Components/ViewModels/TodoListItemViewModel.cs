using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class TodoListItemViewModel:BaseViewModel
    {
        private Todolistitem todolistitem;
        private TODOListService todolistservice;

        public Todolistitem Todolistitem { get { return todolistitem; } }
        public sbyte IsChecked { get { return todolistitem.Checked; } set {todolistitem.Checked = value; UpdateItem(); OnPropertyChanged(); } }
        public TodoListItemViewModel(Todolistitem item,TODOListService todoService)
        {
            todolistservice = todoService;
            todolistitem = item;
        }
        private async System.Threading.Tasks.Task UpdateItem()
        {
            await todolistservice.UpdateItem(Todolistitem);
        }
    }
}
