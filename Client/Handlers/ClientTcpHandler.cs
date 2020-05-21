using Common.Abstractions.Interfaces;
using Common.Enums;
using Common.Messages;
using Common.Models;
using Newtonsoft.Json;
using Prism.Events;
using System.IO;
using System.Net.Sockets;

namespace Client.Handlers
{
    public class ClientTcpHandler : ITcpHandler
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private readonly IEventAggregator _eventAggregator;

        public BinaryReader Reader { get; set; }
        public BinaryWriter Writer { get; set; }

        public ClientTcpHandler(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            SubscribeEvents();

            //var day = ReadDay();

            //Writer.Write(day);

            //Console.WriteLine("Aguardando a resposta do servidor..\n");
            //var msg = Reader.ReadString();
            //Console.WriteLine(msg);
            //msg = Reader.ReadString();
            //Console.WriteLine(msg);
            //msg = Reader.ReadString();
            //Console.WriteLine($"{msg} \n");

            //Console.WriteLine("Digite qualquer tecla para sair!");
            //Console.ReadKey();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<ClientCreateContactMessage>().Subscribe(SendNewContact);
        }

        private void SendNewContact(Contact newContact)
        {
            var jsonContact = JsonConvert.SerializeObject(newContact);
            var message = new MessageExchange(ActionType.NewContact, jsonContact);
            var jsonMessage = JsonConvert.SerializeObject(message);

            Writer.Write(jsonMessage);
            
            _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Esperando resposta do servidor..\n\n");

            var messageFromServer = Reader.ReadString();
            
            _eventAggregator.GetEvent<AddConsoleMessage>().Publish(messageFromServer);
        }

        public void Start()
        {
            _tcpClient = new TcpClient("localhost", 8888);

            _eventAggregator.GetEvent<AddConsoleMessage>().Publish("Conexão estabelecida com o servidor..\n\n");

            _networkStream = _tcpClient.GetStream();
            Reader = new BinaryReader(_networkStream);
            Writer = new BinaryWriter(_networkStream);
        }

        public void CloseConnection()
        {
            _tcpClient.Close();
            _networkStream.Close();
            Reader.Close();
            Writer.Close();
        }
    }
}