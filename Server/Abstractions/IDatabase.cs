using System.Data;

namespace Server.Abstractions
{
    public interface IDatabase
    {
        IDbConnection GetDbConnection();
    }
}
