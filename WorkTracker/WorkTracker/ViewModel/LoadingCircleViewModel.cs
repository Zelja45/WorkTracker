using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class LoadingCircleViewModel:BaseViewModel
    {
        private bool _isLoading = false;
        public bool IsLoading { get { return _isLoading; } set { _isLoading = value; OnPropertyChanged(); } }
    }
}
