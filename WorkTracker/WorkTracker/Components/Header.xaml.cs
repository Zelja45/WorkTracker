using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
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
using WorkTracker.Stores;
using WorkTracker.Utils.UtilityModels;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        
        
        public Header()
        {
            DataContext = App.serviceProvider.GetRequiredService<HeaderViewModel>();
            InitializeComponent();
        }

        private void maximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    window.WindowState = WindowState.Normal;
                }
                else
                {
                    double cornerAdjustment = 15; // Podešavanje za CornerRadius
                    double adjustment = 2; // Dodatna kompenzacija
                    window.MaxHeight = SystemParameters.WorkArea.Height+10;
                    window.MaxWidth = SystemParameters.WorkArea.Width+10;
                    window.WindowState = WindowState.Maximized;
                }
            }

        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            RelayCommand yesCommand = new RelayCommand(async o => {
                if (App.serviceProvider.GetRequiredService<UserStore>().User.AccountType == Utils.Constants.WorkerKeyWord)
                {
                    if (App.serviceProvider.GetRequiredService<WorkSessionViewModel>().CurrentSession != null)
                        await App.serviceProvider.GetRequiredService<WorkSessionViewModel>().StopWorkingSession();
                }
                Application.Current.Shutdown();
            }, o => true);
            RelayCommand noCommand = new RelayCommand(o => { }, o => true);
            new CustomDialog(true, true, (string)Application.Current.Resources["Logout"], (string)Application.Current.Resources["LogoutQuestion"], yesCommand, noCommand).Show();

        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this); 
            if (window != null)
            {
                window.WindowState = WindowState.Minimized; 
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window window = Window.GetWindow(this);
                if (window != null)
                {
                    window.DragMove();
                }
            }
                
        }

        private void ShowCalendarButon_Click(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = !CalendarPopup.IsOpen;
        }
    }
}
