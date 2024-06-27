using Microsoft.AspNetCore.Mvc;
using PawsAndTailsWebAPISwagger.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.DTOs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PawsAndTailsWebAPISwagger.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        //GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all products");
                return StatusCode(500, "An internal server error occurred");
            }
        }

        //GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid Product ID");
            }
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the product");
                return StatusCode(500, "An internal server error occurred");
            }
        }

        //GET: api/Product/Category/{categoryId}
        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("Invalid Category ID");
            }
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(categoryId);

                //Check if any products are found for the given category
                if (products == null || !products.Any())
                {
                    return NotFound($"No Products found for category ID {categoryId}");
                }

                return Ok(products);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting products by category");
                return StatusCode(500, "An Internal Server Error Occurred.");
            }
        }

        //GET: api/Product/TopRated/{count}
        [HttpGet("TopRated/{count}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetTopRatedProducts(int count)
        {
            if(count <= 0)
            {
                return BadRequest("Invalid count value");
            }
            try
            {
                var products = await _productService.GetTopRatedProductsAsync(count);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting top-rated products");
                return StatusCode(500, "An Internal Server Error Occurred.");
            }
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
                var createdProduct = await _productService.AddProductAsync(productDto);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the product");
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        //PUT: api/Product/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if(id != productDto.ProductId || id <= 0)
            {
                return BadRequest("Invalid Product ID");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        //DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid Product ID");
            }
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound("No product found for the given ID");
                }

                await _productService.DeleteProductAsync(id);
                return Ok("Product Deleted Successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product");
                return StatusCode(500, "An internal server error occurred");
            }
        }
    }
}
