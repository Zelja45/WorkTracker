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
        public App()
        {

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<INavigationService, NavigationServices>();
            services.AddSingleton<Func<Type, BaseViewModel>>(provider => viewModelType => (BaseViewModel)provider.GetRequiredService(viewModelType));//function for getting specific viewmodel

            services.AddSingleton<LoginWindow>(provider => new LoginWindow
            {
                DataContext = provider.GetRequiredService<LoginViewModel>()
            });
             
            services.AddSingleton<SettingsService>();
            services.AddSingleton<SettingsStore>();
            services.AddSingleton<SectorService>();
            services.AddSingleton<ThemeChanger>();
            services.AddSingleton(provider =>
                new LanguageChanger(Application.Current.Resources)
            );

            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<WorkerMainViewModel>();
            services.AddSingleton<LoadingCircleViewModel>();
            services.AddSingleton<AdminHomeViewModel>();
            services.AddSingleton<AddNewUserViewModel>();
            services.AddSingleton<AdminManageUsersViewModel>();
            services.AddSingleton<AddSectorViewModel>();
            services.AddSingleton<MyProfileViewModel>();
            services.AddSingleton<HeaderViewModel>();
            services.AddSingleton<User>();


           

            services.AddSingleton<UserService>();
            services.AddSingleton<UserStore>();
            serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var loginWindow = serviceProvider.GetRequiredService<LoginWindow>();
            serviceProvider.GetRequiredService<SettingsStore>().LoadSettings();
            serviceProvider.GetRequiredService<SettingsService>().ApplyCurrentSettings();
            serviceProvider.GetRequiredService<SettingsViewModel>().IsDarkThemeSetted = serviceProvider.GetRequiredService<SettingsService>().IsDarkThemeSetted;
            string languageCode=serviceProvider.GetRequiredService<SettingsStore>().CurrentSettings.LanguageCode;
            serviceProvider.GetRequiredService<SettingsViewModel>().ChangeLanguage(languageCode);
            loginWindow.Show();
            base.OnStartup(e);

        }
    }

}
