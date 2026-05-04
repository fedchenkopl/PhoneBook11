using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook10.Services;
using PhoneBook10.ViewModels;
using PhoneBook10.Views;

namespace PhoneBook10
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
               

                var serviceCollection = new ServiceCollection();



                serviceCollection.AddSingleton<IDialogService, DialogService>();
                serviceCollection.AddSingleton<INavigationService, NavigationService>();

                serviceCollection.AddTransient<ContactsListViewModel>();
                serviceCollection.AddTransient<AboutViewModel>();
                serviceCollection.AddTransient<ContactEditViewModel>();

                serviceCollection.AddSingleton<MainWindowViewModel>();
                serviceCollection.AddSingleton<MainWindow>(sp =>
                {
                    var window = new MainWindow();
                    window.DataContext = sp.GetRequiredService<MainWindowViewModel>();
                    return window;
                });


                _serviceProvider = serviceCollection.BuildServiceProvider();

                var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ОШИБКА: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}