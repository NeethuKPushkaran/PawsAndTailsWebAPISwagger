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


        public async Task AddAsync(Cart entity)
        {
            try
            {
                await _context.Carts.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add to cart", ex);
            }
        }

        public async Task UpdateAsync(Cart entity)
        {
            try
            {
                _context.Carts.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update cart", ex);
            }
        }

        public async Task DeleteAsync(Cart entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Carts.Remove(entity);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete cart", ex);
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
    }
}
