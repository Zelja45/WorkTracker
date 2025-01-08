using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Components.ViewModels;
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
            MenuItems.Add(new MenuItemData() { TextResourceKey="Home",Text = (String)Application.Current.Resources["Home"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<AdminHomeViewModel>(); }, o => true), IconKind = PackIconKind.Home, IsSelectedInitialy = true });
            MenuItems.Add(new MenuItemData() { TextResourceKey = "AddUsers", Text = (String)Application.Current.Resources["AddUsers"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<AddNewUserViewModel>(); }, o => true), IconKind = PackIconKind.UserAdd });
            MenuItems.Add(new MenuItemData() { TextResourceKey = "ManageUsers", Text = (String)Application.Current.Resources["ManageUsers"], ItemCommand = new RelayCommand(async o => { App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true; await Navigation.NavigateTo<AdminManageUsersViewModel>(); App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false; }, o => true), IconKind = PackIconKind.AccountCog });
            MenuItems.Add(new MenuItemData() { TextResourceKey = "CreateSectors", Text = (String)Application.Current.Resources["CreateSectors"], ItemCommand = new RelayCommand(async o => { App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true; await Navigation.NavigateTo<AddSectorViewModel>(); App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false; }, o => true), IconKind = PackIconKind.AccountMultiplePlus });
            MenuItems.Add(new MenuItemData() { TextResourceKey = "MyProfile", Text = (String)Application.Current.Resources["MyProfile"], ItemCommand = new RelayCommand(async o => {  await Navigation.NavigateTo<MyProfileViewModel>(); }, o => true), IconKind = PackIconKind.AccountCircle });
            MenuItems.Add(new MenuItemData() { TextResourceKey = "Settings", Text = (String)Application.Current.Resources["Settings"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<SettingsViewModel>(); }, o => true), IconKind = PackIconKind.Settings });

        }
        public void PrepareWorkerUI()
       {
            MenuItems = new List<MenuItemData>();
            MenuItems.Add(new MenuItemData() { TextResourceKey = "Home", Text = (String)Application.Current.Resources["Home"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<ManagerHomeViewModel>(); }, o => true), IconKind = PackIconKind.Home, IsSelectedInitialy = true });
            
            MenuItems.Add(new MenuItemData() { TextResourceKey = "MyProfile", Text = (String)Application.Current.Resources["MyProfile"], ItemCommand = new RelayCommand(async o => { await Navigation.NavigateTo<MyProfileViewModel>(); }, o => true), IconKind = PackIconKind.AccountCircle });
            MenuItems.Add(new MenuItemData() { TextResourceKey = "Settings", Text = (String)Application.Current.Resources["Settings"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<SettingsViewModel>(); }, o => true), IconKind = PackIconKind.Settings });

            
        }
       public void PrepareManagerUI()
       {
            MenuItems = new List<MenuItemData>();
            MenuItems.Add(new MenuItemData() { TextResourceKey = "Home", Text = (String)Application.Current.Resources["Home"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<ManagerHomeViewModel>(); }, o => true), IconKind = PackIconKind.Home, IsSelectedInitialy = true });
            MenuItems.Add(new MenuItemData() { TextResourceKey = "YourSectors", Text = (String)Application.Current.Resources["YourSectors"], ItemCommand = new RelayCommand(async o => { App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = true; await Navigation.NavigateTo<ManageSectorsManagerViewModel>(); App.serviceProvider.GetRequiredService<LoadingCircleViewModel>().IsLoading = false; }, o => true), IconKind = PackIconKind.AccountGroup });


            MenuItems.Add(new MenuItemData() { TextResourceKey = "MyProfile", Text = (String)Application.Current.Resources["MyProfile"], ItemCommand = new RelayCommand(async o => { await Navigation.NavigateTo<MyProfileViewModel>(); }, o => true), IconKind = PackIconKind.AccountCircle });
            MenuItems.Add(new MenuItemData() { TextResourceKey = "Settings", Text = (String)Application.Current.Resources["Settings"], ItemCommand = new RelayCommand(o => { Navigation.NavigateTo<SettingsViewModel>(); }, o => true), IconKind = PackIconKind.Settings });

        }


    }
}
