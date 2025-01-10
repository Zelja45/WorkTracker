using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class TODOListViewModel:BaseViewModel
    {
        private UserStore _userStore;
        private TODOListService _toDOListService;

       
        private Todolist _selectedList;
        private string _newItemContent="";
        private bool _isAddable = false;
        private string _newListTitle = "";
        private bool _newListAddable = false;
        private bool _noCreatedTODO = false;

        public Todolist SelectedList { get { return _selectedList; } set { _selectedList = value; OnPropertyChanged(); } }

        public ObservableCollection<Todolist> TodoLists = new ObservableCollection<Todolist>();

        public ObservableCollection<TodoListItemViewModel> Items { get; set; } = new ObservableCollection<TodoListItemViewModel>();

        public ObservableCollection<PopupBoxItemViewModel> popupBoxItemViewModels { get; set; } = new ObservableCollection<PopupBoxItemViewModel>();
        public bool IsAddable { get { return _isAddable; } set { _isAddable = value;  OnPropertyChanged(); } }

        public bool IsNewListAddable { get { return _newListAddable; } set { _newListAddable=value; OnPropertyChanged(); } } 

        public bool NoCreatedTODO { get { return _noCreatedTODO; } set { _noCreatedTODO = value; OnPropertyChanged(); } }


        public string NewListTitle { get { return _newListTitle; } set { _newListTitle = value; if (value.Length != 0) IsNewListAddable = true; else IsNewListAddable = false; OnPropertyChanged(); } }

        public string NewItemContent { get { return _newItemContent; } set { _newItemContent = value; if (value.Length != 0&&SelectedList!=null) IsAddable = true; else IsAddable = false; OnPropertyChanged(); } }

        public RelayCommand AddNewTodolistItem { get; set; }
        public RelayCommand AddNewTodolistCommand { get; set; }    

        public TODOListViewModel(UserStore userStore, TODOListService toDOListService)
        {
            _userStore = userStore;
            _toDOListService = toDOListService;
            AddNewTodolistItem = new RelayCommand(async o => { await AddNewListItem(); }, o => true);
            AddNewTodolistCommand = new RelayCommand(async o => { await AddNewList(); }, o => true);
        }
        private async System.Threading.Tasks.Task AddNewListItem()
        {
            Todolistitem item = new Todolistitem
            {
                 Content = _newItemContent,
                 IdTodolist=SelectedList.IdTodolist,
                 Checked=0
            };
            await _toDOListService.AddNewItem(item);
            Items.Add(new TodoListItemViewModel(item, _toDOListService));
            SelectedList.Todolistitems.Add(item);
            NewItemContent = "";
        }
        private async System.Threading.Tasks.Task AddNewList()
        {
            Todolist list = new Todolist
            {
                Title = NewListTitle,
                WorkerUsername=_userStore.User.Username,
                IsSelected=0
            };
            NewListTitle = "";
            
            await _toDOListService.AddNewList(list);
            PopupBoxItemViewModel vm = new PopupBoxItemViewModel(list, new RelayCommand(o => { SelectedList = list; DrawItems(); }, o => true));
            popupBoxItemViewModels.Add(vm);
        }

        public override async System.Threading.Tasks.Task Initialize()
        {
            _newItemContent = "";
            _newListTitle = "";
            _newListAddable = false;
            _isAddable = false;
            SelectedList = null;
            NoCreatedTODO = false;
            Items.Clear();
            TodoLists.Clear();
            var allLists = await _toDOListService.GetWorkerTodolists(_userStore.User.Username);
            popupBoxItemViewModels.Clear();
            foreach (var list in allLists) 
            {
                TodoLists.Add(list);


                PopupBoxItemViewModel vm=new PopupBoxItemViewModel(list,new RelayCommand(o => {
                    if (SelectedList != null) { SelectedList.IsSelected = 0; _toDOListService.UpdateListIsSelected(SelectedList); }
                    SelectedList = list; list.IsSelected = 1; DrawItems();
                    NoCreatedTODO = false;
                    _toDOListService.UpdateListIsSelected(list);
                },o=>true));

                popupBoxItemViewModels.Add(vm);
                if (list.IsSelected == 1)
                    SelectedList = list;

            }
            if(SelectedList != null)
            {
                DrawItems();
                NoCreatedTODO = false;
            }
            else
            {
                NoCreatedTODO = true;
            }
        }
        private void DrawItems()
        {
            Items.Clear();
            foreach (var item in SelectedList.Todolistitems)
            {
                TodoListItemViewModel vm = new TodoListItemViewModel(item, _toDOListService);
                Items.Add(vm);
            }
        }

    }
    public class PopupBoxItemViewModel : BaseViewModel
    {
        public Todolist Todolist { get; set; }
        public RelayCommand SelectCommand { get; set; }
        
        public PopupBoxItemViewModel(Todolist todolist, RelayCommand relayCommand)
        {
            Todolist = todolist;
            SelectCommand = relayCommand;
        }

    }
}
