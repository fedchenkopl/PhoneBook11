using System.Windows.Input;
using PhoneBook10.Models;
using PhoneBook10.Services;

namespace PhoneBook10.ViewModels
{
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private Contact _contact = null!;

        public string EditName
        {
            get => _contact.Name;
            set { _contact.Name = value; OnPropertyChanged(); }
        }

        public string EditPhone
        {
            get => _contact.Phone;
            set { _contact.Phone = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is Contact contact)
                _contact = contact;
        }

        private void Save() => _navigationService.NavigateTo<ContactsListViewModel>();
        private void Cancel() => _navigationService.NavigateTo<ContactsListViewModel>();
    }
}