using Common.Abstractions.Interfaces;
using Common.Messages;
using Data.Sqlite.Repositories.Interfaces;
using Prism.Events;
using Prism.Mvvm;
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
        }

        private void AddConsoleMessageHandler(string message)
         => ConsoleText += message;

        private void InitializeServer()
            => new Task(() => _tcpHandler.Start()).Start();
    }
}