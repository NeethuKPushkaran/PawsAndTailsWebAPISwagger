using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        //GET: api/Cart/User/userId
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<Cart>> GetCartByUserId(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        //POST: api/Cart
        [HttpPost]
        public async Task<ActionResult<Cart>> AddCart(Cart cart)
        {
            await _cartRepository.AddAsync(cart);
            return CreatedAtAction(nameof(GetCartByUserId), new {userId = cart.UserId}, cart);
        }

        //PUT: api/Cart/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, Cart cart)
        {
            if(id != cart.CartId)
            {
                return BadRequest();
            }

            try
            {
                await _cartRepository.UpdateAsync(cart);
            }
            catch (DbUpdateConcurrencyException)
            {
                if(await _cartRepository.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //DELETE: api/Cart/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            await _cartRepository.DeleteAsync(cart);
            return NoContent();
        }
    }
}
