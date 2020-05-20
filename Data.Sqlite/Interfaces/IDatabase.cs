using System.Data;

namespace Data.Sqlite.Interfaces
{
    public interface IDatabase
    {
        IDbConnection GetDbConnection();
    }
}