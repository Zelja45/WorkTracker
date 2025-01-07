using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Stores;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.Components.ViewModels
{
    public class HomeViewHeaderModel:BaseViewModel
    {
        public UserStore UserStore { get; set; }
        public string LabelTitle
        {
            get
            {
                DateTime now = DateTime.Now;
                if ((now.Hour >= 21 && now.Hour <= 23) || (now.Hour >= 0 && now.Hour <= 5))
                {
                    return Application.Current.Resources["GoodNight"] as String;
                }
                else if (now.Hour >= 6 && now.Hour <= 10)
                {
                    return Application.Current.Resources["GoodMorning"] as String;
                }
                else if (now.Hour >= 11 && now.Hour <= 17)
                {
                    return Application.Current.Resources["GoodDay"] as String;
                }
                else
                {
                    return Application.Current.Resources["GoodEvening"] as String;
                }
            }
        }
        public string UserNameSurname
        {
            get
            {
                return UserStore.User.Name + " " + UserStore.User.Surname;
            }
        }
        public HomeViewHeaderModel(UserStore userStore)
        {
            UserStore = userStore;
        }
    }
}
