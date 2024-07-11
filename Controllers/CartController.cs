using AutoMapper;
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
                var cartDto = await _cartService.GetCartByUserIdAsync(userId);
                if (cartDto == null)
                {
                    return NotFound();
                }
                return Ok(cartDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            try
            {
                await _cartService.AddToCartAsync(userId, productId, quantity);
                return Ok("Product added to cart successfully");
            }
            catch (Exception ex)
            {
                // Handle generic exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add item to cart.");
            }
        }

        [HttpPost("increase/{cartItemId}")]
        public async Task<IActionResult> IncreaseQuantity(int cartItemId)
        {
            try
            {
                await _cartService.IncreaseQuantityAsync(cartItemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("decrease/{cartItemId}")]
        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            try
            {
                await _cartService.DecreaseQuantityAsync(cartItemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost("checkout /{cartId}")]
        //public async Task<IActionResult> DoCheckout(int cartId)
        //{
        //    try
        //    {
        //        await _cartService.DoCheckoutAsync(cartId);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPut("{cartId}")]
        ////[Authorize(Roles = "User, Admin")]
        //public async Task<IActionResult> UpdateCart(int cartId, [FromBody] CartDto cartDto)
        //{
        //    try
        //    {
        //        var updatedCart = await _cartService.UpdateCartAsync(cartId, cartDto);
        //        return Ok(updatedCart);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpDelete("{cartId}/product/{productId}")]
        //[Authorize(Roles = "User, Admin")]
        //public async Task<IActionResult> RemoveFromCart(int cartId, int productId)
        //{
        //    try
        //    {
        //        await _cartService.RemoveFromCartAsync(cartId, productId);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetCartById(int id)
        //{
        //    try
        //    {
        //        var cart = await _cartService.GetCartByIdAsync(id);
        //        return Ok(cart);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

    }
}
