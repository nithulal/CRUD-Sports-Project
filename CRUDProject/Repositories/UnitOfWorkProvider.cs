using CRUDProject.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace CRUDProject.Repositories
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        private readonly IDbConnectionStringProvider dbConnectionStringProvider;

        public UnitOfWorkProvider(
            IDbConnectionStringProvider dbConnectionStringProvider)
        {
            this.dbConnectionStringProvider = dbConnectionStringProvider ?? throw new ArgumentNullException(nameof(dbConnectionStringProvider));

        }

        private string ApplicationDbConnectionString => dbConnectionStringProvider.CreateConnection().ConnectionString;

        public IAppDbUnitOfWork GetApplicationDbUnitOfWork() => new ApplicationDbUnitOfWork(ApplicationDbConnectionString);
    }
}
