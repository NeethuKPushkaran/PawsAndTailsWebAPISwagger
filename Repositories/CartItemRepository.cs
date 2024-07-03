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
                return await _context.CartItems.ToListAsync();
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
                return await _context.CartItems.FirstOrDefaultAsync(c => c.CartItemId == id);
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
                await _context.CartItems.AddAsync(entity);
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
                _context.CartItems.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update cart item", ex);
            }
        }

        public async Task DeleteAsync(CartItem entity)
        {
            try
            {
                _context.CartItems.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete cart item", ex);
            }
        }
    }
}
