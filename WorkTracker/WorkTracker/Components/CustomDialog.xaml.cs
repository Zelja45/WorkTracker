using Org.BouncyCastle.Pqc.Crypto.Lms;
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
using System.Windows.Shapes;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components
{
    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        private bool yesNo;
        private bool infoMessage;
        private string title;
        private string message;
        private RelayCommand yesCommand;
        private RelayCommand noCommand;

        public RelayCommand YesCommand { get{ return yesCommand;} }
        public RelayCommand NoCommand { get { return noCommand;} }
        public CustomDialog(bool yesNo, bool infoMessage, string title, string message,RelayCommand? yesCommand=null,RelayCommand? noCommand=null)
        {
            DataContext = this;
            this.yesNo = yesNo;
            this.infoMessage = infoMessage;
            this.title = title;
            this.message = message;
            this.yesCommand = yesCommand??new RelayCommand(o => { },o=>true);
            this.noCommand = noCommand ?? new RelayCommand(o => { }, o => true);
            InitializeComponent();

            Title.Text = title;
            Description.Text = message;
            Icon.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (infoMessage == false)
            {
                Icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Dangerous;
                Icon.Foreground = Brushes.Red;
            }
            if (yesNo)
            {
                YesNoPanel.Visibility = Visibility.Visible;
            }
            else
            {
                OKPanel.Visibility = Visibility.Visible;
            }
            this.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.Source is not Button)
            {
                this.DragMove();
            }
        }

        private void noBtn_Click(object sender, RoutedEventArgs e)
        {
            noCommand.Execute(null);
            this.Close();
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            yesCommand.Execute(null);
            this.Close();
        }
    }
}
