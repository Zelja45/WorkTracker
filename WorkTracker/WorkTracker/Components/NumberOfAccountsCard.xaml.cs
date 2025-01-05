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
    /// Interaction logic for NumberOfAccountsCard.xaml
    /// </summary>
    public partial class NumberOfAccountsCard : UserControl
    {
        public NumberOfAccountsCard()
        {
            DataContext = this;
            InitializeComponent();
        }
        public string TopLabel
        {
            get;
            set;
        }
        public string NumberActive
        {
            get;
            set;
        }

        
       


       
        public string NumberOfDeactivatedAccounts
        {
            get;
            set;
        }

        
    
    }
}
