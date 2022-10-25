namespace CRUDProject.Interfaces.Repositories
{
    public interface IUnitOfWorkProvider
    {
        IAppDbUnitOfWork GetApplicationDbUnitOfWork();
    }
}
