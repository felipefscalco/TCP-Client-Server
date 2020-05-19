using Common.Abstractions.Interfaces;
using Common.Messages;
using Prism.Events;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace Server.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private ITcpHandler _tcpHandler;
        private string _consoleText;

        public string ConsoleText
        {
            get => _consoleText;
            set => SetProperty(ref _consoleText, value);
        }

        public MainWindowViewModel(IEventAggregator eventAggregator, ITcpHandler tcpHandler)
        {
            _eventAggregator = eventAggregator;
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