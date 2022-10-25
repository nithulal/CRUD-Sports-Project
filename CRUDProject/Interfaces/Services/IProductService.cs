using CRUDProject.Models;

namespace CRUDProject.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProduct(int ProductId);

        Task<bool> Delete(int ProductId);

        Task<bool> Update(Product product);

        Task<int> Create(Product product);

    }
}
