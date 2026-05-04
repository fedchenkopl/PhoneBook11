using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhoneBook10.Models;
using PhoneBook10.Services;

namespace PhoneBook10.ViewModels
{
    public class ContactsListViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Contact> Contacts { get; private set; }

        private string _name = string.Empty;
        public string Name { get => _name; set => Set(ref _name, value); }

        private string _phone = string.Empty;
        public string Phone { get => _phone; set => Set(ref _phone, value); }

        private Contact? _selectedContact;
        public Contact? SelectedContact { get => _selectedContact; set => Set(ref _selectedContact, value); }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public ContactsListViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, () => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone));
            DeleteCommand = new RelayCommand<Contact>(DeleteContact, c => c != null);
            EditCommand = new RelayCommand<Contact>(EditContact, c => c != null);
        }

        private void AddContact()
        {
            if (Contacts.Any(c => c.Phone == Phone.Trim()))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует!");
                return;
            }
            try
            {
                var newContact = new Contact(Name.Trim(), Phone.Trim());
                Contacts.Add(newContact);
                Name = string.Empty;
                Phone = string.Empty;
                _dialogService.ShowInfo("Контакт добавлен!");
            }
            catch (System.ArgumentException ex)
            {
                _dialogService.ShowError(ex.Message);
            }
        }

        private void DeleteContact(Contact? contact)
        {
            if (contact == null) return;
            if (_dialogService.ShowConfirmation($"Удалить {contact.Name}?"))
            {
                Contacts.Remove(contact);
                _dialogService.ShowInfo("Контакт удалён!");
            }
        }

        private void EditContact(Contact? contact)
        {
            if (contact != null)
                _navigationService.NavigateTo<ContactEditViewModel>(contact);
        }
    }
}