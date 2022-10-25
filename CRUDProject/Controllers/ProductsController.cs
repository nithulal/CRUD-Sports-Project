using CRUDProject.Interfaces.Repositories;
using CRUDProject.Interfaces.Services;
using CRUDProject.Models;
using CRUDProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CRUDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products.ToList());

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetProduct(int Id)
        {
            var product = await _productService.GetProduct(Id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            var Id = await _productService.Create(product);
            return CreatedAtAction(
                "GetProduct",
                new { Id = product.Id },
                product
                );
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateProduct(int Id, Product product)
        {
            if (Id != product.Id)
                return BadRequest("Update not allowed");

            await _productService.Update(product);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int Id)
        {
            await _productService.Delete(Id);
            return NoContent();
        }

    }

}
