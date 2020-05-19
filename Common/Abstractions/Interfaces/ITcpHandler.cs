namespace Common.Abstractions.Interfaces
{
    public interface ITcpHandler
    {
        void Start();
        void CloseConnection();
    }
}