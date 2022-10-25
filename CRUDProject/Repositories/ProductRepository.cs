using CRUDProject.Interfaces.Repositories;
using CRUDProject.Models;
using System.Text;

namespace CRUDProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IQueryExecutor queryExecutor;

        public ProductRepository(IQueryExecutor queryExecutor)
        {
            this.queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public async Task<int> Create(Product product)
        {
            var sql = new StringBuilder();
            sql.Append(@"
                BEGIN
                    INSERT INTO Product
                                (Sku,
                                 Name,
                                 Description,
                                 Price,
                                 IsAvailable,
                                 CategoryId
                                )
                    SELECT      @Sku,
                                @Name,
                                @Description,
                                @Price,
                                @IsAvailable,
                                @CategoryId

                END");

            var parameters = new
            {
                product.Sku,
                product.Name,
                product.Description,
                product.Price,
                product.IsAvailable,
                product.CategoryId
            };

            var id = await queryExecutor.ExecuteScalarAsync<int>(sql.ToString(), parameters);
            return id;
        }

        public async Task<bool> Delete(int Id)
        {
            var sql = new StringBuilder();
            sql.Append(@"DELETE from Product where Id = @ID");
            var status = await queryExecutor.ExecuteScalarAsync<bool>(sql.ToString(), new { Id });
            return status;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var sql = "SELECT * FROM Product";
            return await queryExecutor.QueryAsync<Product>(sql);
        }

        public async Task<Product> GetProductById(int Id)
        {
            var sql = "SELECT * FROM Product WHERE Id = @Id";
            return await queryExecutor.QuerySingleAsync<Product>(sql, new {Id  = Id});
        }

        public async Task<int> Update(Product product)
        {
            var sql = @"
                IF(EXISTS(SELECT TOP 1 * FROM Product WHERE Id = @Id))
                BEGIN 
                    UPDATE Product
                    SET    Sku = @Sku,
                           Name = @Name,
                           Description = @Description,
                           Price = @Price,
                           IsAvailable = @IsAvailable
                           CategoryId = @CategoryId
                    WHERE  Id = @Id;
                 END";

            var parameters = new
            {
                product.Id,
                product.Sku,
                product.Name,
                product.Description,
                product.Price,
                product.CategoryId
            };

            return await queryExecutor.ExecuteAsync(sql.ToString(), parameters);
        }
    }
}
