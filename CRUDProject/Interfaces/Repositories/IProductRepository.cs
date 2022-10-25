using CRUDProject.Models;

namespace CRUDProject.Interfaces.Repositories
{
    public interface IProductRepository : IRepository
    {
        public Task<IEnumerable<Product>> GetAllProducts();

        public Task<Product> GetProductById(int Id);

        Task<bool> Delete(int Id);

        Task<int> Update(Product product);

        Task<int> Create(Product product);
    }
}
