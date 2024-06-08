using Microsoft.AspNetCore.Mvc;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;
using System.Net.Sockets;
using Microsoft.AspNetCore.Authorization;

namespace PawsAndTailsWebAPISwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            await _productRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new {id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if(id != product.ProductId)
            {
                return BadRequest();
            }

            await _productRepository.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoveProduct(int id)
        {
            await _productRepository.RemoveProductAsync(id);
            return NoContent();
        }
    }
}
