using PhoneBook10.Services;
using PhoneBook10;
using System.Windows.Input;

namespace PhoneBook10.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly INavigationService _navigationService;

        public INavigationService NavigationService => _navigationService;
        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ShowContactsCommand = new RelayCommand(() => _navigationService.NavigateTo<ContactsListViewModel>());
            ShowAboutCommand = new RelayCommand(() => _navigationService.NavigateTo<AboutViewModel>());
            _navigationService.NavigateTo<ContactsListViewModel>();
        }
    }
}