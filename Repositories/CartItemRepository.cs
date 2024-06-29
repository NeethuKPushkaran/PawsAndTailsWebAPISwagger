using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.Data;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            try
            {
                return await _context.CartItems.Include(ci => ci.Product).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve cart items", ex);
            }
        }

        public async Task<CartItem> GetByIdAsync(int id)
        {
            try
            {
                return await _context.CartItems.Include(ci => ci.Product).SingleOrDefaultAsync(ci => ci.CartItemId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart item with ID {id}", ex);
            }
        }

        public async Task AddAsync(CartItem cartItem)
        {
            try
            {
                await _context.CartItems.AddAsync(cartItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add cart item", ex);
            }
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            try
            {
                _context.Entry(cartItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update cart item", ex);
            }
        }

        public async Task DeleteAsync(CartItem cartItem)
        {
            try
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete cart item", ex);
            }
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            try
            {
                return await _context.CartItems.Include(ci => ci.Product).Where(ci => ci.CartId == cartId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart items for cart ID {cartId}", ex);
            }
        }
    }
}
