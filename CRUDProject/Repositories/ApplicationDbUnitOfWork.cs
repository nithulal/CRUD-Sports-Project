using CRUDProject.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace CRUDProject.Repositories
{
    public class ApplicationDbUnitOfWork: DapperUnitOfWork, IAppDbUnitOfWork
    {
        public ApplicationDbUnitOfWork(string sqlConnectionString)
            : base(sqlConnectionString)
        {
        }

        public IProductRepository ProductRepository => Resolve<IProductRepository, ProductRepository>();
    }
}
