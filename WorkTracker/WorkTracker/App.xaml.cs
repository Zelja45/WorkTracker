using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Stores;
using WorkTracker.Utils;
using WorkTracker.View;
using WorkTracker.ViewModel;
using WorkTracker.ViewModel.Core;

namespace WorkTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider;
        public static LoginWindow loginWindow;
        public static MainWindow mainWindow;
        public App()
        {

            IServiceCollection services = new ServiceCollection();
            

            services.AddSingleton<INavigationService, NavigationServices>();
            services.AddSingleton<Func<Type, BaseViewModel>>(provider => viewModelType => (BaseViewModel)provider.GetRequiredService(viewModelType));//function for getting specific viewmodel

            
             
            services.AddSingleton<SettingsService>();
            services.AddSingleton<SettingsStore>();
            services.AddSingleton<SectorService>();
            services.AddSingleton<TaskService>();
            services.AddSingleton<WorksessionService>();
            services.AddSingleton<TODOListService>();
            services.AddSingleton<ThemeChanger>();
            services.AddSingleton(provider =>
                new LanguageChanger(Application.Current.Resources)
            );

            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();
            
            services.AddSingleton<LoadingCircleViewModel>();
            services.AddSingleton<AdminHomeViewModel>();
            services.AddSingleton<AddNewUserViewModel>();
            services.AddSingleton<AdminManageUsersViewModel>();
            services.AddSingleton<AddSectorViewModel>();
            services.AddSingleton<MyProfileViewModel>();
            services.AddSingleton<HeaderViewModel>();
            services.AddSingleton<ManagerHomeViewModel>();
            services.AddSingleton<HomeViewHeaderModel>();
            services.AddSingleton<AddNewTaskViewModel>();
            services.AddSingleton<ManageSectorsManagerViewModel>();
            services.AddSingleton<ManageSectorViewModel>();
            services.AddSingleton<AddWorkerInSectorViewModel>();
            services.AddSingleton<WorkerHomeViewModel>();
            services.AddSingleton<WorkSessionViewModel>();
            services.AddSingleton<AllTasksWorkerViewModel>();
            services.AddSingleton<TODOListViewModel>();
            services.AddSingleton<WorkerStatsViewModel>();
            services.AddSingleton<ManagerWorkerStatsViewModel>();
            services.AddSingleton<User>();


            services.AddSingleton<UserService>();
            services.AddSingleton<UserStore>();
            serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            
            serviceProvider.GetRequiredService<SettingsStore>().LoadSettings();
            serviceProvider.GetRequiredService<SettingsService>().ApplyCurrentSettings();
            serviceProvider.GetRequiredService<SettingsViewModel>().IsDarkThemeSetted = serviceProvider.GetRequiredService<SettingsService>().IsDarkThemeSetted;
            string languageCode=serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            string font= serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.FontCode;
            string colorCode = serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.PrimaryColorCode;
            serviceProvider.GetRequiredService<SettingsViewModel>().SelectedFont= font;
            serviceProvider.GetRequiredService<SettingsViewModel>().ChangeLanguage(languageCode);
            serviceProvider.GetRequiredService<SettingsViewModel>().SelectedColorIndex = ThemeChanger.ColorCodes.IndexOf(colorCode);
            loginWindow=new LoginWindow
            {
                DataContext = serviceProvider.GetRequiredService<LoginViewModel>()
            };
            loginWindow.Show();
            base.OnStartup(e);

        }
    }

}
