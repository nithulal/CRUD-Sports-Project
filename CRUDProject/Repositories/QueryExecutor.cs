using CRUDProject.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using Dapper;

namespace CRUDProject.Repositories
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IDbTransaction dbTransaction;
        private IDbConnection Connection => dbTransaction?.Connection;
       
        public QueryExecutor(IDbTransaction dbTransaction)
        {
            this.dbTransaction = dbTransaction ?? throw new ArgumentNullException(nameof(dbTransaction));
            //this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }
        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool useCache = false)
        {
           return Connection.QueryAsync<T>(sql, param, dbTransaction, commandTimeout, commandType);            
        }

        public Task<T> QuerySingleAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, bool useCache = false)
        {
           return Connection.QuerySingleAsync<T>(sql, param, dbTransaction, commandTimeout, commandType);
            
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteScalarAsync<T>(sql, param, dbTransaction, commandTimeout, commandType);
        }

        public Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.ExecuteAsync(sql, param, dbTransaction, commandTimeout, commandType);
        }

    }
}
