using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Sqlite.Executors.Interfaces
{
    public interface IDbExecutor
    {
        Task<int> ExecuteAsync(IDbConnection connection, string sql);

        Task InsertAsync<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class;

        void Insert<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class;

        Task DeleteAsync<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class;

        void Delete<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class;

        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(IDbConnection connection, string sql) where TEntity : class;

        Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql);

        Task<TEntity> QueryFirstAsync<TEntity>(IDbConnection connection, string sql);
        
        Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(IDbConnection connection, string sql);

        TEntity QueryFirstOrDefault<TEntity>(IDbConnection connection, string sql);
    }
}