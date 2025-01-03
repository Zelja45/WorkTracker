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

namespace WorkTracker.Components
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
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
                    window.WindowState = WindowState.Maximized;
                }
            }

        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
    }
}
