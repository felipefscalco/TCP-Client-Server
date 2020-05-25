using Common.Abstractions.Interfaces;
using Common.Enums;
using Common.Messages;
using Common.Models;
using Newtonsoft.Json;
using Prism.Events;
using Server.Extensions;
using Server.Message;
using System.IO;
using System.Net.Sockets;

namespace Server.Handlers
{
    public class ServerTcpHandler : ITcpHandler
    {
        private TcpListener _server;
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private readonly IEventAggregator _eventAggregator;

        public BinaryReader Reader { get; set; }
        public BinaryWriter Writer { get; set; }

        public ServerTcpHandler(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<SendMessageToClientMessage>().Subscribe(SendMessageToClient);
        }

        private void SendMessageToClient(string message)
            => Writer.Write(message);

        public void Start()
        {
            _server = new TcpListener(8888);
            _server.Start();

            _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Servidor inicializado..\n\n");
            _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Aguardando cliente conectar..\n\n");

            _tcpClient = _server.AcceptTcpClient();
            _networkStream = _tcpClient.GetStream();

            Reader = new BinaryReader(_networkStream);
            Writer = new BinaryWriter(_networkStream);

            _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Cliente conectado.\n\n");

            while (true)
            {
                var jsonMessage = Reader.ReadString();
                var message = JsonConvert.DeserializeObject<MessageExchange>(jsonMessage);

                ResolveAction(message);
            }
        }

        private void ResolveAction(MessageExchange message)
        {
            switch (message.Action)
            {
                case ActionType.NewContact:
                    message.CreateNewContact(_eventAggregator);
                    break;
                case ActionType.GetAllContacts:
                    _eventAggregator.GetEvent<GetAllContactsMessage>().Publish();
                    break;
                case ActionType.EditContact:
                    message.EditContact(_eventAggregator);
                    break;
                case ActionType.DeleteContact:
                    message.DeleteContact(_eventAggregator);
                    break; 
                case ActionType.SearchContact:
                    _eventAggregator.GetEvent<SearchContactsMessage>().Publish(message.Content);
                    break;
                default:
                    break;
            }            
        }

        public void CloseConnection()
        {
            _server.Stop();
            _tcpClient.Close();
            _networkStream.Close();
            Reader.Close();
            Writer.Close();
        }
    }
}