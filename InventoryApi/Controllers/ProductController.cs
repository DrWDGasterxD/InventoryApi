using InventoryApi.Models;
using InventoryApi.DTOs;
using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();

            var productsDto = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name!,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category?.Name ?? ""
            });

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name!,
                Price = product.Price,
                Stock = product.Stock,
                CategoryName = product.Category?.Name ?? ""
            };

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId
            };
            var newProduct = await _productService.CreateProductAsync(product);

            if(newProduct is null)
            {
                return BadRequest("Category does not exist");
            }

            var responseDto = new ProductDto
            {
                Name = newProduct.Name,
                Id = newProduct.Id,
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                CategoryName = newProduct.Category?.Name ?? ""
            };

            return CreatedAtAction(nameof(GetProduct), new { id = responseDto.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, CreateProductDto productDto)
        {

            var product = new Product
            {
                Name = productDto.Name, 
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
                Price = productDto.Price
            };

            var productUpdate = await _productService.UpdateProductAsync(id, product);

            if (productUpdate is null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
