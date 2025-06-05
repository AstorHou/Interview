using Interview.DTOs;
using Interview.Models;
using Interview.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
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
            if (product == null) 
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ProductName))
            {
                return BadRequest("ProductName為必填");
            }

            var product = new Product 
            { 
                ProductName = dto.ProductName,            
                SupplierID = dto.SupplierID,
                CategoryID = dto.CategoryID,
                QuantityPerUnit = dto.QuantityPerUnit,
                UnitPrice = dto.UnitPrice,
                UnitsInStock = dto.UnitsInStock,
                UnitsOnOrder = dto.UnitsOnOrder,
                ReorderLevel = dto.ReorderLevel,
                Discontinued = dto.Discontinued
            };

            var created = await _repository.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = created.ProductID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {          
            if (id != product.ProductID)
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
