using Common.Abstractions.Interfaces;
using Common.Messages;
using Common.Models;
using Data.Sqlite.Repositories.Interfaces;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Mvvm;
using Server.Message;
using System;
using System.Threading.Tasks;
using System.Windows.Documents;

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
            _eventAggregator.GetEvent<GetAllContactsMessage>().Subscribe(async () => await GetAllContacts());
        }

        private void EditContact(Contact contactToEdit)
        {
            if (string.IsNullOrEmpty(contactToEdit.Name) || string.IsNullOrEmpty(contactToEdit.Email))
            {
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Não foi possível editar o contato, nome e email são obrigatórios.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Erro ao tentar editar contato.\n\n");
                return;
            }
            else
            {
                _contactRepository.UpdateContactAsync(contactToEdit);
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Contato editado com sucesso.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Contato editado com sucesso.\n\n");
            }
        }

        private async Task GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            var contactsJson = JsonConvert.SerializeObject(contacts);

            _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish(contactsJson);

            _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Lista de contatos devolvida com sucesso.\n\n");
            _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Lista de contatos devolvida com sucesso.\n\n");
        }

        private void CreateContact(Contact newContact)
        {
            if (string.IsNullOrEmpty(newContact.Name) || string.IsNullOrEmpty(newContact.Email))
            {
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Não foi possível inserir o contato, nome e email são obrigatórios.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Erro ao tentar inserir contato.\n\n");
                return;
            }
            else
            {
                _contactRepository.InsertContactAsync(newContact);
                _eventAggregator.GetEvent<SendMessageToClientMessage>().Publish("Contato inserido com sucesso.\n\n");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Contato inserido com sucesso.\n\n");
            }
        }

        private void AddConsoleMessageHandler(string message)
         => ConsoleText += message;

        private void InitializeServer()
            => new Task(() => _tcpHandler.Start()).Start();
    }
}