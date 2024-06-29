using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;

namespace PawsAndTailsWebAPISwagger.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCartItems()
        {
            try
            {
                var cartItems = await _cartItemService.GetAllCartItemsAsync();
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(int id)
        {
            try
            {
                var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody] CartItemDto cartItemDto)
        {
            try
            {
                if(cartItemDto == null)
                {
                    return BadRequest("CartItem object is null");
                }
                await _cartItemService.AddCartItemAsync(cartItemDto);
                return CreatedAtAction(nameof(GetCartItemById), new { id = cartItemDto.CartItemId }, cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemDto cartItemDto)
        {
            try
            {
                if(id != cartItemDto.CartItemId)
                {
                    return BadRequest("CartItem ID Mismatch");
                }

                var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
                if(cartItem == null)
                {
                    return NotFound();
                }

                await _cartItemService.UpdateCartItemAsync(cartItemDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                await _cartItemService.DeleteCartItemAsync(id);
                return Ok("Deleted successfully");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
