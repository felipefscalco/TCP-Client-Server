using Common.Messages;
using Common.Models;
using Prism.Events;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private ClientTcpHandler _clientTcpHandler;
        private string _consoleText;

        public string ConsoleText
        {
            get => _consoleText;
            set => SetProperty(ref _consoleText, value);
        }

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            ConsoleText = string.Empty;

            SubscribeEvents();

            InitializeClient();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<AddConsoleMessage>().Subscribe(AddConsoleMessageHandler);
        }

        private void AddConsoleMessageHandler(string message)
         => ConsoleText += message;

        private void InitializeClient()
            => new Task(() => _clientTcpHandler = new ClientTcpHandler(_eventAggregator)).Start();
    }
}