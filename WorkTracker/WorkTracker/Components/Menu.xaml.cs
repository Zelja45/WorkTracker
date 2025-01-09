using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkTracker.Components.ViewModels;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.Utils.UtilityModels;
using WorkTracker.ViewModel;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public static MenuItemData _item;
        public Menu()
        {
            RelayCommand yesCommand = new RelayCommand( async o => { App.loginWindow = new LoginWindow { DataContext = App.serviceProvider.GetRequiredService<LoginViewModel>() }; 
                if (App.serviceProvider.GetRequiredService<UserStore>().User.AccountType == Utils.Constants.WorkerKeyWord)
                {
                    if(App.serviceProvider.GetRequiredService<WorkSessionViewModel>().CurrentSession!=null)
                    await App.serviceProvider.GetRequiredService<WorkSessionViewModel>().StopWorkingSession();
                }
                App.loginWindow.Show(); App.mainWindow.Close(); }, o => true);
            RelayCommand noCommand = new RelayCommand(o => { }, o => true);
            _item = new MenuItemData
            {
                IconKind = PackIconKind.Logout,
                Text = (string)Application.Current.Resources["Logout"],
                TextResourceKey = "Logout",
                ItemCommand = new ViewModel.Core.RelayCommand(o =>
                {
                    
                    new CustomDialog(true, true, (string)Application.Current.Resources["Logout"], (string)Application.Current.Resources["LogoutQuestion"], yesCommand, noCommand).Show();
  
                }, o => true)
            };
            DataContext = this;

            InitializeComponent();
        }
        public List<MenuItemData> MenuItemsData
        {
            get { return (List<MenuItemData>)GetValue(MenuItemsDataProperty); }
            set { SetValue(MenuItemsDataProperty, value); Debug.WriteLine(value.Count); }
        }
        public static readonly DependencyProperty MenuItemsDataProperty =
            DependencyProperty.Register("MenuItemsData", typeof(List<MenuItemData>), typeof(Menu));

        public MenuItemData LogoutItem
        {
            get { return _item; }
        }
    }
}
