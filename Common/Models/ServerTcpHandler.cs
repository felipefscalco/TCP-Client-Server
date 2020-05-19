using Common.Messages;
using Prism.Events;
using System.IO;
using System.Net.Sockets;

namespace Common.Models
{
    public class ServerTcpHandler
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
        }

        public void StartServer()
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

            //int day = Reader.ReadInt16();

            //Console.WriteLine($"Cliente escolheu o dia {day}\n");

            //envia.Write($"Temperatura Minima -> {temperatures.Minimum}");
        }

        public void CloseServer()
        {
            _server.Stop();
            _tcpClient.Close();
            _networkStream.Close();
            Reader.Close();
            Writer.Close();
        }
    }
}