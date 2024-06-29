using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.Data;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            return await _context.Carts.FindAsync(id);
        }

        public async Task AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cart cart)
        {
            _context.Entry(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cart cart)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int cartId)
        {
            return await _context.CartItems
                                 .Where(ci => ci.CartId == cartId)
                                 .ToListAsync();
        }

        public async Task AddCartItemAsync(int cartId, CartItem cartItem)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if(cart != null)
            {
                cart.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if(cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.Entry(cartItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
