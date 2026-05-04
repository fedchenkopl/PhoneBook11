using System.Text.RegularExpressions;

namespace PhoneBook10.Models
{
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        public Contact(string name, string phone)
        {
            _name = name;
            _phone = phone;
            Validate();
        }

        public string Name
        {
            get => _name;
            set { if (Set(ref _name, value)) Validate(); }
        }

        public string Phone
        {
            get => _phone;
            set { if (Set(ref _phone, value)) Validate(); }
        }

        public bool IsValid { get; private set; }
        public string? ValidationError { get; private set; }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                IsValid = false;
                ValidationError = "Имя не может быть пустым";
                return;
            }
            if (string.IsNullOrWhiteSpace(_phone))
            {
                IsValid = false;
                ValidationError = "Номер не может быть пустым";
                return;
            }
            if (!Regex.IsMatch(_phone, @"^(\+7|8)?\d{10}$"))
            {
                IsValid = false;
                ValidationError = "Неверный формат номера";
                return;
            }
            IsValid = true;
            ValidationError = null;
        }
    }
}