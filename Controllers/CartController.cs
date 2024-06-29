using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var carts = await _cartService.GetAllCartsAsync();
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            try
            {
                var cart = await _cartService.GetCartByIdAsync(id);
                if(cart == null)
                {
                    return NotFound();
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CartDto cartDto)
        {
            try
            {
                if(cartDto == null)
                {
                    return BadRequest("Cart object is null");
                }

                await _cartService.AddCartAsync(cartDto);
                return CreatedAtAction(nameof(GetCartById), new { id = cartDto.CartId }, cartDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] CartDto cartDto)
        {
            try
            {
                if(id != cartDto.CartId)
                {
                    return BadRequest("Cart ID Mismatch");
                }

                var cart = await _cartService.GetCartByIdAsync(id);
                if (cart  == null)
                {
                    return NotFound();
                }

                await _cartService.UpdateCartAsync(cartDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                var cart = await _cartService.GetCartByIdAsync(id);
                if(cart == null)
                {
                    return NotFound();
                }

                await _cartService.DeleteCartAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartsByUserId(int userId)
        {
            try
            {
                var carts = await _cartService.GetCartsByUserIdAsync(userId);
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
