using Assignment_Product_Manager.Core.Database;
using Assignment_Product_Manager.Repositories;
using Assignment_Product_Manager.Repositories.Interfaces;
using Assignment_Product_Manager.Services;
using Assignment_Product_Manager.Services.Interfaces;
using Assignment_Product_Manager.ViewModels;
using Assignment_Product_Manager.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;


namespace Assignment_Product_Manager
{

    public partial class App : Application, IDisposable
    {

        private static ServiceProvider? _serviceProvider;
        public static IServiceProvider Services => _serviceProvider!;
        public static Window MainWindow { get; private set; } = null!;

        public App()
        {
            InitializeComponent();
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            try
            {
                var config = await LoadConfigurationAsync();
                //configuration are external settings that control how are application behaves

                ConfigureServices(config);
                InitializeMainWindow();

                NavigateToLogin();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"startup error: {ex}");
            }
        }
        private static async Task<IConfiguration> LoadConfigurationAsync()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///appsettings.json"));

            using var stream = await file.OpenStreamForReadAsync();
            //using var automatically disposes the stream after the configuration is built

            return new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
        }


        private static void ConfigureServices(IConfiguration config)
        {
            var services = new ServiceCollection();

            services.AddSingleton(config);
            services.AddSingleton<DbConnectionFactory>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IProductService, ProductService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<AddEditProductViewModel>();

            _serviceProvider = services.BuildServiceProvider();
        }

        private static void InitializeMainWindow()
        {
            MainWindow = new MainWindow();

            MainWindow.Closed += (_, _) =>
            {
                _serviceProvider?.Dispose();
            };

            var frame = new Frame();
            MainWindow.Content = frame;

            MainWindow.Activate();
        }

        private static void NavigateToLogin()
        {
            if (MainWindow.Content is not Frame frame)
                throw new InvalidOperationException("Root frame not initialized");

            if (!frame.Navigate(typeof(LoginView)))
                throw new InvalidOperationException("Failed to navigate to LoginView");
        }

        public static T GetService<T>() where T : class =>
            Services.GetRequiredService<T>();

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}
