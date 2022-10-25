using CRUDProject.Models;

namespace CRUDProject.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}
