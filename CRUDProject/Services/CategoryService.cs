using CRUDProject.Interfaces.Repositories;
using CRUDProject.Interfaces.Services;
using CRUDProject.Models;

namespace CRUDProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly ILogger _logger;


        public CategoryService(ILogger<ProductService> logger, IUnitOfWorkProvider unitOfWorkProvider)
        {
            this._unitOfWorkProvider = unitOfWorkProvider ?? throw new ArgumentNullException(nameof(unitOfWorkProvider));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                using (var unitOfWork = _unitOfWorkProvider.GetApplicationDbUnitOfWork())
                {
                    var result = await unitOfWork.CategoryRepository.GetCategories();

                    return result;
                }
            }
            catch (Exception ex)
            {
                Error.TrapError(_logger, ex);
                return Enumerable.Empty<Category>();
            }
        }

    }
}
