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
                return await _context.Set<CartItem>().ToListAsync();
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
                return await _context.Set<CartItem>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart item with ID {id}", ex);
            }
        }

        public async Task AddAsync(CartItem entity)
        {
            try
            {
                await _context.Set<CartItem>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add cart item", ex);
            }
        }

        public async Task UpdateAsync(CartItem entity)
        {
            try
            {
                _context.Set<CartItem>().Update(entity);
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

        public async Task<IEnumerable<CartItem>> GetItemsByCartIdAsync(int cartId)
        {
            try
            {
                return await _context.Set<CartItem>()
                    .Where(ci => ci.CartId == cartId)
                    .Include(ci => ci.Product)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart items for cart ID {cartId}", ex);
            }
        }
    }
}
