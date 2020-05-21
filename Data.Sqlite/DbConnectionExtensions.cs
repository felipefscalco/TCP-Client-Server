using System.Data;

namespace Data.Sqlite
{
    internal static class DbConnectionExtensions
    {
        internal static void EnsureConnectionOpen(this IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
    }
}