using CRUDProject.Interfaces.Repositories;
using CRUDProject.Models;

namespace CRUDProject.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IQueryExecutor queryExecutor;

        public CategoryRepository(IQueryExecutor queryExecutor)
        {
            this.queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var sql = "SELECT * FROM Category";
            return await queryExecutor.QueryAsync<Category>(sql);
        }
    }
}
