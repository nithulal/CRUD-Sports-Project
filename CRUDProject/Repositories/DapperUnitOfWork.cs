using CRUDProject.Interfaces.Repositories;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace CRUDProject.Repositories
{
    public  class DapperUnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IQueryExecutor _queryExecutor;
        private ConcurrentDictionary<Type, IRepository> _repositories;


        protected DapperUnitOfWork(string sqlConnectionString)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
                throw new ArgumentException("sqlConnectionString is blank", nameof(sqlConnectionString));

            _connection = new SqlConnection(sqlConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _queryExecutor = new QueryExecutor(_transaction);
            _repositories = new();
        }

        public void Dispose()
        {
           _repositories?.Clear();
           _repositories = null;
           _transaction?.Dispose();
           _transaction = null;
           _connection?.Dispose();
           _connection = null;
           GC.SuppressFinalize(this);
        }

        public bool Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _repositories?.Clear();
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }

            return true;
        }

        protected TImplementation Resolve<TInterface, TImplementation>()
            where TInterface : IRepository
        {
            if (!typeof(TInterface).IsInterface || !typeof(TImplementation).IsClass || !typeof(TInterface).IsAssignableFrom(typeof(TImplementation)))
                throw new ArgumentException($"Invalid usage of Resolve<> detected with types: {typeof(TInterface).FullName} and {typeof(TImplementation).FullName}");

            var result = _repositories.GetOrAdd(typeof(TInterface), (IRepository)Activator.CreateInstance(typeof(TImplementation), _queryExecutor));

            return (TImplementation)result;
        }

        /// <summary>
        /// Intended to be used when the Repository's constructor takes additional parameters
        /// </summary>
        protected TImplementation Resolve<TInterface, TImplementation>(Func<IQueryExecutor, TImplementation> createRepoFunc)
            where TInterface : IRepository
        {
            if (createRepoFunc == null) throw new ArgumentNullException(nameof(createRepoFunc));

            if (!typeof(TInterface).IsInterface || !typeof(TImplementation).IsClass || !typeof(TInterface).IsAssignableFrom(typeof(TImplementation)))
                throw new ArgumentException($"Invalid usage of Resolve<> detected with types: {typeof(TInterface).FullName} and {typeof(TImplementation).FullName}");

            var result = _repositories.GetOrAdd(typeof(TInterface), (IRepository)createRepoFunc(_queryExecutor));

            return (TImplementation)result;
        }

    }
}
