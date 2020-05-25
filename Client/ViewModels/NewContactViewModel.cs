using Client.Messages;
using Client.Views;
using Common.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace Client.ViewModels
{
    public class NewContactViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _name;
        private string _telephone;
        private string _email;
        private string _address;

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, RemoveEspecialCharacters(value));
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Telephone
        {
            get => _telephone;
            set => SetProperty(ref _telephone, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public NewContactView Window { get; set; }

        public DelegateCommand CreateContactCommand { get; }

        public NewContactViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            CreateContactCommand = new DelegateCommand(() =>
            {
                var newContact = new Contact(Guid.NewGuid(), Name, Telephone, Email, Address);
                _eventAggregator.GetEvent<CreateContactMessage>().Publish(newContact);
                Window.Close();
            });
        }
        private string RemoveEspecialCharacters(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value.Replace("\r", string.Empty).Replace("\n", string.Empty);

            return value;
        }
    }
}