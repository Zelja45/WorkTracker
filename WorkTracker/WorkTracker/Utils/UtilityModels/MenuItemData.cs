using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Utils.UtilityModels
{
    public class MenuItemData:ObeservableObject
    {
        private string _text;
        public string TextResourceKey { get; set; }
        public PackIconKind IconKind { get; set; }
        public string Text { get { return _text; } set { _text = value; OnPropertyChanged(); } }
        public RelayCommand ItemCommand { get; set; }
        public bool IsSelectedInitialy { get; set; } = false;
    }
}
