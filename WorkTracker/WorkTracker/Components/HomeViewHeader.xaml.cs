using Microsoft.Extensions.DependencyInjection;
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
using WorkTracker.Components.ViewModels;

namespace WorkTracker.Components
{
    /// <summary>
    /// Interaction logic for HomeViewHeader.xaml
    /// </summary>
    public partial class HomeViewHeader : UserControl
    {
        public HomeViewHeader()
        {
            DataContext = App.serviceProvider.GetRequiredService<HomeViewHeaderModel>();
            InitializeComponent();
        }
    }
}
