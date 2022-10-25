using System.Data;

namespace CRUDProject.Interfaces.Repositories
{
    public interface IDbConnectionStringProvider
    {
        public IDbConnection CreateConnection();
    }
}
