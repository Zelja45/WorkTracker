using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Components.ViewModels;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class WorkerHomeViewModel:BaseViewModel
    {
        public override async Task Initialize()
        {
            App.serviceProvider.GetRequiredService<TODOListViewModel>().Initialize();
        }
    }
}
