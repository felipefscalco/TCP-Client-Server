using Client.Messages;
using Client.Views;
using Common.Abstractions.Interfaces;
using Common.Messages;
using Common.Models;
using Polly;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
        private string _searchText;

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

        public string SearchText
        {
            get =>_searchText;
            set => SetProperty(ref _searchText, value);
        }

        public DelegateCommand ShowNewContactWindowCommand { get; private set; }
        public DelegateCommand GetAllContactsCommand { get; private set; }
        public DelegateCommand<Contact> EditContactCommand { get; private set; }
        public DelegateCommand<Contact> DeleteContactCommand { get; private set; }
        public DelegateCommand SearchContactCommand { get; private set; }

        public MainWindowViewModel(IEventAggregator eventAggregator, ITcpHandler tcpHandler)
        {
            _eventAggregator = eventAggregator;
            _tcpHandler = tcpHandler;

            ConsoleText = string.Empty;
            ContactList = new ObservableCollection<Contact>();

            SubscribeEvents();

            CreateCommands();

            InitializeClient().ConfigureAwait(false);
        }

        private void CreateCommands()
        {
            ShowNewContactWindowCommand = new DelegateCommand(() => new NewContactView().ShowDialog());
            EditContactCommand = new DelegateCommand<Contact>((contact) => new EditContactView(contact).ShowDialog());
            DeleteContactCommand = new DelegateCommand<Contact>((contact) => DeleteContact(contact));
            GetAllContactsCommand = new DelegateCommand(() => _eventAggregator.GetEvent<GetAllContactsMessage>().Publish());
            SearchContactCommand = new DelegateCommand(() => SearchContacts(SearchText));
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<AddConsoleMessage>().Subscribe(AddConsoleMessageHandler);
            _eventAggregator.GetEvent<UpdateContactsMessage>().Subscribe(UpdateContacts);
        }

        private void SearchContacts(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
                _eventAggregator.GetEvent<SearchContactsMessage>().Publish(searchText);
        }

        private void DeleteContact(Contact contact)
        {
            ContactList.Remove(contact);
            _eventAggregator.GetEvent<DeleteContactMessage>().Publish(contact.Id);
        }

        private void UpdateContacts(List<Contact> contacts)
        {
            ContactList.Clear();

            if (contacts != null)
                ContactList.AddRange(contacts);
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