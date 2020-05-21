using Client.Views;
using Common.Abstractions.Interfaces;
using Common.Messages;
using Common.Models;
using Polly;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITcpHandler _tcpHandler;
        private ObservableCollection<Contact> _contactList;
        private string _consoleText;
        private bool _isConnected;

        public ObservableCollection<Contact> ContactList
        {
            get => _contactList;
            set => SetProperty(ref _contactList, value);
        }

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        public string ConsoleText
        {
            get => _consoleText;
            set => SetProperty(ref _consoleText, value);
        }

        public DelegateCommand ShowNewContactWindow { get; private set; }

        public MainWindowViewModel(IEventAggregator eventAggregator, ITcpHandler tcpHandler)
        {
            _eventAggregator = eventAggregator;
            _tcpHandler = tcpHandler;

            ContactList = new ObservableCollection<Contact>
            {
                new Contact(Guid.NewGuid(), "name", "telephone", "email", "address"),
                new Contact(Guid.NewGuid(), "name", "telephone", "email", "address"),
                new Contact(Guid.NewGuid(), "name", "telephone", "email", "address"),
                new Contact(Guid.NewGuid(), "name", "telephone", "email", "address"),
                new Contact(Guid.NewGuid(), "name", "telephone", "email", "address")
            };

            ConsoleText = string.Empty;

            SubscribeEvents();

            CreateCommands();

            InitializeClient().ConfigureAwait(false);
        }

        private void CreateCommands()
        {
            ShowNewContactWindow = new DelegateCommand(() => new NewContactView().ShowDialog());
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<AddConsoleMessage>().Subscribe(AddConsoleMessageHandler);
        }

        private void AddConsoleMessageHandler(string message)
         => ConsoleText += message;

        private async Task InitializeClient()
        {
            _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Conectando com o servidor..\n\n");

            var maxRetryAttempts = 5;
            var pauseBetweenFailures = TimeSpan.FromSeconds(5);

            var retryPolicy = Policy
                .Handle<System.Net.Sockets.SocketException>()
                .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);

            var result = await retryPolicy.ExecuteAndCaptureAsync(async () =>
            {
                await Task.Run(() => _tcpHandler.Start());
            });

            if (result.Outcome == OutcomeType.Failure)
                _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Não foi possível conectar com o servidor..\n\n");
            else
                IsConnected = true;
        }
    }
}