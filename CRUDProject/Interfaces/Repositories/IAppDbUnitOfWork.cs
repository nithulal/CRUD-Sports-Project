namespace CRUDProject.Interfaces.Repositories
{
    public interface IAppDbUnitOfWork: IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        ICategoryRepository CategoryRepository { get; }
    }
}
