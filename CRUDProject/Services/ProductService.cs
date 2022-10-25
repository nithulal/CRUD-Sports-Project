using CRUDProject.Interfaces;
using CRUDProject.Interfaces.Repositories;
using CRUDProject.Interfaces.Services;
using CRUDProject.Models;
using CRUDProject.Repositories;
using Microsoft.Extensions.Logging;

namespace CRUDProject.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly ILogger _logger;


        public ProductService(ILogger<ProductService> logger, IUnitOfWorkProvider unitOfWorkProvider)
        {
            this._unitOfWorkProvider = unitOfWorkProvider?? throw new ArgumentNullException(nameof(unitOfWorkProvider));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task<int> Create(Product product)
        {
            try
            {                
                using var unitOfWork = _unitOfWorkProvider.GetApplicationDbUnitOfWork();
                var id = await unitOfWork
                    .ProductRepository
                    .Create(product);

                unitOfWork.Commit();
                return id;
            }
            catch (Exception ex)
            {
                Error.TrapError(_logger, ex);
                return 0;
            }
        }

        public async Task<bool> Delete(int ProductId)
        {
            try
            {               
                using var unitOfWork = _unitOfWorkProvider.GetApplicationDbUnitOfWork();
                var status = await unitOfWork
                    .ProductRepository
                    .Delete(ProductId);

                unitOfWork.Commit();
                return status;
            }
            catch (Exception ex)
            {
                Error.TrapError(_logger, ex);
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                using (var unitOfWork = _unitOfWorkProvider.GetApplicationDbUnitOfWork())
                {
                    var result = await unitOfWork.ProductRepository.GetAllProducts();

                    return result;
                }
            }
            catch (Exception ex)
            {
                Error.TrapError(_logger, ex);
                return Enumerable.Empty<Product>();
            }           
        }

        public async Task<Product> GetProduct(int ProductId)
        {
            try
            {
                using (var unitOfWork = _unitOfWorkProvider.GetApplicationDbUnitOfWork())
                {
                    var result = await unitOfWork.ProductRepository.GetProductById(ProductId);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Error.TrapError(_logger, ex);
                return new Product();
            }
        }

        public async Task<bool> Update(Product product)
        {
            try
            {
                using var unitOfWork = _unitOfWorkProvider.GetApplicationDbUnitOfWork();
                var result = await unitOfWork
                    .ProductRepository
                    .Update(product);

                unitOfWork.Commit();
                return result == 0 ? true : false;
            }
            catch (Exception ex)
            {
                Error.TrapError(_logger, ex);
                return false;
            }
        }

    }
}
