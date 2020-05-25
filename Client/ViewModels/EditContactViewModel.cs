using Client.Messages;
using Client.Views;
using Common.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace Client.ViewModels
{
    public class EditContactViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _name;
        private string _telephone;
        private string _email;
        private string _address;
        private Guid _id;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

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

        public EditContactView Window { get; set; }

        public DelegateCommand EditContactCommand { get; }

        public EditContactViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            EditContactCommand = new DelegateCommand(() =>
            {
                var contactEdited = new Contact(Id, Name, Telephone, Email, Address);
                _eventAggregator.GetEvent<EditContactMessage>().Publish(contactEdited);
                Window.Close();
            });
        }

        public void SetContactDetails(Contact contact)
        {
            Id = contact.Id;
            Name = contact.Name;
            Telephone = contact.Telephone;
            Email = contact.Email;
            Address = contact.Address;
        }

        private string RemoveEspecialCharacters(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value.Replace("\r", string.Empty).Replace("\n", string.Empty);

            return value;
        }
    }
}