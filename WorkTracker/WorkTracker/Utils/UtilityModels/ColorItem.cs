using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Utils.UtilityModels
{
    public class ColorItem:ObeservableObject
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        public string NameKey { get; set; }
        public Brush Color { get; set; }
    }
}
