using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;
using System.Linq.Expressions;

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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            try
            {
                var cart = await _cartService.GetCartByUserIdAsync(userId);
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

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            try
            {
                var cart = await _cartService.GetCartByIdAsync(cartId);
                return Ok(cart);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CartDto cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if(cartDto == null)
                {
                    return BadRequest("Cart object is null");
                }

                await _cartService.AddCartAsync(cartDto);
                //return CreatedAtAction(nameof(GetCartById), new { id = cartDto.CartId }, cartDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{cartId}")]
        public async Task<IActionResult> UpdateCart(int cartId, [FromBody] CartDto cartDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _cartService.UpdateCartAsync(cartId, cartDto);
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            try
            {
                var cart = await _cartService.GetCartByIdAsync(cartId);
                if(cart == null)
                {
                    return NotFound();
                }

                await _cartService.DeleteCartAsync(cartId);
                return Ok("Deleted Successfully.");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
