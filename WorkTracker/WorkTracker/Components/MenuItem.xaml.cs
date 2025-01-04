using MaterialDesignThemes.Wpf;
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
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components
{
    /// <summary>
    /// Interaction logic for MenuItem.xaml
    /// </summary>
    public partial class MenuItem : UserControl
    {
        public MenuItem()
        {
           // DataContext = this;
            InitializeComponent();
        }
        public static readonly DependencyProperty IconKindProperty =
        DependencyProperty.Register("IconKind", typeof(PackIconKind), typeof(MenuItem));

        public PackIconKind IconKind
        {
            get { return (PackIconKind)GetValue(IconKindProperty); }
            set { SetValue(IconKindProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MenuItem));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(MenuItem));
        public RelayCommand ItemCommand
        {
            get { return (RelayCommand)GetValue(ItemCommandProperty); }
            set { SetValue(ItemCommandProperty, value); }
        }
        public static readonly DependencyProperty ItemCommandProperty =
           DependencyProperty.Register("ItemCommand", typeof(RelayCommand), typeof(MenuItem));
    }
}
