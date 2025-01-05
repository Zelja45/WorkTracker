using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Utils.UtilityModels
{
    public class MenuItemData
    {
        public PackIconKind IconKind { get; set; }
        public string Text { get; set; }
        public RelayCommand ItemCommand { get; set; }
        public bool IsSelectedInitialy { get; set; } = false;
    }
}
