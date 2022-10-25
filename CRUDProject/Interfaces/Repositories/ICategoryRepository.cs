using CRUDProject.Models;

namespace CRUDProject.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}
