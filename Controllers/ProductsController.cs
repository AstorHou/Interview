using Interview.Models;
using Interview.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _repository.GetAllProductAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var created = await _repository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = created.ProductId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId) 
            {
                return BadRequest();
            }
            
            await _repository.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _repository.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
