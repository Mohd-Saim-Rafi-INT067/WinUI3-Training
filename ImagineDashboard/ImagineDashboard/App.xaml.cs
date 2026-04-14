using Microsoft.UI.Xaml;
using System;
using ImagineDashboard.Services;
using ImagineDashboard.Services.Interface;
using ImagineDashboard.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ImagineDashboard
{
    public partial class App : Application
    {
        private Window? _window;
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            ConfigureServices();
            UnhandledException += App_UnhandledException;
        }

        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services - one instance for the entire application
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<PatientsViewModel>();
            services.AddSingleton<PatientDetailsViewModel>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<SidebarViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}