using Dapper;
using Dapper.Contrib.Extensions;
using Data.Sqlite.Executors.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Sqlite.Executors
{
    public sealed class DapperDbExecutor : IDbExecutor
    {
        public async Task DeleteAsync<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class
            => await connection.DeleteAsync<TEntity>(entity).ConfigureAwait(false);

        public void Delete<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class
            => connection.Delete(entity);

        public async Task<int> ExecuteAsync(IDbConnection connection, string sql)
            => await connection.ExecuteAsync(sql).ConfigureAwait(false);

        public async Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql)
            => await connection.ExecuteScalarAsync<T>(sql).ConfigureAwait(false);

        public async Task InsertAsync<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class
            => await connection.InsertAsync(entity).ConfigureAwait(false);

        public void Insert<TEntity>(IDbConnection connection, TEntity entity) where TEntity : class
            => connection.Insert(entity);

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(IDbConnection connection, string sql) where TEntity : class
            => await connection.QueryAsync<TEntity>(sql).ConfigureAwait(false);

        public async Task<TEntity> QueryFirstAsync<TEntity>(IDbConnection connection, string sql)
            => await connection.QueryFirstAsync<TEntity>(sql).ConfigureAwait(false);

        public async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(IDbConnection connection, string sql)
            => await connection.QueryFirstOrDefaultAsync<TEntity>(sql).ConfigureAwait(false);

        public TEntity QueryFirstOrDefault<TEntity>(IDbConnection connection, string sql)
            => connection.QueryFirstOrDefault<TEntity>(sql);
    }
}