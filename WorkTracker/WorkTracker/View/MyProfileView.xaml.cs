using System;
using System.Collections.Generic;
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
using WorkTracker.Components;
using WorkTracker.ViewModel;

namespace WorkTracker.View
{
    /// <summary>
    /// Interaction logic for MyProfileView.xaml
    /// </summary>
    public partial class MyProfileView : UserControl
    {
        public MyProfileView()
        {
            InitializeComponent();
        }

        private void ChangePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            OldPassword.Visibility = Visibility.Visible;
            ChangePasswordBtn.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OldPassword.Visibility = Visibility.Collapsed;
            ChangePasswordBtn.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangePassword.Visibility = Visibility.Collapsed;
            ChangePasswordBtn.Visibility = Visibility.Visible;
        }

        private void EditEmail_Click(object sender, RoutedEventArgs e)
        {
            NewEmail.IsEnabled = !NewEmail.IsEnabled;
        }

        private void EditPhone_Click(object sender, RoutedEventArgs e)
        {
            NewPhone.IsEnabled = !NewPhone.IsEnabled;
        }

        private void checkOldPassword_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext;

            if (dataContext is MyProfileViewModel viewModel)
            {
               if(viewModel.CheckOldPassword())
                {
                    OldPassword.Visibility = Visibility.Collapsed;
                    ChangePassword.Visibility = Visibility.Visible;
                }
            }

        }

        private async void confirmPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = this.DataContext;

            if (dataContext is MyProfileViewModel viewModel)
            {
               if( await viewModel.ChangePassword())
                {
                    ChangePassword.Visibility = Visibility.Collapsed;
                    ChangePasswordBtn.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
