namespace CRUDProject.Interfaces.Services
{
    public interface IDbConfiguration
    {
        string DbConnectionString { get; }
        string UserCatalogDbName { get; }
    }
}
