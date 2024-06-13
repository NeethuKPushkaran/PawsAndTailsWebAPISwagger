using Microsoft.AspNetCore.Mvc;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;
using System.Net.Sockets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        //GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        //GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        //GET: api/Product/Category/{categoryId}
        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("Invalid Category ID");
            }
            var products = await _productService.GetProductsByCategoryAsync(categoryId);

            //Check if any products are found for the given category
            if (products == null || !products.Any())
            {
                return NotFound($"No Products found for category ID {categoryId}");
            }

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        //GET: api/Product/TopRated/{count}
        [HttpGet("TopRated/{count}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetTopRatedProducts(int count)
        {
            if(count <= 0)
            {
                return BadRequest("Invalid count");
            }
            var products = await _productService.GetTopRatedProductsAsync(count);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        //POST: api/Product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //var product = _mapper.Map<Product>(productDto);

                //await _productRepository.AddAsync(product);

                var createdProduct = await _productService.AddProductAsync(productDto);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        //PUT: api/Product/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if(id != productDto.ProductId)
            {
                return BadRequest("Product ID mismatch");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDto);

            try
            {
                await _productService.UpdateProductAsync(id, productDto);
                return Ok(new { message = "Product updated Successfully" });
            }

            catch(DbUpdateConcurrencyException)
            {
                if(await _productService.GetProductByIdAsync(id) == null)
                {
                    return NotFound("Product not found.");
                }
                else
                {
                    throw;
                }
            }
        }

        //DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound("No product found for the given ID");
            }

            await _productService.DeleteProductAsync(id);
            return Ok("Product Deleted Successfully!");
        }
    }
}
