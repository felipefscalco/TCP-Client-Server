using Common.Abstractions.Interfaces;
using Common.Messages;
using Common.Models;
using Data.Sqlite.Repositories.Interfaces;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Mvvm;
using Server.Message;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IContactRepository _contactRepository;
        private readonly ITcpHandler _tcpHandler;
        private string _consoleText;

        public string ConsoleText
        {
            get => _consoleText;
            set => SetProperty(ref _consoleText, value);
        }

        public MainWindowViewModel(IEventAggregator eventAggregator, IContactRepository contactRepository, ITcpHandler tcpHandler)
        {
            _eventAggregator = eventAggregator;
            _contactRepository = contactRepository;
            _tcpHandler = tcpHandler;

            ConsoleText = string.Empty;

            SubscribeEvents();

            InitializeServer();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<AddConsoleMessage>().Subscribe(AddConsoleMessageHandler);
            _eventAggregator.GetEvent<CreateContactMessage>().Subscribe(CreateContact);
            _eventAggregator.GetEvent<EditContactMessage>().Subscribe(EditContact);
            _eventAggregator.GetEvent<DeleteContactMessage>().Subscribe(DeleteContact);
            _eventAggregator.GetEvent<GetAllContactsMessage>().Subscribe(async () => await GetAllContacts());
            _eventAggregator.GetEvent<SearchContactsByNameMessage>().Subscribe(async (searchText) => await SearchContactsByName(searchText));
            _eventAggregator.GetEvent<SearchContactsByTelephoneMessage>().Subscribe(async (searchText) => await SearchContactsByTelephone(searchText));
        }

        private void DeleteContact(Guid id)
        {
            try
            {
                _contactRepository.DeleteContactAsync(id);
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Contact removed with success.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Contact removed with success.\n\n");
            }
            catch
            {
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Error while trying to remove contact.\n\n");
            }
        }

        private void EditContact(Contact contactToEdit)
        {
            if (string.IsNullOrEmpty(contactToEdit.Name) || string.IsNullOrEmpty(contactToEdit.Email))
            {
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Wasn't possible to edit contact, name and e-mail are mandatory.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Error trying to edit contact, name and e-mail are mandatory.\n\n");
                return;
            }
            else
            {
                _contactRepository.UpdateContactAsync(contactToEdit);
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Contato editado com sucesso.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Contato editado com sucesso.\n\n");
            }
        }

        private async Task SearchContactsByName(string searchText)
        {
            var contacts = await _contactRepository.SearchContactsByNameAsync(searchText);
            var contactsJson = JsonConvert.SerializeObject(contacts);

            _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish(contactsJson);

            _eventAggregator.GetEvent<AddConsoleMessage>().Publish($"{contacts.ToList().Count} contact(s) found.\n\n");
            _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish($"{contacts.ToList().Count} contact(s) found.\n\n");
        }

        private async Task SearchContactsByTelephone(string searchText)
        {
            var contacts = await _contactRepository.SearchContactsByTelephoneAsync(searchText);
            var contactsJson = JsonConvert.SerializeObject(contacts);

            _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish(contactsJson);

            _eventAggregator.GetEvent<AddConsoleMessage>().Publish($"{contacts.ToList().Count} contact(s) found.\n\n");
            _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish($"{contacts.ToList().Count} contact(s) found.\n\n");
        }

        private async Task GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            var contactsJson = JsonConvert.SerializeObject(contacts);

            _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish(contactsJson);

            _eventAggregator.GetEvent<AddConsoleMessage>().Publish("List of contacts received with success.\n\n");
            _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("List of contacts received with success.\n\n");
        }

        private void CreateContact(Contact newContact)
        {
            if (string.IsNullOrEmpty(newContact.Name) || string.IsNullOrEmpty(newContact.Email))
            {
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Wasn't possible to add contact, name and e-mail are mandatory.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Wasn't possible to add contact, name and e-mail are mandatory.\n\n");
                return;
            }
            else
            {
                _contactRepository.InsertContactAsync(newContact);
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Contact inserted with success.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Contact inserted with success.\n\n");
            }
        }

        private void AddConsoleMessageHandler(string message)
         => ConsoleText += message;

        private void InitializeServer()
            => new Task(() => _tcpHandler.Start()).Start();
    }
}