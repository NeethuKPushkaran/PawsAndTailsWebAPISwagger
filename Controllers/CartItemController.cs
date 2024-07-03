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
                if(cartItems == null)
                {
                    return NotFound("CartItems not found.");
                }

                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{cartItemId}")]
        public async Task<IActionResult> GetCartItemById(int cartItemId)
        {
            try
            {
                var cartItem = await _cartItemService.GetCartItemByIdAsync(cartItemId);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if(cartItemDto == null)
                {
                    return BadRequest("CartItem object is null");
                }
                await _cartItemService.AddCartItemAsync(cartItemDto);
                //return CreatedAtAction(nameof(GetCartItemById), new { id = cartItemDto.CartItemId }, cartItemDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] CartItemDto cartItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _cartItemService.UpdateCartItemAsync(cartItemId, cartItemDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            try
            {
                var cartItem = await _cartItemService.GetCartItemByIdAsync(cartItemId);
                if (cartItem == null)
                {
                    return NotFound();
                }

                await _cartItemService.DeleteCartItemAsync(cartItemId);
                return Ok("Deleted successfully");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
