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
            try
            {
                return await _context.Carts.Include(c => c.CartItems).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve carts", ex);
            }
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.CartId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart with ID {id}", ex);
            }
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            try
            {
                return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart with UserID {userId}", ex);
            }
        }

        public async Task AddAsync(Cart cart)
        {
            try
            {
                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add to cart", ex);
            }
        }

        public async Task UpdateAsync(Cart cart)
        {
            try
            {
                _context.Carts.Update(cart);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update cart", ex);
            }
        }

        public async Task DeleteAsync(Cart cart)
        {
            try
            {
                if (cart != null)
                {
                    _context.Carts.Remove(cart);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete cart", ex);
            }
        }
    }
}
