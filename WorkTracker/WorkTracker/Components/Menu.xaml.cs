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
using WorkTracker.Utils.UtilityModels;

namespace WorkTracker.Components
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
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

    }
}
