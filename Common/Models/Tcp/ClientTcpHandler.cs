using Common.Abstractions.Interfaces;
using Common.Messages;
using Prism.Events;
using System.IO;
using System.Net.Sockets;

namespace Common.Models.Tcp
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