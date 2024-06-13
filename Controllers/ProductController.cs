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
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        //GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        //GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
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
            // Log the category ID
            Console.WriteLine($"Received CategoryId: {categoryId}");

            if (categoryId <= 0)
            {
                return BadRequest("Invalid Category ID");
            }
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);

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
            var products = await _productRepository.GetTopRatedProductsAsync(count);
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
                foreach(var value in ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        Console.WriteLine($"ModelState error: {error.ErrorMessage}");
                    }
                }
                return BadRequest(ModelState);
            }

            try
            {
                //Log the received product DTO
                Console.WriteLine($"Received ProductDto: {productDto.Name}, {productDto.Description}");

                var product = _mapper.Map<Product>(productDto);

                //Log the mapped product
                Console.WriteLine($"Mapped Product: {product.Name}, {product.Description}");

                await _productRepository.AddAsync(product);

                //Log the added product
                Console.WriteLine($"Added Product: {product.ProductId}, {product.Name}");

                var createdProductDto = _mapper.Map<ProductDto>(product);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProductDto.ProductId }, createdProductDto);
            }
            catch (Exception ex)
            {
                //Log the exception
                Console.WriteLine($"Exception while adding product: {ex.Message}");
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
                Console.WriteLine($"Mismatched Ids: Route ID = {id}, DTO ID = {productDto.ProductId}");
                return BadRequest("Product ID mismatch");
            }

            if(!ModelState.IsValid)
            {
                foreach (var value in ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        Console.WriteLine($"ModelState error: {error.ErrorMessage}");
                    }
                }
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDto);

            try
            {
                await _productRepository.UpdateAsync(product);
            }

            catch(DbUpdateConcurrencyException)
            {
                if(await _productRepository.GetByIdAsync(id) == null)
                {
                    return NotFound("Product not found.");
                }
                else
                {
                    throw;
                }
            }
            Console.WriteLine("Product updated successfully.");
            return NoContent();
        }

        //DELETE: api/Product/{id}
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(product);
            return NoContent();
        }
    }
}
