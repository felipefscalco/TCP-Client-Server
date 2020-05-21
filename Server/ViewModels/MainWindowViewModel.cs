using Common.Abstractions.Interfaces;
using Common.Messages;
using Common.Models;
using Data.Sqlite.Repositories.Interfaces;
using Prism.Events;
using Prism.Mvvm;
using System;
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
            _eventAggregator.GetEvent<ServerCreateContactMessage>().Subscribe(CreateContact);
        }

        private void CreateContact(Contact newContact)
        {
            if (string.IsNullOrEmpty(newContact.Name) || string.IsNullOrEmpty(newContact.Email))
            {
                _eventAggregator.GetEvent<SendMessageFromServerToClientMessage>().Publish("Não foi possível inserir o contato, nome e email são obrigatórios.");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Erro ao tentar inserir contato.");
                return;
            }
            else
            {
                _contactRepository.InsertContactAsync(newContact);
                _eventAggregator.GetEvent<SendMessageFromServerToClientMessage>().Publish("Contato inserido com sucesso.");
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Contato inserido com sucesso.");
            }
        }

        private void AddConsoleMessageHandler(string message)
         => ConsoleText += message;

        private void InitializeServer()
            => new Task(() => _tcpHandler.Start()).Start();
    }
}