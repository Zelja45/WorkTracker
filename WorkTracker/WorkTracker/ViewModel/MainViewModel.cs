using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Services;
using WorkTracker.Utils.UtilityModels;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
       private INavigationService _navigationService;
        public INavigationService Navigation { get { return _navigationService; } }
        public List<MenuItemData> MenuItems{ get; set; }
       public MainViewModel(INavigationService navigationService)
       {
            _navigationService = navigationService;   

       } 
       public void PrepareAdminUI()
       {
            MenuItems = new List<MenuItemData>();
            MenuItems.Add(new MenuItemData() { Text = (String)Application.Current.Resources["Home"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<AdminHomeViewModel>(); }, o => true), IconKind = PackIconKind.Home, IsSelectedInitialy = true });
            MenuItems.Add(new MenuItemData() { Text = (String)Application.Current.Resources["AddUsers"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<AddNewUserViewModel>(); }, o => true), IconKind = PackIconKind.UserAdd });
        }
       public void PrepareWorkerUI()
       {
            MenuItems = new List<MenuItemData>();
            MenuItems.Add(new MenuItemData() { Text = "Radnik 1", ItemCommand = new RelayCommand(o => { }, o => true), IconKind = PackIconKind.ThumbsDownOutline });
            MenuItems.Add(new MenuItemData() { Text = "Radnik 2", ItemCommand = new RelayCommand(o => { }, o => true), IconKind = PackIconKind.ThumbsUp });
        }
       public void PrepareManagerUI()
       {
            MenuItems = new List<MenuItemData>();
            MenuItems.Add(new MenuItemData() { Text = "Menadzer 1", ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<WorkerMainViewModel>(); }, o => true), IconKind = PackIconKind.ThumbsDownOutline });
            MenuItems.Add(new MenuItemData() { Text = "Menadzer 2", ItemCommand = new RelayCommand(o => { }, o => true), IconKind = PackIconKind.ThumbsUp });
        }
        
       
    }
}
